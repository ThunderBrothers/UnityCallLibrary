using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    private AndroidJavaClass ajc;
    public Text sum;

    // Start is called before the first frame update
    void Start()
    {
        ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        CallShowDialog();
        CallAdd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CallShowDialog()
    {
        AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        ajo.Call("showDialog","这是一个标题","内容");
    }

    public void CallAdd()
    {
        AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        int shu = ajo.Call<int>("GetAdd",2,5);
        sum.text = shu.ToString();
    }
}
