using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Net.Sockets;
using System.Collections;

using com.fpnn;

public class Unit_FPProcessor {

    private class TestProcessor: FPProcessor.IProcessor {

        public int ServiceCount {
            get;
            set;
        }
        public int HasPushCount {
            get;
            set;
        }
        public int SecondCount {
            get;
            set;
        }

        public void Service(FPData data, AnswerDelegate answer) {
            ServiceCount++;
        }

        public bool HasPushService(string name) {
            HasPushCount++;
            return false;
        }

        public void OnSecond(long timestamp) {
            SecondCount++;
        }
    }

    private FPProcessor _psr;

    [SetUp]
    public void SetUp() {
        this._psr = new FPProcessor();
    }

    [TearDown]
    public void TearDown() {
        this._psr.Destroy();
    }


    /**
     *  SetProcessor(IProcessor processor)
     */
    [Test]
    public void Processor_SetProcessor_NullProcessor() {
        int count = 0;
        this._psr.SetProcessor(null);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Processor_SetProcessor_SimpleProcessor() {
        int count = 0;
        this._psr.SetProcessor(new TestProcessor());
        Assert.AreEqual(0, count);
    }


    /**
     *  Service(FPData data, AnswerDelegate answer)
     */
    [Test]
    public void Processor_Service_NullData() {
        int count = 0;
        this._psr.Service(null, (payload, exception) => {
            count++;
        });
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Processor_Service_EmptyData() {
        int count = 0;
        this._psr.Service(new FPData(), (payload, exception) => {
            count++;
        });
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Processor_Service_NullDelegate() {
        int count = 0;
        FPData data = new FPData();
        data.SetMethod("Processor_Service_NullDelegate");
        this._psr.Service(data, null);
        Assert.AreEqual(0, count);
    }


    /**
     *  OnSecond(long timestamp)
     */
    [Test]
    public void Processor_OnSecond_ZeroTimestamp() {
        int count = 0;
        this._psr.OnSecond(0);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Processor_OnSecond_NegativeTimestamp() {
        int count = 0;
        this._psr.OnSecond(-1567849836);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Processor_OnSecond_SimpleTimestamp() {
        int count = 0;
        this._psr.OnSecond(1567849836);
        Assert.AreEqual(0, count);
    }
}
