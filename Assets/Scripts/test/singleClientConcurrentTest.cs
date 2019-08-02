using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GameDevWare.Serialization;

namespace com.fpnn
{
    public class singleClientConcurrentTest
    {

        FPClient client = null;

        public singleClientConcurrentTest()
        {
        }

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
            data.SetMethod("test");
            data.SetPayload(payload);
            return data;
        }

        void showSignDesc()
        {
            Console.WriteLine("Sign:");
            Console.WriteLine("    +: establish connection");
            Console.WriteLine("    ~: close connection");
            Console.WriteLine("    #: connection error");

            Console.WriteLine("    *: send sync quest");
            Console.WriteLine("    &: send async quest");

            Console.WriteLine("    ^: sync answer Ok");
            Console.WriteLine("    ?: sync answer exception");
            Console.WriteLine("    |: sync answer exception by connection closed");
            Console.WriteLine("    (: sync operation fpnn exception");
            Console.WriteLine("    ): sync operation unknown exception");

            Console.WriteLine("    $: async answer Ok");
            Console.WriteLine("    @: async answer exception");
            Console.WriteLine("    ;: async answer exception by connection closed");
            Console.WriteLine("    {: async operation fpnn exception");
            Console.WriteLine("    }: async operation unknown exception");

            Console.WriteLine("    !: close operation");
            Console.WriteLine("    [: close operation fpnn exception");
            Console.WriteLine("    ]: close operation unknown exception");
        }

        void testThread(object param)
        {
            int count = (int)param;
            int act = 0;
            for (int i = 0; i < count; i++)
            {
                Int64 index = (FPManager.Instance.GetMilliTimestamp() + i) % 64;
                if (i >= 10)
                {
                    if (index < 6)
                        act = 2;    //-- close operation
                    else if (index < 32)
                        act = 1;    //-- async quest
                    else
                        act = 0;    //-- sync quest
                }
                else
                    act = (int)index & 0x1;
                try
                {
                    switch (act)
                    {
                        case 0:
                            Console.Write("*");
                            CallbackData cbdS = client.SendQuest(buildQuest(), 0);
                            if (cbdS.GetException() == null)
                            {
                                Console.Write("^");
                            }
                            else {
                                Console.Write("?");
                            }

                            break;
                        case 1:
                            Console.Write("&");

                            client.SendQuest(buildQuest(), delegate (CallbackData cbd)
                            {
                                if (cbd.GetException() == null)
                                {
                                    Console.Write("$");
                                }
                                else
                                {
                                    Console.Write("@");
                                }
                            });
                            break;
                        case 2:
                            Console.Write("!");
                            client.Close();
                            break;
                    }
                }
                catch (Exception)
                {
                    switch (act)
                    {
                        case 0: Console.Write(')'); break;
                        case 1: Console.Write('}'); break;
                        case 2: Console.Write(']'); break;
                    }
                }

            }
        }

        void test(FPClient client, int threadCount, int questCount)
        {
            Console.WriteLine("========[ Test: thread " + threadCount + ", per thread quest: " + questCount + " ]==========");

            ArrayList _threads = new ArrayList();

            for (int i = 0; i < threadCount; i++)
            {
                Thread t = new Thread(testThread);
                t.IsBackground = true;
                t.Start(questCount);
                _threads.Add(t);
            }

            System.Threading.Thread.Sleep(5000);

            for (int i = 0; i < _threads.Count; i++)
            {
                Thread t = (Thread)_threads[i];
                t.Join();
            }
        }


        public void launch()
        {
            client = new FPClient("52.83.245.22:13697", 0);

            client.Client_Connect = (evd) => {

                Console.Write("+");
            };

            client.Client_Close = (evd) => {

                Console.Write("#");
            };

            client.Connect();

            showSignDesc();
            test(client, 15, 3000);

            /*test(client, 10, 30000);
            test(client, 20, 30000);
            test(client, 30, 30000);
            test(client, 40, 30000);
            test(client, 50, 30000);
            test(client, 60, 30000);*/
        }
    }
}