#pragma once
#include <iostream>
using namespace std;

//Unity��C++�������鷳���ǵ��ԵĹ��̣���C++ DLL��ֱ��print��cout��ӡlog��û�������ģ�
//���ǿ�����C++�е���C#�ĺ��������log������Ҫ��delegateӳ�䵽C++�ĺ���ָ�롣
//�ο� www.jianshu.com/p/8877c1d3f2e2

class Debuger
{
public:
	typedef void (*DebugFuncPtr)(const char*);
	static DebugFuncPtr FuncPtr;
	static void SetDebugFuncPtr(DebugFuncPtr ptr);

	static char container[100];
	static void Log(const std::string str);
};

