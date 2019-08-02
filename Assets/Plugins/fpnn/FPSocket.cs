using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace com.fpnn {

    public delegate void OnDataDelegate(NetworkStream stream, FPSocket.SocketLocker socket_locker);

    public class FPSocket {

        public class SocketLocker {

            public int Count = 0;
            public int Status = 0;
        }

        public Action<EventData> Socket_Connect;
        public Action<EventData> Socket_Close;
        public Action<EventData> Socket_Error;

        private int _port;
        private string _host;
        private int _timeout;
        private OnDataDelegate _onData;

        private TcpClient _socket;
        private NetworkStream _stream;

        private bool _isIPv6 = false;
        private bool _isConnecting = false;

        private List<byte> _sendQueue = new List<byte>();
        private ManualResetEvent _sendEvent = new ManualResetEvent(false);

        private SocketLocker socket_locker = new SocketLocker();

        public FPSocket(OnDataDelegate onData, string host, int port, int timeout) {

            this._host = host;
            this._port = port;
            this._timeout = timeout;
            this._onData = onData;
        }

        private object self_locker = new object();

        public void Open() {

            lock (self_locker) {

                if (String.IsNullOrEmpty(this._host)) {

                    this.OnError(new Exception("Cannot open null host"));
                    return;
                }
                
                if (this._port <= 0) {

                    this.OnError(new Exception("Cannot open without port"));
                    return;
                }

                if (this._socket != null && (this.IsOpen() || this.IsConnecting())) {

                    this.OnError(new Exception("has been connect!"));
                    return;
                }

                lock (socket_locker) {

                    socket_locker.Count = 0;
                    socket_locker.Status = 0;
                }

                FPSocket self = this;

                ThreadPool.QueueUserWorkItem(new WaitCallback((state) => {

                    self._isConnecting = true;

                    try {

                        var success = false;
                        IAsyncResult result = null;

                        lock (socket_locker) {

                            IPHostEntry hostEntry = Dns.GetHostEntry(self._host);
                            IPAddress ipaddr = hostEntry.AddressList[0];

                            if (ipaddr.AddressFamily != AddressFamily.InterNetworkV6) {

                                self._socket = new TcpClient(AddressFamily.InterNetwork);
                            } else {

                                self._isIPv6 = true;
                                self._socket = new TcpClient(AddressFamily.InterNetworkV6);
                            }

                            result = self._socket.BeginConnect(ipaddr, self._port, null, null);
                            success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds((double)self._timeout));
                        }

                        if (!success) {

                            self._isConnecting = false;

                            self.Close(new Exception("Connect Timeout"));
                            return;
                        } 

                        lock (socket_locker) {

                            self._socket.EndConnect(result);
                            self._stream = self._socket.GetStream();
                        }

                        self._isConnecting = false;

                        self.StartSendThread();
                        self.OnRead(self._stream, socket_locker);

                        self.OnConnect();
                    } catch (Exception ex) {

                        self._isConnecting = false;
                        self.Close(ex);
                    } 
                }));
            }
        }

        public bool IsIPv6() {

            lock (socket_locker) {

                return this._isIPv6 ? true : false;
            }
        }

        public bool IsOpen() {

            lock (socket_locker) {

                if (this._socket != null) {

                    return this._socket.Connected ? true : false;
                }

                return false;
            }
        }

        public bool IsConnecting() {

            return this._isConnecting;
        }

        public void Close(Exception ex) {

            lock (socket_locker) {

                if (socket_locker.Status == 0) {

                    socket_locker.Status = 1;

                    if (ex != null) {

                        this.OnError(ex);
                    }

                    this.OnClose();
                }

                this.TryClose();
            }
        }

        private void TryClose() {

            if (socket_locker.Status == 3) {

                return;
            }

            if (socket_locker.Count != 0) {

                return;
            }

            socket_locker.Status = 3;

            lock(this._sendQueue) {

                this._sendEvent.Set();
            }

            if (this._stream != null) {

                this._stream.Close();
            }

            if (this._socket != null) {

                this._socket.Close();
            }
        }

        private void OnClose() {

            if (this.Socket_Close != null) {

                this.Socket_Close(new EventData("close"));
            }
        }

        public void Write(byte[] buffer) {

            lock(this._sendQueue) {

                for (int i = 0; i < buffer.Length; i++) {

                    this._sendQueue.Add(buffer[i]);
                }

                this._sendEvent.Set();
            }
        }

        public void Destroy() {

            this.Close(null);
            
            lock (self_locker) {

                this._onData = null;

                this.Socket_Connect = null;
                this.Socket_Close = null;
                this.Socket_Error = null;
            }
        }

        public string GetHost() {

            return this._host;
        }

        public int GetPort() {

            return this._port;
        }

        public int GetTimeout() {

            return this._timeout;
        }

        private void OnConnect() {

            if (this.Socket_Connect != null) {

                this.Socket_Connect(new EventData("connect"));
            }
        }

        private void OnRead(NetworkStream stream, SocketLocker socket_locker) {

            this._onData(stream, socket_locker);
        }

        private void OnError(Exception ex) {

            if (this.Socket_Error != null) {

                this.Socket_Error(new EventData("error", ex));
            }
        }

        private void StartSendThread() {

            FPSocket self = this;

            ThreadPool.QueueUserWorkItem(new WaitCallback((state) => { 

                try {

                    self.OnWrite();
                } catch (ThreadAbortException tex) {
                } catch (Exception ex) {

                    self.Close(ex);
                } 
            }));
        }

        private void OnWrite() {

            this._sendEvent.WaitOne();

            byte[] buffer = new byte[0];

            lock(this._sendQueue) {

                buffer = this._sendQueue.ToArray();

                this._sendQueue.Clear();
                this._sendEvent.Reset();
            }

            this.WriteSocket(buffer, OnWrite);
        }

        private void WriteSocket(byte[] buffer, Action calllback) {

            lock (socket_locker) {

                socket_locker.Count++;
            }

            try {

                FPSocket self = this;

                this._stream.BeginWrite(buffer, 0, buffer.Length, (ar) => {

                    try {

                        try {

                            self._stream.EndWrite(ar);

                        } catch (Exception ex) {

                            self.Close(ex);
                        }

                        lock (socket_locker) {

                            socket_locker.Count--;
                        }

                        if (calllback != null) {

                            calllback();
                        }
                    } catch (Exception ex) {

                        self.Close(ex);
                    }
                }, null);
            } catch (Exception ex) {

                this.Close(ex);
            }
        }
    }
}