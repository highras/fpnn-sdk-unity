using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;

using com.fpnn;

public class Integration_FPManager {

    [SetUp]
    public void SetUp() {

        FPManager.Instance.Init();
    }

    [TearDown]
    public void TearDown() {}


    /**
     *  SecondTimer
     */
    [UnityTest]
    public IEnumerator Manager_Add_Delay() {

        int count = 0;
        FPManager.Instance.AddSecond((evd) => {
            count++;
        });
        yield return new WaitForSeconds(1.1f);
        Assert.AreNotEqual(0, count);
        FPManager.Instance.StopTimerThread();
    }

    [UnityTest]
    public IEnumerator Manager_Add_NullCallback_Delay() {

        int count = 0;
        FPManager.Instance.AddSecond(null);
        yield return new WaitForSeconds(1.1f);
        Assert.AreEqual(0, count);

        yield return new WaitForSeconds(1.1f);
        Assert.AreEqual(0, count);
        FPManager.Instance.StopTimerThread();
    }

    [UnityTest]
    public IEnumerator Manager_Add_Remove_Delay() {

        int count = 0;
        EventDelegate callback = (evd) => {
            count++;
        };

        FPManager.Instance.AddSecond(callback);
        FPManager.Instance.RemoveSecond(callback);
        yield return new WaitForSeconds(1.1f);
        Assert.AreEqual(0, count);

        yield return new WaitForSeconds(1.1f);
        Assert.AreEqual(0, count);
        FPManager.Instance.StopTimerThread();
    }

    [UnityTest]
    public IEnumerator Manager_Add_Delay_Remove_Delay() {

        int count = 0;
        EventDelegate callback = (evd) => {
            count++;
        };

        FPManager.Instance.AddSecond(callback);
        yield return new WaitForSeconds(1.1f);
        FPManager.Instance.RemoveSecond(callback);
        Assert.AreNotEqual(0, count);

        yield return new WaitForSeconds(1.1f);
        Assert.AreEqual(1, count);
        FPManager.Instance.StopTimerThread();
    }

    [UnityTest]
    public IEnumerator Manager_Add60_Delay() {

        int count = 0;
        for (int i = 0; i < 60; i++) {
            FPManager.Instance.AddSecond((evd) => {
                count++;
            });
        }

        yield return new WaitForSeconds(1.1f);
        Assert.AreEqual(50, count);

        yield return new WaitForSeconds(1.1f);
        Assert.AreEqual(100, count);
        FPManager.Instance.StopTimerThread();
    }


    /**
     *  Service
     */
    [UnityTest]
    public IEnumerator Manager_EventTask_Delay() {

        int count = 0;
        FPManager.Instance.EventTask((evd) => {
            count++;
        }, new EventData("Manager_EventTask_Delay"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator Manager_EventTask_NullTask_Delay() {

        int count = 0;
        FPManager.Instance.EventTask(null, new EventData("Manager_EventTask_NullTask_Delay"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, count);
    }

    [UnityTest]
    public IEnumerator Manager_EventTask_NullData_Delay() {

        int count = 0;
        FPManager.Instance.EventTask((evd) => {
            count++;
        }, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator Manager_EventTask_EventTask_Delay() {

        int count = 0;
        FPManager.Instance.EventTask((evd) => {
            count++;
        }, new EventData("Manager_EventTask_EventTask_Delay"));
        FPManager.Instance.EventTask((evd) => {
            count++;
        }, new EventData("Manager_EventTask_EventTask_Delay"));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Manager_CallbackTask_Delay() {

        int count = 0;
        FPManager.Instance.CallbackTask((cbd) => {
            count++;
        }, new CallbackData(new FPData()));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator Manager_CallbackTask_NullTask_Delay() {

        int count = 0;
        FPManager.Instance.CallbackTask(null, new CallbackData(new FPData()));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, count);
    }

    [UnityTest]
    public IEnumerator Manager_CallbackTask_NullData_Delay() {

        int count = 0;
        FPManager.Instance.CallbackTask((cbd) => {
            count++;
        }, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator Manager_CallbackTask_CallbackTask_Delay() {

        int count = 0;
        FPManager.Instance.CallbackTask((cbd) => {
            count++;
        }, new CallbackData(new FPData()));
        FPManager.Instance.CallbackTask((cbd) => {
            count++;
        }, new CallbackData(new FPData()));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Manager_EventTask_CallbackTask_Delay() {

        int count = 0;
        FPManager.Instance.EventTask((evd) => {
            count++;
        }, new EventData("Manager_EventTask_CallbackTask_Delay"));
        FPManager.Instance.CallbackTask((cbd) => {
            count++;
        }, new CallbackData(new FPData()));
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Manager_ExecTask_Delay() {

        int count = 0;
        FPManager.Instance.ExecTask((state) => {
            count++;
        }, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator Manager_ExecTask_NullTask_Delay() {

        int count = 0;
        FPManager.Instance.ExecTask(null, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, count);
    }

    [UnityTest]
    public IEnumerator Manager_ExecTask_ExecTask_Delay() {

        int count = 0;
        FPManager.Instance.ExecTask((state) => {
            count++;
        }, null);
        FPManager.Instance.ExecTask((state) => {
            count++;
        }, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Manager_EventTask_CallbackTask_ExecTask_Delay() {

        int count = 0;
        FPManager.Instance.EventTask((evd) => {
            count++;
        }, new EventData("Manager_EventTask_CallbackTask_ExecTask_Delay"));
        FPManager.Instance.CallbackTask((cbd) => {
            count++;
        }, new CallbackData(new FPData()));
        FPManager.Instance.ExecTask((state) => {
            count++;
        }, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(3, count);
    }

    [UnityTest]
    public IEnumerator Manager_DelayTask_Delay() {

        int count = 0;
        FPManager.Instance.DelayTask(100, (state) => {
            count++;
        }, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator Manager_DelayTask_NullTask_Delay() {

        int count = 0;
        FPManager.Instance.DelayTask(100, null, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, count);
    }

    [UnityTest]
    public IEnumerator Manager_DelayTask_DelayTask_Delay() {

        int count = 0;
        FPManager.Instance.DelayTask(100, (state) => {
            count++;
        }, null);
        FPManager.Instance.DelayTask(700, (state) => {
            count++;
        }, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);

        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Manager_ExecTask_DelayTask_Delay() {

        int count = 0;
        FPManager.Instance.ExecTask((state) => {
            count++;
        }, null);
        FPManager.Instance.DelayTask(800, (state) => {
            count++;
        }, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);

        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Manager_ExecTask_Call_ExecTask_Delay() {

        int count = 0;
        FPManager.Instance.ExecTask((state) => {
            count++;
            FPManager.Instance.ExecTask((st) => {
                count++;
            }, null);
        }, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Manager_ExecTask_Call_DelayTask_Delay() {

        int count = 0;
        FPManager.Instance.ExecTask((state) => {
            count++;
            FPManager.Instance.DelayTask(800, (st) => {
                count++;
            }, null);
        }, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);

        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Manager_DelayTask_Call_ExecTask_Delay() {

        int count = 0;
        FPManager.Instance.DelayTask(300, (state) => {
            count++;
            FPManager.Instance.ExecTask((st) => {
                count++;
            }, null);
        }, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Manager_DelayTask_Call_DelayTask_Delay() {

        int count = 0;
        FPManager.Instance.DelayTask(300, (state) => {
            count++;
            FPManager.Instance.DelayTask(400, (st) => {
                count++;
            }, null);
        }, null);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, count);

        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator Manager_ExecTask4000_Delay() {

        int count = 0;
        for (int i = 0; i < 4000; i++) {
            FPManager.Instance.ExecTask((state) => {
                count++;
            }, null);
        }
        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(4000, count);
    }

    [UnityTest]
    public IEnumerator Manager_DelayTask4000_Delay() {

        int count = 0;
        for (int i = 0; i < 4000; i++) {
            FPManager.Instance.DelayTask(500, (state) => {
                count++;
            }, null);
        }
        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(3000, count);
    }
}
