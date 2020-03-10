using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInfoBridge
{
    private string infos = "No test info ...";

    public void SetInfos(string message)
    {
        lock (this)
            infos = message;
    }

    public string GetInfos()
    {
        lock (this)
            return infos;
    }

    public static TextInfoBridge Instance;
    public static void Init()
    {
        Instance = new TextInfoBridge();
    }
}

public class TestInfos : MonoBehaviour
{
    private Text textComp;

    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textComp.text = TextInfoBridge.Instance.GetInfos();
    }
}
