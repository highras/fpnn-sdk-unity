using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;

using com.fpnn;

public class Integration_FPEvent {

    private FPEvent _event;

    [SetUp]
    public void SetUp() {
        this._event = new FPEvent();
    }

    [TearDown]
    public void TearDown() {
        this._event.RemoveListener();
    }

    [UnityTest]
    public IEnumerator Event_Add_Fire() {
        int count = 0;
        this._event.AddListener("Event_Add_Fire", (evd) => {
            count++;
        });
        this._event.FireEvent(new EventData("Event_Add_Fire"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator Event_Add_Add_Fire() {
        int count = 0;
        this._event.AddListener("Event_Add_Add_Fire", (evd) => {
            count++;
        });
        this._event.AddListener("Event_Add_Add_Fire", (evd) => {
            count++;
        });
        this._event.FireEvent(new EventData("Event_Add_Add_Fire"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Event_Add_Add_Fire_SameEvent() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.AddListener("Event_Add_Add_Fire_SameEvent", lisr);
        this._event.AddListener("Event_Add_Add_Fire_SameEvent", lisr);
        this._event.FireEvent(new EventData("Event_Add_Add_Fire_SameEvent"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator Event_Add_Remove_Fire() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.AddListener("Event_Add_Remove_Fire", (evd) => {
            count++;
        });
        this._event.RemoveListener();
        this._event.FireEvent(new EventData("Event_Add_Remove_Fire"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, count);
        this._event.AddListener("Event_Add_Remove_Fire", (evd) => {
            count++;
        });
        this._event.RemoveListener("Event_Add_Remove_Fire");
        this._event.FireEvent(new EventData("Event_Add_Remove_Fire"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, count);
        this._event.AddListener("Event_Add_Remove_Fire", lisr);
        this._event.RemoveListener("Event_Add_Remove_Fire", lisr);
        this._event.FireEvent(new EventData("Event_Add_Remove_Fire"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, count);
        this._event.AddListener("Event_Add_Remove_Fire", lisr);
        this._event.FireEvent(new EventData("Event_Add_Remove_Fire"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator Event_Add_Fire_Add() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.AddListener("Event_Add_Fire_Add", lisr);
        this._event.FireEvent(new EventData("Event_Add_Fire_Add"));
        this._event.AddListener("Event_Add_Fire_Add", (evd) => {
            count++;
        });
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator Event_Add_Fire_Add_Fire() {
        int count = 0;
        this._event.AddListener("Event_Add_Fire_Add_Fire", (evd) => {
            count++;
        });
        this._event.FireEvent(new EventData("Event_Add_Fire_Add_Fire"));
        this._event.AddListener("Event_Add_Fire_Add_Fire", (evd) => {
            count++;
        });
        this._event.FireEvent(new EventData("Event_Add_Fire_Add_Fire"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(3, count);
    }

    [UnityTest]
    public IEnumerator Event_Add_Fire_Add_Fire_SameEvent() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.AddListener("Event_Add_Fire_Add_Fire_SameEvent", lisr);
        this._event.FireEvent(new EventData("Event_Add_Fire_Add_Fire_SameEvent"));
        this._event.AddListener("Event_Add_Fire_Add_Fire_SameEvent", lisr);
        this._event.FireEvent(new EventData("Event_Add_Fire_Add_Fire_SameEvent"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Event_Add_Fire_Fire() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.AddListener("Event_Add_Fire_Fire", lisr);
        this._event.FireEvent(new EventData("Event_Add_Fire_Fire"));
        this._event.FireEvent(new EventData("Event_Add_Fire_Fire"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Event_Add_Fire_Remove_Remove() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.AddListener("Event_Add_Fire_Remove_Remove", lisr);
        this._event.FireEvent(new EventData("Event_Add_Fire_Remove_Remove"));
        this._event.RemoveListener("Event_Add_Fire_Remove_Remove", lisr);
        this._event.RemoveListener("Event_Add_Fire_Remove_Remove", lisr);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator Event_Add_Fire_Delay_Remove_Remove() {
        int count = 0;
        EventDelegate lisr = (evd) => {
            count++;
        };
        this._event.AddListener("Event_Add_Fire_Delay_Remove_Remove", lisr);
        this._event.FireEvent(new EventData("Event_Add_Fire_Delay_Remove_Remove"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
        this._event.RemoveListener("Event_Add_Fire_Delay_Remove_Remove", lisr);
        this._event.RemoveListener("Event_Add_Fire_Delay_Remove_Remove", lisr);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
    }
}
