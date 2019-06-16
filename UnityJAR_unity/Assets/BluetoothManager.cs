using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothManager : MonoBehaviour
{
    
    public Button openBlueSync;
    public Button openBlueAync;
    public Button startscnner;
    public Button stoptscnner;
    public Button close;

    public Button clearLog;


    public Button exit;

    private AndroidJavaClass ajc;
    public Text logShown;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        if (ajc != null)
        {
            //初始化蓝牙
            CallgetBluetoothAdapter();

            openBlueSync.onClick.AddListener(CallOpenBlueSync);
            openBlueAync.onClick.AddListener(CallOpenBlueAsyn);

            startscnner.onClick.AddListener(() => CallscanleDevice(true));
            stoptscnner.onClick.AddListener(() => CallscanleDevice(false));
        }
#endif
        clearLog.onClick.AddListener(() => logShown.text = "************");

        exit.onClick.AddListener(()=> { Application.Quit(); });
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //初始化Adapter
    public void CallgetBluetoothAdapter()
    {
        Debug.Log($"[UnityBlue] CallgetBluetoothAdapter");
        AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        ajo.Call("getBluetoothAdapter");
        CallhasBluetooth();
        CallisSupportBlue();
    }
    //判断是否有蓝牙功能模块
    public void CallhasBluetooth()
    {
        bool hasBluetooth = false;
        Debug.Log($"[UnityBlue] CallhasBluetooth");
        AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        hasBluetooth = ajo.Call<bool>("hasBluetooth");
        Debug.Log($"[UnityBlue]  是否有蓝牙功能模块= {hasBluetooth}");
    }
    //设备是否支持蓝牙
    public void CallisSupportBlue()
    {
        bool isSupportblue = false;
        Debug.Log($"[UnityBlue] CallisSupportBlue");
        AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        isSupportblue = ajo.Call<bool>("hasBluetooth");
        Debug.Log($"[BlUnityBlueue] 是否支持蓝牙 = {isSupportblue}");
    }
    //蓝牙是否打开
    public bool CallisBlueEnable()
    {
        bool isBlueEnable = false;
        Debug.Log($"[UnityBlue] CallisBlueEnable");
        AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        Debug.Log($"[UnityBlue] 蓝牙是否打开 = {isBlueEnable}");
        isBlueEnable = ajo.Call<bool>("isBlueEnable");
        return isBlueEnable;
    }
   

    //========================================操作=========================================================
    //开启蓝牙同步
    public void CallOpenBlueSync()
    {
        Debug.Log($"[UnityBlue] 开启蓝牙同步");
        AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        ajo.Call("opneBlueSync");
    }
    //开启蓝牙异步
    public void CallOpenBlueAsyn()
    {
        Debug.Log($"[UnityBlue] 开启蓝牙异步");
        AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        ajo.Call("openBlueAsyn");
    }
    //扫描设备
    public void CallscanleDevice(bool enable)
    {
        Debug.Log($"[UnityBlue] CallscanleDevice = {enable}");
        AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        ajo.Call("scanDevice", enable);
    }


    //========================================安卓调用Unity================================================
    //安卓调用筛选器
    public void CalledFilter(string value)
    {
        Debug.Log($"[UnityBlue安卓调用] CalledFilter value = {value}");
        logShown.text += value;
    }



    //========================================测试=========================================================
    public void CallShowDialog()
    {
        AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        ajo.Call("showDialog", "这是一个标题", "内容");
    }

    public void CallAdd()
    {
        AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        int shu = ajo.Call<int>("GetAdd", 2, 5);
        logShown.text = shu.ToString();
    }
}
