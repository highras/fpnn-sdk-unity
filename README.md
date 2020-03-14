# FPNN Unity SDK

[TOC]

## Depends

* [msgpack-csharp](https://github.com/highras/msgpack-csharp)

### Compatibility Version:

C# .Net Standard 2.0

### Capability in Funture

Encryption Capability.

## Usage

### Using package

	using com.fpnn;
	using com.fpnn.proto;

## API docs

### Init (REQUIRED)

**Init MUST in the main thread.**

	using com.fpnn;
	ClientEngine.Init();
	ClientEngine.Init(Config config);

### Create

	TCPClient client = new TCPClient(string host, int port, bool autoConnect = true);
	TCPClient client = TCPClient.Create(string host, int port, bool autoConnect = true);
	TCPClient client = TCPClient.Create(string endpoint, bool autoConnect = true);

**endpoint** format: `"hostname/ip" + ":" + "port"`.  
e.g. `"localhost:8000"`

### Configure (Optional)

#### Set Duplex Mode (Server Push)

		client.SetQuestProcessor(IQuestProcessor processor);

#### Set connection events' callbacks

		client.SetConnectionConnectedDelegate(ConnectionConnectedDelegate ccd);
		client.SetConnectionCloseDelegate(ConnectionCloseDelegate cwcd);

### Send Quest

	//-- Sync method
	Answer answer = client.SendQuest(Quest quest, int timeout = 0);

	//-- Async methods
	bool status = client.SendQuest(Quest quest, AnswerDelegate callback, int timeout = 0);
	bool status = client.SendQuest(Quest quest, IAnswerCallback callback, int timeout = 0);


### Close (Optional)

	client.Close();


### SDK Version

	Debug.Log("com.fpnn.Config.Version");

## API docs

Please refer: [API docs](API.md)


## Directory structure

* **\<fpnn-sdk-unity\>/Assets/Plugins/fpnn**

	Codes of SDK.

* **\<fpnn-sdk-unity\>/Assets/Examples**

	+ **Demo.cs**:

		Basic usagefor this SDK.

	More examples and senior examples can be found at [Examples for fpnn-sdk-csharp](https://github.com/highras/fpnn-sdk-csharp/tree/master/examples).

* **\<fpnn-sdk-unity\>/Assets/Tests**

	+ **asyncStressClient.cs**

		Stress & Concurrent testing codes for SDK.  
		Testing server is <fpnn>/core/test/serverTest. Refer: [Cpp codes of serverTest](https://github.com/highras/fpnn/blob/master/core/test/serverTest.cpp)

	More tests can be found at [Tests for fpnn-sdk-csharp](https://github.com/highras/fpnn-sdk-csharp/tree/master/tests).