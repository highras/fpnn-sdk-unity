using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameDevWare.Serialization;
using com.fpnn;

public class SingleClientConnect : Main.ITestCase {

    private class SendLocker {

        public int Status = 0;
    }

    private FPClient _client;
    private SendLocker send_locker = new SendLocker();

    public SingleClientConnect() {}

    public void StartTest() {

        this._client = new FPClient("52.83.245.22", 13013, 5000);

        SingleClientConnect self = this;

        this._client.Client_Connect = (evd) => {

            Debug.Log("connected");
            self.StartThread();
        };

        this._client.Client_Close = (evd) => {

            Debug.Log("closed");  
        };

        this._client.Client_Error = (evd) => {

            Debug.Log("error: " + evd.GetException());
        };

        this._client.Connect();
    }

    public void StopTest() {

        this.StopThread();

        if (this._client != null) {

            this._client.Close();
        }
    }

    private FPData _payload;

    private FPData GetPayloadData() {

        if (this._payload == null) {

            Dictionary<string, object> payload = new Dictionary<string, object>() {

                {"pid", 11000001},
                {"uid", 123},
                {"token", "ADB313B3083ECE368312AA87AF86A6C3"},
                {"version", "aaa"},
                {"attrs", ""}
            };

            byte[] bytes;

            using (MemoryStream outputStream = new MemoryStream()) {

                MsgPack.Serialize(payload, outputStream);
                outputStream.Seek(0, SeekOrigin.Begin);

                bytes = outputStream.ToArray();
            }

            this._payload = new FPData();
            this._payload.SetFlag(0x1);
            this._payload.SetMtype(0x1);
            this._payload.SetMethod("two way demo");
            this._payload.SetPayload(bytes);
        }

        return this._payload;
    }

    private Thread _thread;

    private void StartThread() {

        lock (send_locker) {

            if (send_locker.Status != 0) {

                return;
            }

            send_locker.Status = 1;

            this._thread = new Thread(new ThreadStart(SendQuest));
            this._thread.Start();
        }
    }

    private void StopThread() {

        lock (send_locker) {

            send_locker.Status = 0;
        }
    }

    private void SendQuest() {

        try {

            while(true) {

                lock (send_locker) {

                    if (send_locker.Status == 0) {

                        return;
                    }

                    this._client.SendQuest(this.GetPayloadData(), (cbd) => {

                        if (cbd.GetException() == null) {

                            MemoryStream inputStream = new MemoryStream(cbd.GetData().MsgpackPayload());
                            Dictionary<string, object> dict = MsgPack.Deserialize<Dictionary<string, object>>(inputStream);

                            MemoryStream jsonStream = new MemoryStream();
                            Json.Serialize(dict, jsonStream);
                            
                            Debug.Log("got answer: " + System.Text.Encoding.UTF8.GetString(jsonStream.ToArray()));
                        } else {

                            Debug.Log("got exception: " + cbd.GetException().Message);
                        }
                    });
                }
            }
        }catch(Exception ex) {

            Debug.Log(ex);
        }
    }
}
