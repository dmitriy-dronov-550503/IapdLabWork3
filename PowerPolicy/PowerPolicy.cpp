// ֳכאגםûי DLL-פאיכ.

#include "Stdafx.h"

#include "PowerPolicy.h"

namespace PowerPolicy {


		SYSTEM_POWER_POLICY PowerPlan::getPowerPolicy(POWER_INFORMATION_LEVEL SystemPowerPolicy) {
			SYSTEM_POWER_POLICY powerPolicy;
			DWORD size = sizeof(SYSTEM_POWER_POLICY);
			DWORD ret = CallNtPowerInformation(SystemPowerPolicy, nullptr, 0, &powerPolicy, size);
			if ((ret != ERROR_SUCCESS) || (size != sizeof(SYSTEM_POWER_POLICY)))
				throw "Can't get SYSTEM_POWER_POLICY information.";
			return powerPolicy;
		}

		void PowerPlan::setPowerPolicy(POWER_INFORMATION_LEVEL SystemPowerPolicy, SYSTEM_POWER_POLICY powerPolicy) {
			DWORD size = sizeof(SYSTEM_POWER_POLICY);
			DWORD ret = CallNtPowerInformation(SystemPowerPolicy, &powerPolicy, size, nullptr, 0);
			if ((ret != ERROR_SUCCESS))
				throw "Can't write system power policy";
		}

		ULONG PowerPlan::getSleepModeTimeout() {
			SYSTEM_POWER_POLICY powerPolicy = PowerPlan::getPowerPolicy(SystemPowerPolicyCurrent);
			return powerPolicy.IdleTimeout;
		}

		ULONG PowerPlan::getSleepModeTimeoutAC() {
			SYSTEM_POWER_POLICY powerPolicy = PowerPlan::getPowerPolicy(SystemPowerPolicyAc);
			return powerPolicy.IdleTimeout;
		}

		ULONG PowerPlan::getSleepModeTimeoutDC() {
			SYSTEM_POWER_POLICY powerPolicy = PowerPlan::getPowerPolicy(SystemPowerPolicyDc);
			return powerPolicy.IdleTimeout;
		}

		ULONG PowerPlan::getScreenOffTimeout() {
			SYSTEM_POWER_POLICY powerPolicy = PowerPlan::getPowerPolicy(SystemPowerPolicyCurrent);
			return powerPolicy.VideoTimeout;
		}

		ULONG PowerPlan::getScreenOffTimeoutAC() {
			SYSTEM_POWER_POLICY powerPolicy = PowerPlan::getPowerPolicy(SystemPowerPolicyAc);
			return powerPolicy.VideoTimeout;
		}

		ULONG PowerPlan::getScreenOffTimeoutDC() {
			SYSTEM_POWER_POLICY powerPolicy = PowerPlan::getPowerPolicy(SystemPowerPolicyDc);
			return powerPolicy.VideoTimeout;
		}

		void PowerPlan::setSleepModeTimeoutAC(DWORD timeout) {
			SYSTEM_POWER_POLICY powerPolicy = PowerPlan::getPowerPolicy(SystemPowerPolicyAc);
			powerPolicy.IdleTimeout = timeout;
			setPowerPolicy(SystemPowerPolicyAc, powerPolicy);
		}

		void PowerPlan::setSleepModeTimeoutDC(DWORD timeout) {
			SYSTEM_POWER_POLICY powerPolicy = PowerPlan::getPowerPolicy(SystemPowerPolicyDc);
			powerPolicy.IdleTimeout = timeout;
			setPowerPolicy(SystemPowerPolicyDc, powerPolicy);
		}

		void PowerPlan::setScreenOffTimeoutAC(DWORD timeout) {
			SYSTEM_POWER_POLICY powerPolicy = PowerPlan::getPowerPolicy(SystemPowerPolicyAc);
			powerPolicy.VideoTimeout = timeout;
			setPowerPolicy(SystemPowerPolicyAc, powerPolicy);
		}

		void PowerPlan::setScreenOffTimeoutDC(DWORD timeout) {
			SYSTEM_POWER_POLICY powerPolicy = PowerPlan::getPowerPolicy(SystemPowerPolicyDc);
			powerPolicy.VideoTimeout = timeout;
			setPowerPolicy(SystemPowerPolicyDc, powerPolicy);
		}

}