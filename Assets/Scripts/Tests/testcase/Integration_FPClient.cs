using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

using com.fpnn;
using GameDevWare.Serialization;

public class Integration_FPClient {

    private int _port = 13325;
    private int _timeout = 1 * 1000;
    private String _host = "rtm-intl-frontgate.funplus.com";

    [SetUp]
    public void SetUp() {
        FPManager.Instance.Init();
    }

    [TearDown]
    public void TearDown() {}

    [UnityTest]
    public IEnumerator Client_Connect() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        yield return new WaitForSeconds(1.0f);
        Assert.IsTrue(client.IsOpen());
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Connect_Delay_Close() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        yield return new WaitForSeconds(0.5f);
        client.Close();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Connect_Delay_CloseException() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        yield return new WaitForSeconds(0.5f);
        client.Close(new Exception());
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Connect_Close() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        client.Close();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Connect_CloseException() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        client.Close(new Exception());
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Connect_Close_Close() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        client.Close();
        client.Close();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Connect_Close_CloseException() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        client.Close();
        client.Close(new Exception());
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Close_Connect() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Close();
        client.Connect();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_CloseException_Connect() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Close(new Exception());
        client.Connect();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Connect_Connect() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        client.Connect();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Connect_Delay_Connect() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        yield return new WaitForSeconds(0.5f);
        client.Connect();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Connect_Connect_Close() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        client.Connect();
        client.Close();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Close_CloseException() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Close();
        client.Close(new Exception());
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_CloseException_Delay_Close() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Close(new Exception());
        yield return new WaitForSeconds(0.5f);
        client.Close();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Connect_IsIPv6() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        Assert.IsFalse(client.IsIPv6());
        yield return null;
    }

    [UnityTest]
    public IEnumerator Client_Connect_Delay_IsIPv6() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        yield return new WaitForSeconds(0.5f);
        Assert.IsFalse(client.IsIPv6());
    }

    [UnityTest]
    public IEnumerator Client_Connect_IsOpen() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        Assert.IsFalse(client.IsOpen());
        yield return null;
    }

    [UnityTest]
    public IEnumerator Client_Connect_Delay_IsOpen() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        yield return new WaitForSeconds(1.0f);
        Assert.IsTrue(client.IsOpen());
    }

    [UnityTest]
    public IEnumerator Client_Connect_Close_IsOpen() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        client.Close();
        Assert.IsFalse(client.IsOpen());
        yield return null;
    }

    [UnityTest]
    public IEnumerator Client_Connect_Close_Delay_IsOpen() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        client.Close();
        yield return new WaitForSeconds(1.0f);
        Assert.IsFalse(client.IsOpen());
    }

    [UnityTest]
    public IEnumerator Client_Connect_Delay_Close_IsOpen() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        yield return new WaitForSeconds(1.0f);
        client.Close();
        Assert.IsTrue(client.IsOpen());
    }

    [UnityTest]
    public IEnumerator Client_Connect_HasConnect() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        Assert.IsTrue(client.HasConnect());
        yield return null;
    }

    [UnityTest]
    public IEnumerator Client_Connect_Delay_HasConnect() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        yield return new WaitForSeconds(1.0f);
        Assert.IsTrue(client.HasConnect());
    }

    [UnityTest]
    public IEnumerator Client_Connect_Close_HasConnect() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        client.Close();
        Assert.IsTrue(client.HasConnect());
        yield return null;
    }

    [UnityTest]
    public IEnumerator Client_Connect_Close_Delay_HasConnect() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        client.Close();
        yield return new WaitForSeconds(1.0f);
        Assert.IsFalse(client.HasConnect());
    }

    [UnityTest]
    public IEnumerator Client_Connect_Delay_Close_HasConnec() {
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        yield return new WaitForSeconds(1.0f);
        client.Close();
        Assert.IsTrue(client.HasConnect());
    }

    [UnityTest]
    public IEnumerator Client_Connect_SendQuest() {
        int callbackCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        IDictionary<string, object> payload = new Dictionary<string, object>() {
            { "pid", 11000001 },
            { "uid", 777779 },
            { "what", "rtmGated" },
            { "addrType", "ipv4" },
            { "version", null }
        };
        FPData data = new FPData();
        data.SetFlag(0x1);
        data.SetMtype(0x1);
        data.SetMethod("which");
        byte[] bytes;

        using (MemoryStream outputStream = new MemoryStream()) {
            MsgPack.Serialize(payload, outputStream);
            outputStream.Seek(0, SeekOrigin.Begin);
            bytes = outputStream.ToArray();
        }

        data.SetPayload(bytes);
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        client.SendQuest(data, (cbd) => {
            callbackCount++;
        }, this._timeout);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, callbackCount);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Client_Connect_SendQuest_Close() {
        int callbackCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        IDictionary<string, object> payload = new Dictionary<string, object>() {
            { "pid", 11000001 },
            { "uid", 777779 },
            { "what", "rtmGated" },
            { "addrType", "ipv4" },
            { "version", null }
        };
        FPData data = new FPData();
        data.SetFlag(0x1);
        data.SetMtype(0x1);
        data.SetMethod("which");
        byte[] bytes;

        using (MemoryStream outputStream = new MemoryStream()) {
            MsgPack.Serialize(payload, outputStream);
            outputStream.Seek(0, SeekOrigin.Begin);
            bytes = outputStream.ToArray();
        }

        data.SetPayload(bytes);
        FPClient client = new FPClient(this._host, this._port, this._timeout);
        client.Client_Connect = (evd) => {
            connectCount++;
        };
        client.Client_Close = (evd) => {
            closeCount++;
        };
        client.Client_Error = (evd) => {
            errorCount++;
        };
        client.Connect();
        client.SendQuest(data, (cbd) => {
            callbackCount++;
        }, this._timeout);
        client.Close();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, callbackCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }
}
