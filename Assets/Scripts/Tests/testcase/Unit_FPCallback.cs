using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;

using com.fpnn;

public class Unit_FPCallback {

    private FPCallback _callback;

    [SetUp]
    public void SetUp() {

        FPManager.Instance.Init();
        this._callback = new FPCallback();
    }

    [TearDown]
    public void TearDown() {

        this._callback.RemoveCallback();
    }


    /**
     *  AddCallback
     */
    [Test]
    public void AddCallback_EmptyKey() {

        int count = 0;

        this._callback.AddCallback("", (cbd) => {

            count++;
        }, 1 * 1000);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void AddCallback_NullKey() {

        int count = 0;

        this._callback.AddCallback(null, (cbd) => {

            count++;
        }, 1 * 1000);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void AddCallback_SimpleKey() {

        int count = 0;
        this._callback.AddCallback("AddCallback_SimpleKey", (cbd) => {

            count++;
        }, 1 * 1000);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void AddCallback_SameCallback() {

        int count = 0;
        CallbackDelegate callback = (cbd) => {

            count++;
        };

        this._callback.AddCallback("AddCallback_SameCallback_1", callback, 1 * 1000);
        this._callback.AddCallback("AddCallback_SameCallback_2", callback, 1 * 1000);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void AddCallback_SameKey() {

        int count = 0;
        this._callback.AddCallback("AddCallback_SameKey", (cbd) => {

            count++;
        }, 1 * 1000);
        this._callback.AddCallback("AddCallback_SameKey", (cbd) => {

            count++;
        }, 1 * 1000);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void AddCallback_NullCallback() {

        int count = 0;
        this._callback.AddCallback("AddCallback_NullCallback", null, 1 * 1000);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void AddCallback_ZeroTimeout() {

        int count = 0;
        this._callback.AddCallback("AddCallback_ZeroTimeout", (cbd) => {

            count++;
        }, 0);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void AddCallback_NegativeTimeout() {

        int count = 0;
        this._callback.AddCallback("AddCallback_NegativeTimeout", (cbd) => {

            count++;
        }, -1);
        Assert.AreEqual(0, count);
    }


    /**
     *  ExecFPData
     */
    [Test]
    public void ExecFPData_EmptyKey() {

        int count = 0;
        this._callback.ExecCallback("", new FPData());
        Assert.AreEqual(0, count);
    }

    [Test]
    public void ExecFPData_NullKey() {

        int count = 0;
        this._callback.ExecCallback(null, new FPData());
        Assert.AreEqual(0, count);
    }

    [Test]
    public void ExecFPData_SimpleKey() {

        int count = 0;
        this._callback.ExecCallback("ExecFPData_SimpleKey", new FPData());
        Assert.AreEqual(0, count);
    }


    /**
     *  ExecException
     */
    [Test]
    public void ExecException_EmptyKey() {

        int count = 0;
        this._callback.ExecCallback("", new Exception());
        Assert.AreEqual(0, count);
    }

    [Test]
    public void ExecException_NullKey() {

        int count = 0;
        this._callback.ExecCallback(null, new Exception());
        Assert.AreEqual(0, count);
    }

    [Test]
    public void ExecException_SimpleKey() {

        int count = 0;
        this._callback.ExecCallback("SimpleKey", new Exception());
        Assert.AreEqual(0, count);
    }


    /**
     *  RemoveCallback
     */
    [Test]
    public void RemoveCallback_SimpleCall() {

        int count = 0;
        this._callback.RemoveCallback();
        Assert.AreEqual(0, count);
    }


    /**
     *  OnSecond
     */
    [Test]
    public void OnSecond_SimpleTimestamp() {

        int count = 0;
        this._callback.OnSecond(1567485878);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void OnSecond_ZeroTimestamp() {

        int count = 0;
        this._callback.OnSecond(0);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void OnSecond_NegativeTimestamp() {

        int count = 0;
        this._callback.OnSecond(-1567485878);
        Assert.AreEqual(0, count);
    }
}
