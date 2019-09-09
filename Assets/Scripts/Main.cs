using System;
using System.Collections;
using UnityEngine;

using com.fpnn;

public class Main : MonoBehaviour {

    public interface ITestCase {

        void StartTest();
        void StopTest();
    }

    private ITestCase _testCase;

    void Start() {

        Debug.Log("hello fpnn!");

        FPManager.Instance.Init();

        //SingleClientConnect
        this._testCase = new SingleClientConnect();

        if (this._testCase != null) {

            this._testCase.StartTest();
        }
    }

    void Update() {}

    void OnApplicationQuit() {

    	if (this._testCase != null) {

            this._testCase.StopTest();
        }
    }

    void OnApplicationPause() {
        
        if (this._testCase != null) {

            this._testCase.StopTest();
        }
    }
}