using System.Threading;
using UnityEngine;
using com.fpnn;
using com.fpnn.proto;

class Demo: Main.ITestCase
{
    private string ip = "52.83.245.22";
    private int port = 13609;

    public void Start()
    {
        TCPClient client = new TCPClient(ip, port);

        Debug.Log("\nBegin demonstration to send one way quest");
        DemoSendOneWayQuest(client);

        Debug.Log("\nBegin demonstration to send empty quest");
        DemoSendEmptyQuest(client);

        Debug.Log("\nBegin demonstration to send quest in synchronous");
        DemoSendQuestInSync(client);

        Debug.Log("\nBegin demonstration to send quest in asynchronous");
        DemoSendQuestInAsync(client);
    }

    public void Stop() { }

    static void ShowErrorAnswer(Answer answer)
    {
        Debug.Log("Receive error answer: code: " + answer.ErrorCode() + ", ex: " + answer.Ex());
    }

    static void DemoSendEmptyQuest(TCPClient client)
    {
        Quest quest = new Quest("two way demo");
        Answer answer = client.SendQuest(quest);
        if (answer.IsException())
        {
            ShowErrorAnswer(answer);
            return;
        }

        string v1 = (string)answer.Want("Simple");
        int v2 = answer.Want<int>("Simple2");

        Debug.Log("Receive answer with 'two way demo' quest: 'Simple':" + v1 + ", 'Simple2':" + v2);
    }

    static void DemoSendQuestInSync(TCPClient client)
    {
        Quest quest = new Quest("two way demo");
        quest.Param("key1", 12345);
        quest.Param("key2", 123.45);
        quest.Param("key3", "12345");

        Answer answer = client.SendQuest(quest);
        if (answer.IsException())
        {
            ShowErrorAnswer(answer);
            return;
        }

        string v1 = answer.Want<string>("Simple");
        int v2 = answer.Want<int>("Simple2");

        Debug.Log("Receive answer with 'two way demo' quest: 'Simple':" + v1 + ", 'Simple2':" + v2);
    }

    static void DemoSendQuestInAsync(TCPClient client)
    {
        Quest quest = new Quest("httpDemo");
        quest.Param("key1", 12345);
        quest.Param("key2", 123.45);
        quest.Param("key3", "12345");

        bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {
            if (errorCode != ErrorCode.FPNN_EC_OK)
            {
                if (answer != null)
                    ShowErrorAnswer(answer);
                else
                    Debug.Log("Receive error code " + errorCode + " without available answer.");
            }
            else
            {
                string v1 = (string)answer.Want("HTTP");
                int v2 = answer.Want<int>("TEST");

                Debug.Log("Receive answer with 'httpDemo' quest: 'HTTP':" + v1 + ", 'TEST': " + v2);
            }
        });

        if (status)
            Thread.Sleep(3000);
        else
            Debug.Log("Send 'htpDemo' quest in async failed.");
    }

    static void DemoSendOneWayQuest(TCPClient client)
    {
        Quest quest = new Quest("one way demo", true);
        client.SendQuest(quest);
    }
}

