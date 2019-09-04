using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using com.fpnn;

public class Unit_EventData {

    [SetUp]
    public void SetUp() {}

    [TearDown]
    public void TearDown() {}

    [Test]
    public void EventData_Type() {

        EventData evd = new EventData("EventData_Type");

        Assert.AreEqual("EventData_Type", evd.GetEventType());
        Assert.IsNull(evd.GetData());
        Assert.IsNull(evd.GetException());
        Assert.IsNull(evd.GetPayload());
        Assert.AreEqual(0, evd.GetTimestamp());
        Assert.IsFalse(evd.HasRetry());
    }

    [Test]
    public void EventData_Type_Data() {

        EventData evd = new EventData("EventData_Type_Data", new FPData());

        Assert.AreEqual("EventData_Type_Data", evd.GetEventType());
        Assert.IsNotNull(evd.GetData());
        Assert.IsNull(evd.GetException());
        Assert.IsNull(evd.GetPayload());
        Assert.AreEqual(0, evd.GetTimestamp());
        Assert.IsFalse(evd.HasRetry());
    }

    [Test]
    public void EventData_Type_Exception() {

        EventData evd = new EventData("EventData_Type_Exception", new Exception());

        Assert.AreEqual("EventData_Type_Exception", evd.GetEventType());
        Assert.IsNull(evd.GetData());
        Assert.IsNotNull(evd.GetException());
        Assert.IsNull(evd.GetPayload());
        Assert.AreEqual(0, evd.GetTimestamp());
        Assert.IsFalse(evd.HasRetry());
    }

    [Test]
    public void EventData_Type_Timestamp() {

        EventData evd = new EventData("EventData_Type_Timestamp", 1567501679);

        Assert.AreEqual("EventData_Type_Timestamp", evd.GetEventType());
        Assert.IsNull(evd.GetData());
        Assert.IsNull(evd.GetException());
        Assert.IsNull(evd.GetPayload());
        Assert.AreEqual(1567501679, evd.GetTimestamp());
        Assert.IsFalse(evd.HasRetry());
    }

    [Test]
    public void EventData_Type_Payload() {

        EventData evd = new EventData("EventData_Type_Payload", new object());

        Assert.AreEqual("EventData_Type_Payload", evd.GetEventType());
        Assert.IsNull(evd.GetData());
        Assert.IsNull(evd.GetException());
        Assert.IsNotNull(evd.GetPayload());
        Assert.AreEqual(0, evd.GetTimestamp());
        Assert.IsFalse(evd.HasRetry());
    }

    [Test]
    public void EventData_Type_Retry() {

        EventData evd = new EventData("EventData_Type_Retry", true);

        Assert.AreEqual("EventData_Type_Retry", evd.GetEventType());
        Assert.IsNull(evd.GetData());
        Assert.IsNull(evd.GetException());
        Assert.IsNull(evd.GetPayload());
        Assert.AreEqual(0, evd.GetTimestamp());
        Assert.IsTrue(evd.HasRetry());
    }
}
