#include <Windows.h>
#include <iostream>
#include <fstream>
#include <PowrProf.h>
#include <string>
#include <sstream>
using namespace std;

class PowerPlan {
private:

	SYSTEM_POWER_POLICY getPowerPolicy(POWER_INFORMATION_LEVEL SystemPowerPolicy) {
		SYSTEM_POWER_POLICY powerPolicy;
		DWORD size = sizeof(SYSTEM_POWER_POLICY);
		DWORD ret = CallNtPowerInformation(SystemPowerPolicy, nullptr, 0, &powerPolicy, size);
		if ((ret != ERROR_SUCCESS) || (size != sizeof(SYSTEM_POWER_POLICY)))
			throw "Can't get SYSTEM_POWER_POLICY information.";
		return powerPolicy;
	}

	void setPowerPolicy(POWER_INFORMATION_LEVEL SystemPowerPolicy, SYSTEM_POWER_POLICY powerPolicy) {
		DWORD size = sizeof(SYSTEM_POWER_POLICY);
		DWORD ret = CallNtPowerInformation(SystemPowerPolicy, &powerPolicy, size, nullptr, 0);
		if ((ret != ERROR_SUCCESS))
			throw "Can't write system power policy";
	}

public:
	ULONG getSleepModeTimeout() {
		SYSTEM_POWER_POLICY powerPolicy = getPowerPolicy(SystemPowerPolicyCurrent);
		return powerPolicy.IdleTimeout;
	}

	ULONG getSleepModeTimeoutAC() {
		SYSTEM_POWER_POLICY powerPolicy = getPowerPolicy(SystemPowerPolicyAc);
		return powerPolicy.IdleTimeout;
	}

	ULONG getSleepModeTimeoutDC() {
		SYSTEM_POWER_POLICY powerPolicy = getPowerPolicy(SystemPowerPolicyDc);
		return powerPolicy.IdleTimeout;
	}

	ULONG getScreenOffTimeout() {
		SYSTEM_POWER_POLICY powerPolicy = getPowerPolicy(SystemPowerPolicyCurrent);
		return powerPolicy.VideoTimeout;
	}

	ULONG getScreenOffTimeoutAC() {
		SYSTEM_POWER_POLICY powerPolicy = getPowerPolicy(SystemPowerPolicyAc);
		return powerPolicy.VideoTimeout;
	}

	ULONG getScreenOffTimeoutDC() {
		SYSTEM_POWER_POLICY powerPolicy = getPowerPolicy(SystemPowerPolicyDc);
		return powerPolicy.VideoTimeout;
	}

	void setSleepModeTimeoutAC(DWORD timeout) {
		SYSTEM_POWER_POLICY powerPolicy = getPowerPolicy(SystemPowerPolicyAc);
		powerPolicy.IdleTimeout = timeout;
		setPowerPolicy(SystemPowerPolicyAc, powerPolicy);
	}

	void setSleepModeTimeoutDC(DWORD timeout) {
		SYSTEM_POWER_POLICY powerPolicy = getPowerPolicy(SystemPowerPolicyDc);
		powerPolicy.IdleTimeout = timeout;
		setPowerPolicy(SystemPowerPolicyDc, powerPolicy);
	}

	void setScreenOffTimeoutAC(DWORD timeout) {
		SYSTEM_POWER_POLICY powerPolicy = getPowerPolicy(SystemPowerPolicyAc);
		powerPolicy.VideoTimeout = timeout;
		setPowerPolicy(SystemPowerPolicyAc, powerPolicy);
	}

	void setScreenOffTimeoutDC(DWORD timeout) {
		SYSTEM_POWER_POLICY powerPolicy = getPowerPolicy(SystemPowerPolicyDc);
		powerPolicy.VideoTimeout = timeout;
		setPowerPolicy(SystemPowerPolicyDc, powerPolicy);
	}

};