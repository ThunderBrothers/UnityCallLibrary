using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Unity_InteropCppDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
