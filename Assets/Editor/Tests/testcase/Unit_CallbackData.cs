using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using com.fpnn;

public class Unit_CallbackData {

    [SetUp]
    public void SetUp() {}

    [TearDown]
    public void TearDown() {}

    [Test]
    public void CallbackData_FPData() {

        CallbackData cbd = new CallbackData(new FPData());

        Assert.IsNotNull(cbd.GetData());
        Assert.IsNull(cbd.GetException());
        Assert.IsNull(cbd.GetPayload());
        Assert.AreEqual(0, cbd.GetMid());
    }

    [Test]
    public void CallbackData_Exception() {

        CallbackData cbd = new CallbackData(new Exception("CallbackData_Exception"));

        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetData());
        Assert.IsNull(cbd.GetPayload());
        Assert.AreEqual(0, cbd.GetMid());
    }

    [Test]
    public void CallbackData_Object() {

        CallbackData cbd = new CallbackData(new object());
        
        Assert.IsNotNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetException());
        Assert.IsNull(cbd.GetData());
        Assert.AreEqual(0, cbd.GetMid());
    }

    [Test]
    public void CallbackData_Mid() {

        CallbackData cbd = new CallbackData(new object());

        cbd.SetMid(0);
        Assert.AreEqual(0, cbd.GetMid());

        cbd.SetMid(1567494415);
        Assert.AreEqual(1567494415, cbd.GetMid());

        cbd.SetMid(-1567494415);
        Assert.AreEqual(-1567494415, cbd.GetMid());
    }

    [Test]
    public void CheckException_NullData() {

        CallbackData cbd;


        cbd = new CallbackData(new object());

        cbd.CheckException(true, null);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetData());

        cbd.CheckException(false, null);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetData());


        cbd = new CallbackData(new Exception("CheckException_Exception_NullData"));

        cbd.CheckException(true, null);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetData());

        cbd.CheckException(false, null);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetData());
    }

    [Test]
    public void CheckException_Exception_Data() {

        CallbackData cbd;
        IDictionary<string, object> data;

        data = new Dictionary<string, object>() {

            {"code", 1},
            {"ex", "exception"}
        };

        cbd = new CallbackData(new object());

        cbd.CheckException(true, data);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetData());

        data = new Dictionary<string, object>() {

            {"a", "b"},
            {"c", "d"}
        };

        cbd = new CallbackData(new object());

        cbd.CheckException(true, data);
        Assert.IsNotNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetException());
        Assert.IsNull(cbd.GetData());

        cbd = new CallbackData(new Exception("CheckException_Exception_Data"));

        cbd.CheckException(true, data);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetData());
        Assert.IsNull(cbd.GetPayload());
    }

    [Test]
    public void CheckException_NoException_Data() {

        CallbackData cbd;
        IDictionary<string, object> data;

        data = new Dictionary<string, object>() {

            {"code", 1},
            {"ex", "exception"}
        };

        cbd = new CallbackData(new object());

        cbd.CheckException(false, data);
        Assert.IsNotNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetException());
        Assert.IsNull(cbd.GetData());

        data = new Dictionary<string, object>() {

            {"a", "b"},
            {"c", "d"}
        };

        cbd = new CallbackData(new object());

        cbd.CheckException(false, data);
        Assert.IsNotNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetException());
        Assert.IsNull(cbd.GetData());

        cbd = new CallbackData(new Exception("CheckException_Exception_Data"));

        cbd.CheckException(false, data);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetData());
        Assert.IsNull(cbd.GetPayload());
    }
}
