using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;

using com.fpnn;

public class Unit_FPEvent {

    private FPEvent _event;

    [SetUp]
    public void SetUp() {
        this._event = new FPEvent();
    }

    [TearDown]
    public void TearDown() {
        this._event.RemoveListener();
    }


    /**
     *  AddListener
     */
    [Test]
    public void Event_AddListener_EmptyType() {
        int count = 0;
        this._event.AddListener("", (evd) => {
            count++;
        });
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_AddListener_NullType() {
        int count = 0;
        this._event.AddListener(null, (evd) => {
            count++;
        });
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_AddListener_SimpleType() {
        int count = 0;
        this._event.AddListener("AddListener_SimpleType", (evd) => {
            count++;
        });
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_AddListener_SameType() {
        int count = 0;
        this._event.AddListener("AddListener_SameType", (evd) => {
            count++;
        });
        this._event.AddListener("AddListener_SameType", (evd) => {
            count++;
        });
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_AddListener_SameEvent() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.AddListener("AddListener_AnotherType", lisr);
        this._event.AddListener("AddListener_AnotherType", lisr);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_AddListener_NullEvent() {
        int count = 0;
        this._event.AddListener("AddListener_NullEvent", null);
        Assert.AreEqual(0, count);
    }

    /**
     *  RemoveListener
     */
    [Test]
    public void Event_RemoveListener_SimpleCall() {
        int count = 0;
        this._event.RemoveListener();
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_RemoveListener_EmptyType() {
        int count = 0;
        this._event.RemoveListener("");
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_RemoveListener_NullType() {
        int count = 0;
        this._event.RemoveListener(null);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_RemoveListener_SimpleType() {
        int count = 0;
        this._event.RemoveListener("RemoveListener_SimpleType");
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_RemoveListener_SameType() {
        int count = 0;
        this._event.RemoveListener("RemoveListener_SameType");
        this._event.RemoveListener("RemoveListener_SameType");
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_RemoveListener_EmptyType_Event() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.RemoveListener("", lisr);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_RemoveListener_NullType_Event() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.RemoveListener(null, lisr);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_RemoveListener_SimpleType_Event() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.RemoveListener("RemoveListener_SimpleType_Event", lisr);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_RemoveListener_SameType_Event() {
        int count = 0;
        this._event.RemoveListener("RemoveListener_SameType_Event", (evd) => {
            count++;
        });
        this._event.RemoveListener("RemoveListener_SameType_Event", (evd) => {
            count++;
        });
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_RemoveListener_Type_NullEvent() {
        int count = 0;
        this._event.RemoveListener("RemoveListener_Type_NullEvent", null);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_RemoveListener_Type_SimpleEvent() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.RemoveListener("RemoveListener_Type_SimpleEvent", lisr);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_RemoveListener_Type_SameEvent() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.RemoveListener("RemoveListener_Type_SameEvent", lisr);
        this._event.RemoveListener("RemoveListener_Type_SameEvent", lisr);
        Assert.AreEqual(0, count);
    }


    /**
     *  FireEvent
     */
    [Test]
    public void Event_FireEvent_SimpleCall() {
        int count = 0;
        this._event.FireEvent(new EventData("FireEvent_SimpleCall"));
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_FireEvent_NullEventData() {
        int count = 0;
        this._event.FireEvent(null);
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_FireEvent_NullEventType() {
        int count = 0;
        this._event.FireEvent(new EventData(null));
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_FireEvent_EmptyEventType() {
        int count = 0;
        this._event.FireEvent(new EventData(""));
        Assert.AreEqual(0, count);
    }

    [Test]
    public void Event_FireEvent_SameEventData() {
        int count = 0;
        EventData evd = new EventData("FireEvent_SameEventData");
        this._event.FireEvent(evd);
        this._event.FireEvent(evd);
        Assert.AreEqual(0, count);
    }
}
