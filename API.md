# FPNN Unity SDK API Docs

# Index

[TOC]

## Current Version

	public static readonly string com.fpnn.Config.Version = "2.0.0";

## Init & Config SDK

Initialize and configure FPNN SDK. 

	using com.fpnn;
	ClientEngine.Init();
	ClientEngine.Init(Config config);

### com.fpnn.Config Fields

* **Config.taskThreadPoolConfig.initThreadCount**

	Inited threads count of SDK task thread pool. Default value is 1.

* **Config.taskThreadPoolConfig.perfectThreadCount**

	Max resident threads count of SDK task thread pool. Default value is 2.

* **Config.taskThreadPoolConfig.maxThreadCount**

	Max threads count of SDK task thread pool, including resident threads and temporary threads. Default value is 4.

* **Config.taskThreadPoolConfig.maxQueueLengthLimitation**

	Max tasks count of SDK task thread pool. Default value is 0, means no limitation.

* **Config.taskThreadPoolConfig.tempLatencySeconds**

	How many seconds are waited for the next dispatched task before the temporary thread exit. Default value is 60.

* **Config.globalConnectTimeoutSeconds**

	Global client connecting timeout setting when no special connecting timeout are set for a client or connect function.

	Default is 5 seconds.

* **Config.globalQuestTimeoutSeconds**

	Global quest timeout setting when no special quest timeout are set for a client or sendQuest function.

	Default is 5 seconds.

* **Config.maxPayloadSize**

	Max bytes limitation for the quest & answer package. Default is 4MB.

* **Config.errorRecorder**

	Instance of com.fpnn.common.ErrorRecoder implemented. Default is null.


## FPNN TCPClient

### Constructors

	public TCPClient(string host, int port, bool autoConnect = true);
	public static TCPClient Create(string host, int port, bool autoConnect = true);
	public static TCPClient Create(string endpoint, bool autoConnect = true);

* endpoint:

	RTM servers endpoint. Please get your project endpoint from RTM Console.

* host:

	Target server host name or IP address.

* port:

	Target server port.

* autoConnect:

	Auto connect. Note: This parameter is AUTO CONNECT, not KEEP connection.

### Properties

* **ConnectTimeout**

		public volatile int ConnectTimeout { get; set; }

	Connecting timeout in seconds for current TCPClient instance. Default is 0, meaning using the global config. 

* **QuestTimeout**

		public volatile int QuestTimeout { get; set; }

	Quest timeout in seconds for current TCPClient instance. Default is 0, meaning using the global config.

* **AutoConnect**

		public volatile bool AutoConnect { get; set; }

	Auto connect. Note: This parameter is AUTO CONNECT, not KEEP connection.


### Config & Properties Methods

#### public string Endpoint()

	public string Endpoint();

Return current endpoint.

#### public ClientStatus Status()

	public ClientStatus Status();

Return client current status.

Values:

+ ClientStatus.Closed
+ ClientStatus.Connecting
+ ClientStatus.Connected

#### public bool IsConnected()

	public bool IsConnected();

Return client current is connected or not.


#### public void SetErrorRecorder(common.ErrorRecorder recorder)

	public void SetErrorRecorder(common.ErrorRecorder recorder);

Config the ErrorRecorder instance for current client. Default is null.


### Connect & Close Methods


#### public void AsyncConnect()

	public void AsyncConnect();

Start an asynchronous connecting. The connecting timeout can be configured by `ConnectTimeout` property.


#### public bool SyncConnect()

	public bool SyncConnect();

Do a synchronous connecting. The connecting timeout can be configured by `ConnectTimeout` property.


#### public void AsyncReconnect()

	public void AsyncReconnect();

Start an asynchronous connecting after closing the current connection. The connecting timeout can be configured by `ConnectTimeout` property.


#### public bool SyncReconnect()

	public bool SyncReconnect();

Do a synchronous connecting after closing the current connection. The connecting timeout can be configured by `ConnectTimeout` property.

#### public void Close()

	public void Close();

Close the current connection. Calling the method in all cases is safe.

### Event Methods

#### public void SetConnectionConnectedDelegate(ConnectionConnectedDelegate ccd)

	public void SetConnectionConnectedDelegate(ConnectionConnectedDelegate ccd);

Config the connecting callback. Whether connected or not, the callback will be called.

Prototype:

	public delegate void ConnectionConnectedDelegate(Int64 connectionId, string endpoint, bool connected);

Parameters:

+ `Int64 connectionId`

	Unique id when the connection is connected. When the connection closing callback returned, this id may be reused by another connection.

	If connecting is failure, this id is 0. 

+ `string endpoint`

	Endpoint of the target server for this connection.

+ `bool connected`

	Connecting successful or not.


#### public void SetConnectionCloseDelegate(ConnectionCloseDelegate cwcd)

	public void SetConnectionCloseDelegate(ConnectionCloseDelegate cwcd);

Config the closing callback for connected connection.

Prototype:

	public delegate void ConnectionCloseDelegate(Int64 connectionId, string endpoint, bool causedByError);

Parameters:

+ `Int64 connectionId`

	Unique connection id before the callbakc returned. When the callback returned, this id may be reused by another connection.

+ `string endpoint`

	Endpoint of the target server for this connection.

+ `bool causedByError`

	Connection closing is triggered by error or normal close (e.g. motivated calling Close function, or normal shutdown).


#### public void SetQuestProcessor(IQuestProcessor processor)

	public void SetQuestProcessor(IQuestProcessor processor);

Config processor for server push.

	public delegate Answer QuestProcessDelegate(Int64 connectionId, string endpoint, Quest quest);

    public interface IQuestProcessor
    {
        QuestProcessDelegate GetQuestProcessDelegate(string method);
    }

Parameters:

+ `Int64 connectionId`

	Unique id when the connection is connected. When the connection closing callback returned, this id may be reused by another connection.

	If connecting is failure, this id is 0. 

+ `string endpoint`

	Endpoint of the target server for this connection.

+ `Quest quest`

	Server pushed quest. Return answer for two way quest, and `null` for one way quest.

+ `string method`

	The API or interface are requested by the server pushed quest.

`QuestProcessDelegate`:

Please return answer for two way quest, and `null` for one way quest.

If an AsyncAnswer instance is created in delegate function, or `AdvanceAnswer.SendAnswer(Answer answer)` is called in delegate function, the delegate function **MUST** return `null` for two way quest.


### Send Quest & Answer Methods


#### public bool SendQuest(Quest quest, IAnswerCallback callback, int timeout = 0)

	public bool SendQuest(Quest quest, IAnswerCallback callback, int timeout = 0);

Start an asynchronous request.

	public interface IAnswerCallback
    {
        void OnAnswer(Answer answer);
        void OnException(Answer answer, int errorCode);
    }

If request successful, the `OnAnswer` interface will be called;  
if failed, the `OnException` interface will be called. In failed case, the `Answer answer` maybe `null`.

#### public bool SendQuest(Quest quest, AnswerDelegate callback, int timeout = 0)

	public bool SendQuest(Quest quest, AnswerDelegate callback, int timeout = 0);

Start an asynchronous request.

	public delegate void AnswerDelegate(Answer answer, int errorCode);

If errorCode is 0 (com.fpnn.ErrorCode.FPNN_EC_OK), means request is successful;  
else, the failed reason is hinted by the errorCode. For failed case,  `Answer answer` maybe `null`.


#### public Answer SendQuest(Quest quest, int timeout = 0)

	public Answer SendQuest(Quest quest, int timeout = 0);

Request server in synchonous way.

#### public void SendAnswer(Answer answer)

	public void SendAnswer(Answer answer);

Send an answer in unforeseen case.


## FPNN AsyncAnswer

### Constructors

	public static AsyncAnswer Create();

Create an asynchonous handler to do an asynchonous answer return in future.

If the AsyncAnswer instance is created, the `QuestProcessDelegate` **MUST** return `null` for two way quest.

**This static method MUST & ONLY can be called in the `QuestProcessDelegate` function for server pushed quest.**

### Method

	public bool SendAnswer(Answer answer);

Return an answer for two way quest. This method **ONLY** can be called once for an AsyncAnswer instance.

## FPNN AdvanceAnswer

### Method

	public static bool SendAnswer(Answer answer);

Return answer to server before the `QuestProcessDelegate` returned.

If the static method called, the `QuestProcessDelegate` MUST return `null` for two way quest.