#pragma once
#include <iostream>
#include "Debuger.h"

//1.其中extern "C"的作用是告诉编译器将被它修饰的代码按C语言的方式进行编译
//2._declspec(dllexport)，此修饰符告诉编译器和链接器被它修饰的函数或变量需要从DLL导出，以供其他应用程序使用。与其相对的还有一句代码是__declspec(dllimport)此修饰符的作用是告诉编译器和链接器被它修饰的函数或变量需要从DLL导入，它在后面也会被用到。
//3.函数void SayHello()，它就是需要被其他程序调用的函数


extern "C" _declspec(dllexport) void SayHello();


extern "C" _declspec(dllexport) int Add(int a,int b);


extern "C" _declspec(dllexport) void SetDebugFunction(Debuger::DebugFuncPtr fp);