using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using GameDevWare.Serialization;
using UnityEngine;

namespace com.fpnn
{
    class Program
    {
        private FPClient _client;

        public void Begin() {

            this.Main();
        }

        public void End() {

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

            this._client = new FPClient("52.83.245.22", 13013, true, 5000);

            this._client.GetEvent().AddListener("ping", (evd) => {

                Debug.Log("method: ping " + evd.GetPayload());
            });

            Program self = this;

            this._client.GetEvent().AddListener("connect", (evd) => {

                Debug.Log("connected");

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

                self._client.SendQuest(data, (cbd) => {

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
            });

            this._client.GetEvent().AddListener("close", (evd) => {

                Debug.Log("closed");  
            });

            this._client.GetEvent().AddListener("error", (evd) => {

                Debug.Log("error: " + evd.GetException());
            });

            this._client.Connect();
        }
    }
}
