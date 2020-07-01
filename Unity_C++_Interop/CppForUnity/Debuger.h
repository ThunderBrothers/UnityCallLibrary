#pragma once
#include <iostream>
using namespace std;

//Unity与C++交互最麻烦的是调试的过程，在C++ DLL中直接print或cout打印log是没法看到的，
//我们可以在C++中调用C#的函数来输出log，这需要将delegate映射到C++的函数指针。
//参考 www.jianshu.com/p/8877c1d3f2e2

class Debuger
{
public:
	typedef void (*DebugFuncPtr)(const char*);
	static DebugFuncPtr FuncPtr;
	static void SetDebugFuncPtr(DebugFuncPtr ptr);

	static char container[100];
	static void Log(const std::string str);
};

