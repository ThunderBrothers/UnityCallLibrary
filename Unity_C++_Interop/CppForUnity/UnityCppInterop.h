#pragma once
#include <iostream>
#include "Debuger.h"

//1.����extern "C"�������Ǹ��߱��������������εĴ��밴C���Եķ�ʽ���б���
//2._declspec(dllexport)�������η����߱��������������������εĺ����������Ҫ��DLL�������Թ�����Ӧ�ó���ʹ�á�������ԵĻ���һ�������__declspec(dllimport)�����η��������Ǹ��߱��������������������εĺ����������Ҫ��DLL���룬���ں���Ҳ�ᱻ�õ���
//3.����void SayHello()����������Ҫ������������õĺ���


extern "C" _declspec(dllexport) void SayHello();


extern "C" _declspec(dllexport) int Add(int a,int b);


extern "C" _declspec(dllexport) void SetDebugFunction(Debuger::DebugFuncPtr fp);