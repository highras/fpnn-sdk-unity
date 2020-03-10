using System;
using UnityEngine;

class Recorder : com.fpnn.common.ErrorRecorder
{
    public void RecordError(Exception e)
    {
        Debug.Log(e.ToString());
    }
    public void RecordError(string message)
    {
        Debug.Log(message);
    }
    public void RecordError(string message, Exception e)
    {
        Debug.Log(message + "\n" + e.ToString());
    }
}
