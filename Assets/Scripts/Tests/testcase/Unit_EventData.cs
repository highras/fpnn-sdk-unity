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


    /**
     *  EventData(string type)
     */
    [Test]
    public void EventData_SimpleType() {
        EventData evd = new EventData("EventData_SimpleType");
        Assert.AreEqual("EventData_SimpleType", evd.GetEventType());
    }

    [Test]
    public void EventData_EmptyType() {
        EventData evd = new EventData("");
        Assert.AreEqual("", evd.GetEventType());
    }

    [Test]
    public void EventData_NullType() {
        EventData evd = new EventData(null);
        Assert.IsNull(evd.GetEventType());
    }


    /**
     *  EventData(string type, FPData data)
     */
    [Test]
    public void EventData_SimpleType_Data() {
        EventData evd = new EventData("EventData_SimpleType_Data", new FPData());
        Assert.AreEqual("EventData_SimpleType_Data", evd.GetEventType());
        Assert.IsNotNull(evd.GetData());
    }

    [Test]
    public void EventData_EmptyType_Data() {
        EventData evd = new EventData("", new FPData());
        Assert.AreEqual("", evd.GetEventType());
        Assert.IsNotNull(evd.GetData());
    }

    [Test]
    public void EventData_NullType_Data() {
        EventData evd = new EventData(null, new FPData());
        Assert.IsNull(evd.GetEventType());
        Assert.IsNotNull(evd.GetData());
    }


    /**
     *  EventData(string type, Exception ex)
     */
    [Test]
    public void EventData_SimpleType_Exception() {
        EventData evd = new EventData("EventData_SimpleType_Exception", new Exception());
        Assert.AreEqual("EventData_SimpleType_Exception", evd.GetEventType());
        Assert.IsNotNull(evd.GetException());
    }

    [Test]
    public void EventData_EmptyType_Exception() {
        EventData evd = new EventData("", new Exception());
        Assert.AreEqual("", evd.GetEventType());
        Assert.IsNotNull(evd.GetException());
    }

    [Test]
    public void EventData_NullType_Exception() {
        EventData evd = new EventData(null, new Exception());
        Assert.IsNull(evd.GetEventType());
        Assert.IsNotNull(evd.GetException());
    }


    /**
     *  EventData(string type, long timestamp)
     */
    [Test]
    public void EventData_SimpleType_Timestamp() {
        EventData evd = new EventData("EventData_SimpleType_Timestamp", 1567501679);
        Assert.AreEqual("EventData_SimpleType_Timestamp", evd.GetEventType());
        Assert.AreEqual(1567501679, evd.GetTimestamp());
    }

    [Test]
    public void EventData_EmptyType_Timestamp() {
        EventData evd = new EventData("", 1567501679);
        Assert.AreEqual("", evd.GetEventType());
        Assert.AreEqual(1567501679, evd.GetTimestamp());
    }

    [Test]
    public void EventData_NullType_Timestamp() {
        EventData evd = new EventData(null, 1567501679);
        Assert.IsNull(evd.GetEventType());
        Assert.AreEqual(1567501679, evd.GetTimestamp());
    }

    [Test]
    public void EventData_SimpleType_ZeroTimestamp() {
        EventData evd = new EventData("EventData_SimpleType_ZeroTimestamp", 0);
        Assert.AreEqual("EventData_SimpleType_ZeroTimestamp", evd.GetEventType());
        Assert.AreEqual(0, evd.GetTimestamp());
    }

    [Test]
    public void EventData_SimpleType_NegativeTimestamp() {
        EventData evd = new EventData("EventData_SimpleType_NegativeTimestamp", -1567501679);
        Assert.AreEqual("EventData_SimpleType_NegativeTimestamp", evd.GetEventType());
        Assert.AreEqual(-1567501679, evd.GetTimestamp());
    }


    /**
     *  EventData(string type, Object payload)
     */
    [Test]
    public void EventData_SimpleType_Payload() {
        EventData evd = new EventData("EventData_SimpleType_Payload", new object());
        Assert.AreEqual("EventData_SimpleType_Payload", evd.GetEventType());
        Assert.IsNotNull(evd.GetPayload());
    }

    [Test]
    public void EventData_EmptyType_Payload() {
        EventData evd = new EventData("", new object());
        Assert.AreEqual("", evd.GetEventType());
        Assert.IsNotNull(evd.GetPayload());
    }

    [Test]
    public void EventData_NullType_Payload() {
        EventData evd = new EventData(null, new object());
        Assert.IsNull(evd.GetEventType());
        Assert.IsNotNull(evd.GetPayload());
    }


    /**
     *  EventData(string type, bool retry)
     */
    [Test]
    public void EventData_SimpleType_Retry() {
        EventData evd = new EventData("EventData_SimpleType_Retry", true);
        Assert.AreEqual("EventData_SimpleType_Retry", evd.GetEventType());
        Assert.IsTrue(evd.HasRetry());
    }

    [Test]
    public void EventData_EmptyType_Retry() {
        EventData evd = new EventData("", true);
        Assert.AreEqual("", evd.GetEventType());
        Assert.IsTrue(evd.HasRetry());
    }

    [Test]
    public void EventData_NullType_Retry() {
        EventData evd = new EventData(null, true);
        Assert.IsNull(evd.GetEventType());
        Assert.IsTrue(evd.HasRetry());
    }
}
