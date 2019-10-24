using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;

using com.fpnn;

public class Unit_FPManager {

    [SetUp]
    public void SetUp() {}

    [TearDown]
    public void TearDown() {}


    /**
     *  Instance
     */
    [Test]
    public void Manager_Instance() {
        Assert.AreEqual(FPManager.Instance, FPManager.Instance);
    }


    /**
     *  Init()
     */
    [Test]
    public void Manager_Init() {
        int count = 0;
        FPManager.Instance.Init();
        Assert.AreEqual(0, count);
    }


    /**
     *  AddSecond(EventDelegate callback)
     */
    [Test]
    public void Manager_AddSecond_NullDelegate() {
        int count = 0;
        FPManager.Instance.AddSecond(null);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_AddSecond_SimpleDelegate() {
        int count = 0;
        FPManager.Instance.AddSecond((evd) => {
            count++;
        });
        Assert.AreEqual(0, count);
    }


    /**
     *  RemoveSecond(EventDelegate callback)
     */
    [Test]
    public void Manager_RemoveSecond_NullDelegate() {
        int count = 0;
        FPManager.Instance.RemoveSecond(null);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_RemoveSecond_SimpleDelegate() {
        int count = 0;
        FPManager.Instance.RemoveSecond((evd) => {
            count++;
        });
        Assert.AreEqual(0, count);
    }


    /**
     *  StartTimerThread()
     */
    [Test]
    public void Manager_StartTimerThread() {
        int count = 0;
        FPManager.Instance.StartTimerThread();
        Assert.AreEqual(0, count);
    }


    /**
     *  StopTimerThread()
     */
    [Test]
    public void Manager_StopTimerThread() {
        int count = 0;
        FPManager.Instance.StopTimerThread();
        Assert.AreEqual(0, count);
    }


    /**
     *  EventTask(EventDelegate callback, EventData evd)
     */
    [Test]
    public void Manager_EventTask_NullDelegate() {
        int count = 0;
        FPManager.Instance.EventTask(null, new EventData("Manager_EventTask_NullDelegate"));
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_EventTask_SimpleDelegate() {
        int count = 0;
        FPManager.Instance.EventTask((evd) => {
            count++;
        }, new EventData("Manager_EventTask_SimpleDelegate"));
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_EventTask_NullData() {
        int count = 0;
        FPManager.Instance.EventTask((evd) => {
            count++;
        }, null);
        Assert.AreEqual(0, count);
    }


    /**
     *  CallbackTask(CallbackDelegate callback, CallbackData cbd)
     */
    [Test]
    public void Manager_CallbackTask_NullDelegate() {
        int count = 0;
        FPManager.Instance.CallbackTask(null, new CallbackData(new FPData()));
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_CallbackTask_SimpleDelegate() {
        int count = 0;
        FPManager.Instance.CallbackTask((cbd) => {
            count++;
        }, new CallbackData(new FPData()));
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_CallbackTask_NullData() {
        int count = 0;
        FPManager.Instance.CallbackTask((cbd) => {
            count++;
        }, null);
        Assert.AreEqual(0, count);
    }


    /**
     *  AsyncTask(Action<object> taskAction, object state)
     */
    [Test]
    public void Manager_AsyncTask_NullAction() {
        int count = 0;
        FPManager.Instance.AsyncTask(null, new object());
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_AsyncTask_SimpleAction() {
        int count = 0;
        FPManager.Instance.AsyncTask((state) => {
            count++;
        }, new object());
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_AsyncTask_NullState() {
        int count = 0;
        FPManager.Instance.AsyncTask((state) => {
            count++;
        }, null);
        Assert.AreEqual(0, count);
    }


    /**
     *  DelayTask(int milliSecond, Action<object> taskAction, object state)
     */
    [Test]
    public void Manager_DelayTask_NullAction() {
        int count = 0;
        FPManager.Instance.DelayTask(1, null, new object());
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_DelayTask_SimpleAction() {
        int count = 0;
        FPManager.Instance.DelayTask(1, (state) => {
            count++;
        }, new object());
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_DelayTask_NullState() {
        int count = 0;
        FPManager.Instance.DelayTask(1, (state) => {
            count++;
        }, null);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_DelayTask_ZeroDelay() {
        int count = 0;
        FPManager.Instance.DelayTask(1, (state) => {
            count++;
        }, new object());
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_DelayTask_NegativeDelay() {
        int count = 0;
        FPManager.Instance.DelayTask(-100, (state) => {
            count++;
        }, new object());
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Manager_DelayTask_SimpleDelay() {
        int count = 0;
        FPManager.Instance.DelayTask(200, (state) => {
            count++;
        }, new object());
        Assert.AreEqual(0, count);
    }


    /**
     *  GetMilliTimestamp()
     */
    [Test]
    public void Manager_GetMilliTimestamp() {
        Assert.AreNotEqual(0, FPManager.Instance.GetMilliTimestamp());
    }


    /**
     *  GetTimestamp()
     */
    [Test]
    public void Manager_GetTimestamp() {
        Assert.AreNotEqual(0, FPManager.Instance.GetTimestamp());
    }
}
