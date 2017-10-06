using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace IapdLabWork3
{
    class BatteryInfo
    {
        private string powerType = "N/A", adapterStatus = "N/A", batteryStatus = "N/A";
        private Double remainingTimeInSeconds = 0.0;
        private UInt16 chargeInPercent = 0;
        private UInt64 voltage = 0;
        ManagementObjectSearcher searcher;

        public List<KeyValuePair<string, string>> GetListInfo()
        {
            List<KeyValuePair<string, string>> infoList = new List<KeyValuePair<string, string>>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Battery");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                foreach (PropertyData property in queryObj.Properties)
                {
                    string str = string.Format("{0}", property.Value);
                    infoList.Add(new KeyValuePair<string, string>(property.Name, str));
                }
            }
            return infoList;
        }

        public void UpdateInfo()
        {
            searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT DesignVoltage, EstimatedChargeRemaining FROM Win32_Battery");
            if (searcher.Get() == null)
                powerType = "AC";
            else
            {
                powerType = "Battery";
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    chargeInPercent = (UInt16)queryObj["EstimatedChargeRemaining"];
                    Type type = typeof(PowerStatus);
                    PropertyInfo[] pi = type.GetProperties();
                    adapterStatus = pi[0].GetValue(SystemInformation.PowerStatus, null).ToString();
                    batteryStatus = pi[1].GetValue(SystemInformation.PowerStatus, null).ToString();
                    remainingTimeInSeconds = Double.Parse(pi[4].GetValue(SystemInformation.PowerStatus, null).ToString());
                    voltage = (UInt64)queryObj["DesignVoltage"];
                }
            }
        }

        public Double GetRemainingTimeInSeconds()
        {
            return remainingTimeInSeconds;
        }

        public string GetRemainingTime()
        {
            if (remainingTimeInSeconds != (double)-1)
            {
                TimeSpan t = TimeSpan.FromSeconds(remainingTimeInSeconds);
                return string.Format("{0:D2}h:{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);
            }
            else return "Charging";
        }

        public UInt64 GetVoltage()
        {
            //string.Format("{0} V", ((double)voltage) / 1000)
            return voltage;
        }

        public string GetPowerType()
        {
            return powerType;
        }

        public UInt16 GetCharge()
        {
            return chargeInPercent;
        }

        public string GetAdapterStatus()
        {
            return adapterStatus;
        }

        public string GetBatteryStatus()
        {
            return batteryStatus;
        }
    }
}
