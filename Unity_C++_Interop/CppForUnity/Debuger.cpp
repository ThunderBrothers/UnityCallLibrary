#include "pch.h"
#include "Debuger.h"

Debuger::DebugFuncPtr Debuger::FuncPtr;

extern "C" void Debuger::SetDebugFuncPtr(DebugFuncPtr ptr) {
	FuncPtr = ptr;
}

char Debuger::container[100];

void Debuger::Log(const string str) {
	if (FuncPtr != nullptr)
	{
		FuncPtr(str.c_str());
	}
}
