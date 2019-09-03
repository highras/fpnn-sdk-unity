using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GameDevWare.Serialization;

using com.fpnn;
using UnityEngine;

namespace com.test
{
    public class asyncStressClient
    {

        string _ip;
        int _port;
        int _thread_num;
        int _qps;
        object locker = new object();
        ArrayList _threads = new ArrayList();
        Int64 _send;
        Int64 _recv;
        Int64 _sendError;
        Int64 _recvError;
        Int64 _timecost;

        public FPData buildQuest()
        {
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
            return data;
        }

        public asyncStressClient(string ip, int port, int thread_num, int qps)
        {
            _ip = ip;
            _port = port;
            _thread_num = thread_num;
            _qps = qps;
            _send = 0;
            _recv = 0;
            _sendError = 0;
            _recvError = 0;
            _timecost = 0;
        }

        ~asyncStressClient()
        {
            this.stop();
        }


        void incSend()
        {
            lock (locker)
            {
                _send++;
            }
        }
        void incRecv() { 
            lock (locker)
            { 
                _recv++; 
            }
        }
        void incSendError()
        {
            lock (locker)
            {
                _sendError++;
            }
        }
        void incRecvError() { 
            lock (locker)
            {
                _recvError++;
            }
        }
        void addTimecost(Int64 cost) 
        { 
            lock (locker)
            {
                _timecost += cost;
            }
        }

        public void launch()
        {
            int pqps = _qps / _thread_num;
            if (pqps == 0)
                pqps = 1;
            int remain = _qps - pqps * _thread_num;

            for (int i = 0; i < _thread_num; i++)
            {
                Thread t = new Thread(test_worker);
                t.IsBackground = true;
                t.Start(pqps);
                this._threads.Add(t);
            }


            if (remain > 0)
            {
                Thread t = new Thread(test_worker);
                t.IsBackground = true;
                t.Start(remain);
                this._threads.Add(t);
            }
        }

        public void stop()
        {
            for (int i = 0; i < _threads.Count; i++)
            {
                Thread t = (Thread)_threads[i];
                t.Join();
            }
        }

        public void showStatistics()
        {
            int sleepSeconds = 3000;

            Int64 send = _send;
            Int64 recv = _recv;
            Int64 sendError = _sendError;
            Int64 recvError = _recvError;
            Int64 timecost = _timecost;


            while (true)
            {
                Int64 start = GetMilliTimestamp();

                System.Threading.Thread.Sleep(sleepSeconds);

                Int64 s = _send;
                Int64 r = _recv;
                Int64 se = _sendError;
                Int64 re = _recvError;
                Int64 tc = _timecost;

                Int64 ent = GetMilliTimestamp();

                Int64 ds = s - send;
                Int64 dr = r - recv;
                Int64 dse = se - sendError;
                Int64 dre = re - recvError;
                Int64 dtc = tc - timecost;

                send = s;
                recv = r;
                sendError = se;
                recvError = re;
                timecost = tc;

                Int64 real_time = ent - start;

                ds = ds * 1000 / real_time;
                dr = dr * 1000 / real_time;
                //dse = dse * 1000 * 1000 / real_time;
                //dre = dre * 1000 * 1000 / real_time;
                if (dr > 0)
                    dtc = dtc / dr;


                Debug.Log("time interval: " + (real_time) + " ms, send error: " + dse + ", recv error: " + dre);
                Debug.Log("[QPS] send: " + ds + ", recv: " + dr + ", per quest time cost: " + dtc + " usec");
            }

        }


        public void test_worker(object param)
        {
            int qps = (int)param;
            int usec = 1000 * 1000 / qps;

            Debug.Log("-- qps: " + qps + ", usec: " + usec);

            FPClient client = new FPClient(_ip, _port, 0);
            client.Connect();
            while (true)
            {
                FPData data = buildQuest();
                Int64 send_time = GetMilliTimestamp();

                try
                {
                    client.SendQuest(data, delegate (CallbackData cbd)
                    {
                        if (cbd.GetException() != null)
                        {
                            incRecvError();
                        }
                        else {
                            incRecv();

                            Int64 recv_time = GetMilliTimestamp();
                            Int64 diff = recv_time - send_time;
                            addTimecost(diff);
                        }
                    });
                    incSend();
                }
                catch (Exception)
                {
                    incSendError();
                }
                Int64 sent_time = GetMilliTimestamp();
                Int64 real_usec = (usec - (sent_time - send_time) * 1000) / 1000;
                if (real_usec > 0)
                {
                    System.Threading.Thread.Sleep((int)real_usec);
                }
            }
        }

        public Int64 GetMilliTimestamp() {

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }
}