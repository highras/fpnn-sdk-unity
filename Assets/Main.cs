using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.test;

public class Main : MonoBehaviour
{
	private Program _program;

    // Start is called before the first frame update
    void Start() {
        
        Debug.Log("hello fpnn!");

        this._program = new Program();
        this._program.Begin();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnApplicationQuit() {

    	if (this._program != null) {

    		this._program.End();
    	}
    }

    void OnApplicationPause() {
        
        if (this._program != null) {

            this._program.End();
        }
    }
}
