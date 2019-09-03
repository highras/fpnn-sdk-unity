using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using GameDevWare.Serialization;

using com.fpnn;
using UnityEngine;

namespace com.test
{
    class Program
    {

        private class SendLocker {

            public int Status = 0;
        }

        private FPClient _client;
        private SendLocker send_locker = new SendLocker();

        public void Begin() {

            this.Main();
        }

        public void End() {

            this.StopThread();
            
            if (this._client != null) {

                this._client.Close();
            }
        }

        void Main() {
            
            /* asyncStressClient tester = new asyncStressClient("52.83.245.22", 13697, 2, 10);
            tester.launch();
            tester.showStatistics(); */

            /* singleClientConcurrentTest tester = new singleClientConcurrentTest();
			tester.launch();*/

            this._client = new FPClient("52.83.245.22", 13013, 5000);

            Program self = this;

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

        private Thread _thread;

        private void StartThread() {

            lock (send_locker) {

                if (send_locker.Status != 0) {

                    return;
                }

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

                        Dictionary<string, object> mp = new Dictionary<string, object>();
                        mp.Add("pid", 11000001);
                        mp.Add("uid", 123);
                        mp.Add("token", "ADB313B3083ECE368312AA87AF86A6C3");
                        mp.Add("version", "aaa");
                        mp.Add("attrs", "");

                        MemoryStream outputStream = new MemoryStream();

                        MsgPack.Serialize(mp, outputStream);
                        outputStream.Position = 0; 

                        byte[] payload = outputStream.ToArray();

                        FPData data = new FPData();
                        data.SetFlag(0x1);
                        data.SetMtype(0x1);
                        data.SetMethod("two way demo");
                        data.SetPayload(payload);

                        this._client.SendQuest(data, (cbd) => {

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
}
