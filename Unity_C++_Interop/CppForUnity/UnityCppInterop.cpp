#include "pch.h"
#include "UnityCppInterop.h"

/// <summary>
/// �޷���ֵ����
/// ��ӡ ���
/// </summary>
void SayHello() {
	std::cout << "���!" << std::endl;
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