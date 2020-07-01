#include "pch.h"
#include "UnityCppInterop.h"
#include "Debuger.h"

/// <summary>
/// �޷���ֵ����
/// ��ӡ ���
/// </summary>
void SayHello() {
	//std::cout << "���!" << std::endl;
	Debuger::Log("hello Unity");
}

/// <summary>
/// ���ؼ�ֵ����
/// </summary>
/// <param name="a"></param>
/// <param name="b"></param>
/// <returns> ����a+b��ֵ </returns>
int Add(int a, int b) {
	return a + b;
}

/// <summary>
/// ����Debug.Log�Ļص�
/// </summary>
/// <param name="fp"></param>
void SetDebugFunction(Debuger::DebugFuncPtr fp){
	Debuger::SetDebugFuncPtr(fp);
}