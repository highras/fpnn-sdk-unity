using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Net.Sockets;
using System.Collections;

using com.fpnn;

public class Integration_FPSocket {

    private int _port = 13325;
    private int _timeout = 1 * 1000;
    private String _host = "52.83.245.22";

    private FPEncryptor _cry;

    [SetUp]
    public void SetUp() {

        FPManager.Instance.Init();
        this._cry = new FPEncryptor(new FPPackage());
    }

    [TearDown]
    public void TearDown() {}

    [UnityTest]
    public IEnumerator Socket_Open() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();

        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, dataCount);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Open_Delay_Close() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, dataCount);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);

        sock.Close(null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, dataCount);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Open_Delay_CloseException() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, dataCount);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);

        sock.Close(new Exception());
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, dataCount);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Open_Close() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        sock.Close(null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Open_CloseException() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        sock.Close(new Exception());
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Open_Close_Close() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        sock.Close(null);
        sock.Close(null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Open_Close_CloseException() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        sock.Close(null);
        sock.Close(new Exception());
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Close_Open() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Close(null);
        sock.Open();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_CloseException_Open() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Close(new Exception());
        sock.Open();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Open_Open() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        sock.Open();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, dataCount);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Open_Delay_Open() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, dataCount);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);

        sock.Open();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, dataCount);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Close_CloseException() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Close(null);
        sock.Close(new Exception());
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_CloseException_Delay_Close() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Close(new Exception());
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);

        sock.Close(null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Open_Timeout() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, 0);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        sock.OnSecond(FPManager.Instance.GetMilliTimestamp());
        yield return new WaitForSeconds(0.5f);
        Assert.IsFalse(sock.IsConnected());
        Assert.IsFalse(sock.IsConnecting());
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Open_Timeout_Close() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, 0);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        sock.OnSecond(FPManager.Instance.GetMilliTimestamp());
        sock.Close(null);
        yield return new WaitForSeconds(0.5f);
        Assert.IsFalse(sock.IsConnected());
        Assert.IsFalse(sock.IsConnecting());
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Open_Timeout_Delay_Close() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, 0);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        sock.OnSecond(FPManager.Instance.GetMilliTimestamp());
        yield return new WaitForSeconds(0.5f);
        sock.Close(null);
        Assert.IsFalse(sock.IsConnected());
        Assert.IsFalse(sock.IsConnecting());
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);
    }

    [UnityTest]
    public IEnumerator Socket_Open_IsIPv6() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        Assert.IsFalse(sock.IsIPv6());
        yield return null;
    }

    [UnityTest]
    public IEnumerator Socket_Open_Delay_IsIPv6() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        yield return new WaitForSeconds(0.5f);
        Assert.IsFalse(sock.IsIPv6());
    }

    [UnityTest]
    public IEnumerator Socket_Open_IsConnected() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        Assert.IsFalse(sock.IsConnected());
        yield return null;
    }

    [UnityTest]
    public IEnumerator Socket_Open_Delay_IsConnected() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        yield return new WaitForSeconds(0.5f);
        Assert.IsTrue(sock.IsConnected());
    }

    [UnityTest]
    public IEnumerator Socket_Open_Close_IsConnected() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        sock.Close(null);
        Assert.IsFalse(sock.IsConnected());
        yield return null;
    }

    [UnityTest]
    public IEnumerator Socket_Open_Close_Delay_IsConnected() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        sock.Close(null);
        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(1, closeCount);
        Assert.IsFalse(sock.IsConnected());
    }

    [UnityTest]
    public IEnumerator Socket_Open_Delay_Close_IsConnected() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        yield return new WaitForSeconds(0.5f);
        sock.Close(null);
        Assert.IsFalse(sock.IsConnected());
    }

    [UnityTest]
    public IEnumerator Socket_Open_IsConnecting() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        Assert.IsFalse(sock.IsConnecting());

        sock.Open();
        Assert.IsTrue(sock.IsConnecting());
        yield return null;
    }

    [UnityTest]
    public IEnumerator Socket_Open_Delay_IsConnecting() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        yield return new WaitForSeconds(0.5f);
        Assert.IsFalse(sock.IsConnecting());
    }

    [UnityTest]
    public IEnumerator Socket_Open_Close_IsConnecting() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        sock.Close(null);
        Assert.IsTrue(sock.IsConnecting());
        yield return null;
    }

    [UnityTest]
    public IEnumerator Socket_Open_Close_Delay_IsConnecting() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        sock.Close(null);
        yield return new WaitForSeconds(1.0f);
        Assert.IsFalse(sock.IsConnecting());
    }

    [UnityTest]
    public IEnumerator Socket_Open_Delay_Write_Read() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = null;
        byte[] buffer = new byte[20];

        OnDataDelegate onData = (stream) => {

            dataCount++;

            Action<NetworkStream, byte[], int> readbytes = null;
            Action<NetworkStream, byte[], int> calllback = (s, b, l) => {

                readbytes(s, b, l);
            };

            readbytes = (st, buf, rlen) => {

                if (rlen < buf.Length) {

                    sock.ReadSocket(st, buf, rlen, (b, l) => {

                        calllback(st, b, l);
                    });
                }
            };

            readbytes(stream, buffer, 0);
        };

        sock = new FPSocket(onData, "www.google.com", 80, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        sock.Open();
        yield return new WaitForSeconds(0.5f);

        sock.Write(new byte[20]);
        yield return new WaitForSeconds(1.0f);
        Assert.IsTrue(sock.IsConnected());
        Assert.IsFalse(sock.IsConnecting());
        Assert.AreEqual(1, dataCount);
        Assert.AreEqual(1, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }
}
