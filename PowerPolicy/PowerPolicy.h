// PowerPolicy.h


#pragma once
#include "Stdafx.h"
#include <Windows.h>
#include <iostream>
#include <fstream>
#include <PowrProf.h>
#include <string>
#include <sstream>
using namespace std;
using namespace System;


namespace PowerPolicy {

	public ref class PowerPlan
	{
	private:
		static SYSTEM_POWER_POLICY getPowerPolicy(POWER_INFORMATION_LEVEL SystemPowerPolicy);

		static void setPowerPolicy(POWER_INFORMATION_LEVEL SystemPowerPolicy, SYSTEM_POWER_POLICY powerPolicy);
	public:
		static ULONG getSleepModeTimeout();

		static ULONG getSleepModeTimeoutAC();

		static ULONG getSleepModeTimeoutDC();

		static ULONG getScreenOffTimeout();

		static ULONG getScreenOffTimeoutAC();

		static ULONG getScreenOffTimeoutDC();

		static void setSleepModeTimeoutAC(DWORD timeout);

		static void setSleepModeTimeoutDC(DWORD timeout);

		static void setScreenOffTimeoutAC(DWORD timeout);

		static void setScreenOffTimeoutDC(DWORD timeout);
	};
}
