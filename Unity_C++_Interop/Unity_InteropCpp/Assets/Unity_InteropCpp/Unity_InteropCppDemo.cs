using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Unity_InteropCppDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RegisterDebugCallBack();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.LogError("Call cpp SayHello();");
            SayHello();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.LogError("Call cpp Add(1,2) = "+ Add(1, 2));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.LogError("Call AddCpp EntryPoint AddCpp(2,3) = " + AddCpp(2, 3));
        }
    }

    #region 基础调用
    /// <summary>
    /// SayHello调用一个函数，方法名和Cpp里的相同SayHello
    /// </summary>
    [DllImport("CppForUnity")]
    public  static extern void SayHello();

    /// <summary>
    /// Add调用一个有返回值得函数，方法名和Cpp里的相同Add
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    [DllImport("CppForUnity")]
    public static extern int Add(int a, int b);

    /// <summary>
    /// AddCpp调用一个有返回值得函数,其中cpp的接入点EntryPoint = Add(原cpp函数名称)
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    [DllImport("CppForUnity",EntryPoint = "Add")]
    public static extern int AddCpp(int a, int b);

    #endregion


    #region 复杂调用

    //Unity与C++交互最麻烦的是调试的过程，在C++ DLL中直接print或cout打印log是没法看到的，
    //我们可以在C++中调用C#的函数来输出log，这需要将delegate映射到C++的函数指针。
    //参考 www.jianshu.com/p/8877c1d3f2e2
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DebugDelegate(IntPtr strPtr);
   
    private void RegisterDebugCallBack()
    {
        DebugDelegate callbcak_delegate = CallBackFunction;
        //将Delegate转换为非托管的函数指针
        IntPtr intptr_delegate = Marshal.GetFunctionPointerForDelegate(callbcak_delegate);
        //调用非托管函数,将Unity的Debug.Log设置到C++代码中方便直接使用
        SetDebugFunction(intptr_delegate);
    }
    private static void CallBackFunction(IntPtr strPtr)
    {
        Debug.Log("[Cpp-Log]" + Marshal.PtrToStringAnsi(strPtr));
    }

    [DllImport("CppForUnity")]
    public static extern void SetDebugFunction(IntPtr fp);

    #endregion
}
