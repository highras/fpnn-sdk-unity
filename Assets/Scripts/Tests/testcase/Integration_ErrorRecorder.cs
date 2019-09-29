using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using com.fpnn;

public class Integration_ErrorRecorder {

    private class TestErrorRecorder: ErrorRecorder {

        public int Count {
            get;
            set;
        }

        public override void recordError(Exception e) {
            Count++;
        }
    }

    private TestErrorRecorder _recorder;

    [SetUp]
    public void SetUp() {
        this._recorder = new TestErrorRecorder();
    }

    [TearDown]
    public void TearDown() {
        this._recorder.Count = 0;
    }

    [UnityTest]
    public IEnumerator Error_Record_Default() {
        int count = 0;
        ErrorRecorderHolder.recordError(new Exception());
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, count);
    }

    [UnityTest]
    public IEnumerator Error_Record_Set() {
        this._recorder.Count = 0;
        ErrorRecorderHolder.recordError(new Exception());
        ErrorRecorderHolder.setInstance(this._recorder);
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(0, this._recorder.Count);
        ErrorRecorderHolder.recordError(new Exception());
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, this._recorder.Count);
    }

    [UnityTest]
    public IEnumerator Error_Set_Record() {
        this._recorder.Count = 0;
        ErrorRecorderHolder.setInstance(this._recorder);
        ErrorRecorderHolder.recordError(new Exception());
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(1, this._recorder.Count);
    }
}
