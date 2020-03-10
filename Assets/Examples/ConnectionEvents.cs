using UnityEngine;
using com.fpnn;
using com.fpnn.proto;

class ConnectionEvents : Main.ITestCase
{
    private string ip = "52.83.245.22";
    private int port = 13609;

    public void Start()
    {
        Debug.Log("Start");
    }

    public void Stop()
    {
        Debug.Log("Stop");
    }
}
