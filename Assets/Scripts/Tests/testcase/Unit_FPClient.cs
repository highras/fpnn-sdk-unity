using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using com.fpnn;

public class Unit_FPClient {

    private int _port = 13325;
    private int _timeout = 1 * 1000;
    private String _host = "52.83.245.22";

    [SetUp]
    public void SetUp() {}

    [TearDown]
    public void TearDown() {}


    /**
     *  FPClient(string endpoint, int connectionTimeout)
     */
    [Test]
    public void Client_NullEndpoint() {

        int count = 0;
        FPClient client = new FPClient(null, this._timeout);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_EmptyEndpoint() {

        int count = 0;
        FPClient client = new FPClient("", this._timeout);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_NullEndpoint_Connect() {

        int count = 0;
        FPClient client = new FPClient(null, this._timeout);
        client.Connect();
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_EmptyEndpoint_Connect() {

        int count = 0;
        FPClient client = new FPClient("", this._timeout);
        client.Connect();
        Assert.AreEqual(0, count);
    }


    /**
     *  FPClient(string host, int port, int connectionTimeout)
     */
    [Test]
    public void Client_NullHost() {

        int count = 0;
        FPClient client = new FPClient(null, this._port, this._timeout);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_EmptyHost() {

        int count = 0;
        FPClient client = new FPClient("", this._port, this._timeout);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_ZeroPort() {

        int count = 0;
        FPClient client = new FPClient(this._host, 0, this._timeout);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_NegativePort() {

        int count = 0;
        FPClient client = new FPClient(this._host, -1, this._timeout);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_ZeroTimeout() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, 0);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_NegativeTimeout() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, -1);
        Assert.AreEqual(0, count);
    }


    /**
     *  GetProcessor()
     */
    [Test]
    public void Client_GetProcessor() {

        FPClient client = new FPClient(this._host, this._port, this._timeout);
        Assert.IsNotNull(client.GetProcessor());
    }

     
    /**
     *  GetPackage()
     */
    [Test]
    public void Client_GetPackage() {

        FPClient client = new FPClient(this._host, this._port, this._timeout);
        Assert.IsNotNull(client.GetPackage());
    }


    /**
     *  GetSock()
     */
    [Test]
    public void Client_GetSock() {

        FPClient client = new FPClient(this._host, this._port, this._timeout);
        Assert.IsNotNull(client.GetSock());
    }


    /**
     *  Connect()
     */
    [Test]
    public void Client_Connect() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);

        client.Connect();
        Assert.AreEqual(0, count);
    }


    /**
     *  Close()
     */
    [Test]
    public void Client_Close() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        
        client.Close();
        Assert.AreEqual(0, count);
    }


    /**
     *  Close(Exception ex)
     */
    [Test]
    public void Client_Close_NullException() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        
        client.Close(null);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_Close_SimpleException() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        
        client.Close(new Exception());
        Assert.AreEqual(0, count);
    }


    /**
     *  SendQuest(FPData data, CallbackDelegate callback, int timeout)
     */
    [Test]
    public void Client_SendQuest_NullData() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        
        client.SendQuest(null, (cbd) => {
            count++;
        }, this._timeout);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_SendQuest_EmptyData() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        
        client.SendQuest(new FPData(), (cbd) => {
            count++;
        }, this._timeout);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_SendQuest_NullDelegate() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        
        client.SendQuest(new FPData(), null, this._timeout);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_SendQuest_ZeroTimeout() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        
        client.SendQuest(new FPData(), (cbd) => {
            count++;
        }, 0);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_SendQuest_NegativeTimeout() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        
        client.SendQuest(new FPData(), (cbd) => {
            count++;
        }, -100);
        Assert.AreEqual(0, count);
    }


    /**
     *  SendNotify(FPData data)
     */
    [Test]
    public void Client_SendNotify_NullData() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.SendNotify(null);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Client_SendNotify_EmptyData() {

        int count = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.SendNotify(new FPData());
        Assert.AreEqual(0, count);
    }


    /**
     *  IsIPv6()
     */
    [Test]
    public void Client_IsIPv6() {

        FPClient client = new FPClient(this._host, this._port, this._timeout);
        Assert.IsFalse(client.IsIPv6());
    }


    /**
     *  IsOpen()
     */
    [Test]
    public void Client_IsOpen() {

        FPClient client = new FPClient(this._host, this._port, this._timeout);
        Assert.IsFalse(client.IsOpen());
    }


    /**
     *  HasConnect()
     */
    [Test]
    public void Client_HasConnect() {

        FPClient client = new FPClient(this._host, this._port, this._timeout);
        Assert.IsFalse(client.HasConnect());
    }
}
