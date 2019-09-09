using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameDevWare.Serialization;
using com.fpnn;

public class TestCase : Main.ITestCase {

    private class TestLocker {

        public int Status = 0;
    }

    private FPClient _client;

    public FPClient GetClient() {

        lock (test_locker) {

            return this._client;
        }
    }

    public void StartTest() {

        this.StartThread();
    }

    public void StopTest() {

        this.StopThread();
        this.DestroyClinet();
    }

    private void CreateClinet() {

        lock (test_locker) {

            this._client = new FPClient("52.83.245.22", 13325, 1 * 1000);

            this._client.Client_Close = (evd) => {

                Debug.Log("TestCase closed!");
            };

            this._client.Client_Connect = (evd) => {

                Debug.Log("TestCase connect!");
            };

            this._client.Connect();
        }

        for (int i = 0; i < 100; i++) {

            this.SendQuest();
        }
    }

    private void DestroyClinet() {

        lock (test_locker) {

            if (this._client != null) {

                this._client.Close();
                this._client = null;
            }
        }
    }

    private Thread _thread;
    private TestLocker test_locker = new TestLocker();

    private void StartThread() {

        lock (test_locker) {

            if (test_locker.Status != 0) {

                return;
            }

            test_locker.Status = 1;

            this._thread = new Thread(new ThreadStart(TestCreateAndDestroy));
            this._thread.IsBackground = true;
            this._thread.Start();
        }
    }

    private void StopThread() {

        lock (test_locker) {

            test_locker.Status = 0;
        }
    }

    private int count;

    private void TestCreateAndDestroy() {

        TestCase self = this;

        try {

            while(true) {

                lock (test_locker) {

                    if (test_locker.Status == 0) {

                        return;
                    } 
                }

                if (++count % 2 != 0) {

                    self.CreateClinet();
                    Thread.Sleep(3 * 1000);
                }else {

                    self.DestroyClinet();
                    // Thread.Sleep(1 * 1000);
                }
            }
        }catch(Exception ex) {

            Debug.Log(ex);
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

    private void SendQuest() {

        this._payload = null;

        lock (test_locker) {

            if (this._client != null) {

                this._client.SendQuest(this.GetPayloadData(), (cbd) => {

                    Debug.Log("SendQuest success");
                }, 1 * 1000);
            }
        }
    }
}
