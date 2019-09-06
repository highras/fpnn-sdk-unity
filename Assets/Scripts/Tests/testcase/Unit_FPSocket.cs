using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Net.Sockets;
using System.Collections;

using com.fpnn;

public class Unit_FPSocket {

    private int _port = 13325;
    private int _timeout = 1 * 1000;
    private String _host = "52.83.245.22";

    [SetUp]
    public void SetUp() {}

    [TearDown]
    public void TearDown() {}

    /**
     *  FPSocket(OnDataDelegate onData, string host, int port, int timeout)
     */
    [Test]
    public void Socket_NullDelegate() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket(null, this._host, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_NullHost() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, null, this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_EmptyHost() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, "", this._port, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_ZeroPort() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, 0, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_NegativePort() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++; 
        }, this._host, -1, this._timeout);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_ZeroTimeout() {

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

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_NegativeTimeout() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;

        FPSocket sock = new FPSocket((stream) => {
            dataCount++;
        }, this._host, this._port, -1);

        sock.Socket_Connect = (evd) => {
            connectCount++;
        };
        sock.Socket_Close = (evd) => {
            closeCount++;
        };
        sock.Socket_Error = (evd) => {
            errorCount++;
        };

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_Open() {

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

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_IsIPv6() {

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

        Assert.IsFalse(sock.IsIPv6());

        sock.Open();
        Assert.IsFalse(sock.IsIPv6());
    }

    [Test]
    public void Socket_IsConnected() {

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

        Assert.IsFalse(sock.IsConnected());

        sock.Open();
        Assert.IsFalse(sock.IsConnected());
    }

    [Test]
    public void Socket_IsConnecting() {

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
    }

    [Test]
    public void Socket_OnSecond_ZeroTimeout() {

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

        sock.OnSecond(0);

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_OnSecond_NegativeTimeout() {

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

        sock.OnSecond(-1);

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_OnSecond_SimpleTimeout() {

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

        sock.OnSecond(1567751446);

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_Close_NullException() {

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

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_Close_Exception() {

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

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(1, closeCount);
        Assert.AreEqual(1, errorCount);
    }

    [Test]
    public void Socket_Write_NullBytes() {

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

        byte[] bytes = null;
        sock.Write(bytes);
        
        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_Write_EmptyBytes() {

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

        byte[] bytes = new byte[0];
        sock.Write(bytes);

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_Write_SimpleBytes() {

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

        byte[] bytes = new byte[20];
        sock.Write(bytes);

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
    }

    [Test]
    public void Socket_ReadSocket_NullStream() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        int callbackCount = 0;

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

        byte[] bytes = new byte[20];
        sock.ReadSocket(null, bytes, 20, (buf, len) => {
            callbackCount++;
        });

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(1, errorCount);
        Assert.AreEqual(0, callbackCount);
    }

    [Test]
    public void Socket_ReadSocket_EmptyStream() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        int callbackCount = 0;

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

        byte[] bytes = new byte[0];
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s.Connect(this._host, this._port);
        NetworkStream ns = new NetworkStream(s);
        ns.Close(0);

        sock.ReadSocket(ns, bytes, 20, (buf, len) => {
            callbackCount++;
        });

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(1, errorCount);
        Assert.AreEqual(0, callbackCount);
    }

    [Test]
    public void Socket_ReadSocket_NullCallback() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        int callbackCount = 0;

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

        byte[] bytes = new byte[20];
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s.Connect(this._host, this._port);
        NetworkStream ns = new NetworkStream(s);

        sock.ReadSocket(ns, bytes, 20, null);

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
        Assert.AreEqual(0, callbackCount);
    }

    [Test]
    public void Socket_ReadSocket_NullBytes() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        int callbackCount = 0;

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

        byte[] bytes = null;
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s.Connect(this._host, this._port);
        NetworkStream ns = new NetworkStream(s);

        sock.ReadSocket(ns, bytes, 20, (buf, len) => {
            callbackCount++;
        });

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(1, errorCount);
        Assert.AreEqual(0, callbackCount);
    }

    [Test]
    public void Socket_ReadSocket_EmptyBytes() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        int callbackCount = 0;

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

        byte[] bytes = new byte[0];
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s.Connect(this._host, this._port);
        NetworkStream ns = new NetworkStream(s);

        sock.ReadSocket(ns, bytes, 20, (buf, len) => {
            callbackCount++;
        });

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(1, errorCount);
        Assert.AreEqual(0, callbackCount);
    }

    [Test]
    public void Socket_ReadSocket_ZeroRlen() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        int callbackCount = 0;

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

        byte[] bytes = new byte[20];
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s.Connect(this._host, this._port);
        NetworkStream ns = new NetworkStream(s);
        
        sock.ReadSocket(ns, bytes, 0, (buf, len) => {
            callbackCount++;
        });

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(0, errorCount);
        Assert.AreEqual(0, callbackCount);
    }

    [Test]
    public void Socket_ReadSocket_NegativeRlen() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        int callbackCount = 0;

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

        byte[] bytes = new byte[20];
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s.Connect(this._host, this._port);
        NetworkStream ns = new NetworkStream(s);
        
        sock.ReadSocket(ns, bytes, -1, (buf, len) => {
            callbackCount++;
        });

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(1, errorCount);
        Assert.AreEqual(0, callbackCount);
    }

    [Test]
    public void Socket_ReadSocket_OORRlen() {

        int dataCount = 0;
        int connectCount = 0;
        int closeCount = 0;
        int errorCount = 0;
        int callbackCount = 0;

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

        byte[] bytes = new byte[20];
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s.Connect(this._host, this._port);
        NetworkStream ns = new NetworkStream(s);
        
        sock.ReadSocket(ns, bytes, 30, (buf, len) => {
            callbackCount++;
        });

        Assert.AreEqual(0, dataCount);
        Assert.AreEqual(0, connectCount);
        Assert.AreEqual(0, closeCount);
        Assert.AreEqual(1, errorCount);
        Assert.AreEqual(0, callbackCount);
    }
}
