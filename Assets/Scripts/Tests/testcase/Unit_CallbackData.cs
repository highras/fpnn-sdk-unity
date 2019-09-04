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
    public void CallbackData_Data() {

        CallbackData cbd = new CallbackData(new FPData());
        Assert.IsNotNull(cbd.GetData());
    }

    [Test]
    public void CallbackData_Exception() {

        CallbackData cbd = new CallbackData(new Exception());
        Assert.IsNotNull(cbd.GetException());
    }

    [Test]
    public void CallbackData_Payload() {

        CallbackData cbd = new CallbackData(new object());
        Assert.IsNotNull(cbd.GetPayload());
    }

    [Test]
    public void CallbackData_SimpleMid() {

        CallbackData cbd = new CallbackData(new object());

        cbd.SetMid(1567494415);
        Assert.AreEqual(1567494415, cbd.GetMid());
    }

    [Test]
    public void CallbackData_ZeroMid() {

        CallbackData cbd = new CallbackData(new object());

        cbd.SetMid(0);
        Assert.AreEqual(0, cbd.GetMid());
    }

    [Test]
    public void CallbackData_NegativeMid() {

        CallbackData cbd = new CallbackData(new object());

        cbd.SetMid(-1567494415);
        Assert.AreEqual(-1567494415, cbd.GetMid());
    }

    [Test]
    public void CheckException_Payload_NullData() {

        CallbackData cbd = new CallbackData(new object());

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
    public void CheckException_Exception_NullData() {

        CallbackData cbd = new CallbackData(new Exception());

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
    public void CheckException_Payload_AnswerEx() {

        IDictionary<string, object> data;
        CallbackData cbd = new CallbackData(new object());

        data = new Dictionary<string, object>() {

            {"code", 1},
            {"ex", "exception"}
        };

        cbd.CheckException(true, data);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetData());

        data = new Dictionary<string, object>() {

            {"a", "b"},
            {"c", "d"}
        };

        cbd.CheckException(true, data);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetData());
        Assert.IsNull(cbd.GetPayload());
    }

    [Test]
    public void CheckException_Exception_AnswerEx() {

        IDictionary<string, object> data;
        CallbackData cbd = new CallbackData(new Exception());

        data = new Dictionary<string, object>() {

            {"code", 1},
            {"ex", "exception"}
        };

        cbd.CheckException(true, data);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetData());

        data = new Dictionary<string, object>() {

            {"a", "b"},
            {"c", "d"}
        };

        cbd.CheckException(true, data);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetData());
        Assert.IsNull(cbd.GetPayload());
    }

    [Test]
    public void CheckException_Payload_NoAnswerEx() {

        IDictionary<string, object> data;
        CallbackData cbd = new CallbackData(new object());

        data = new Dictionary<string, object>() {

            {"code", 1},
            {"ex", "exception"}
        };

        cbd.CheckException(false, data);
        Assert.IsNotNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetException());
        Assert.IsNull(cbd.GetData());

        data = new Dictionary<string, object>() {

            {"a", "b"},
            {"c", "d"}
        };

        cbd.CheckException(false, data);
        Assert.IsNotNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetException());
        Assert.IsNull(cbd.GetData());
    }

    [Test]
    public void CheckException_Exception_NoAnswerEx() {

        IDictionary<string, object> data;
        CallbackData cbd = new CallbackData(new Exception());

        data = new Dictionary<string, object>() {

            {"code", 1},
            {"ex", "exception"}
        };

        cbd.CheckException(false, data);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetData());

        data = new Dictionary<string, object>() {

            {"a", "b"},
            {"c", "d"}
        };

        cbd.CheckException(false, data);
        Assert.IsNotNull(cbd.GetException());
        Assert.IsNull(cbd.GetPayload());
        Assert.IsNull(cbd.GetData());
    }
}
