using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using com.fpnn;

public class Main : MonoBehaviour
{
    private Thread _thread; 
	private Program _program;
    private asyncStressClient _asyncTester;

    // Start is called before the first frame update
    void Start() {
        
        Debug.Log("hello fpnn!");

        this._thread = new Thread(ThreadFunc);
        this._thread.Start();
    }

    private void ThreadFunc() {

        this._asyncTester = new asyncStressClient("52.83.245.22", 13013, 10, 1500);
        this._asyncTester.launch();
        this._asyncTester.showStatistics(); 

        // if (this._program == null) {

        //     this._program = new Program();
        //     this._program.Begin();
        // }
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnApplicationQuit() {

    	if (this._program != null) {

    		this._program.End();
    	}

        if (this._asyncTester != null) {

            this._asyncTester.stop();
        }
    }

    void OnApplicationPause() {
        
        if (this._program != null) {

            this._program.End();
        }

        if (this._asyncTester != null) {

            this._asyncTester.stop();
        }
    }
}
