using System.Threading;
using UnityEngine;
using com.fpnn;

public class Main : MonoBehaviour
{
    public interface ITestCase
    {
        void Start();
        void Stop();
    }

    Thread testThread;
    ITestCase tester;

    // Start is called before the first frame update
    void Start()
    {
        Config config = new Config
        {
            errorRecorder = new Recorder()
        };
        ClientEngine.Init(config);

        TextInfoBridge.Init();

        testThread = new Thread(TestMain)
        {
            IsBackground = true
        };
        testThread.Start();
    }

    void TestMain()
    {
        //-- Examples
        //tester = new Demo();
        //tester = new ConnectionEvents();
        //tester = new OneWayDuplex();
        //tester = new TwoWayDuplex();

        //-- Tests
        tester = new AsyncStressClient();
        //tester = new SingleClientConcurrentTest();

        tester.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        tester.Stop();
        //ClientEngine.Close();
        Debug.Log("Test App exited.");
    }
}
