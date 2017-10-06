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
        static UInt64 oldBatteryLevel;

        public List<KeyValuePair<string, string>> GetInfo()
        {
            List<KeyValuePair<string, string>> infoList = new List<KeyValuePair<string, string>>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Battery");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                foreach (PropertyData property in queryObj.Properties)
                {
                    string str = string.Format("{0}",property.Value);
                    infoList.Add(new KeyValuePair<string, string>(property.Name, str));
                }
            }
            return infoList;
        }

        public List<KeyValuePair<string, string>> GetShortInfo()
        {
            List<KeyValuePair<string, string>> infoList = new List<KeyValuePair<string, string>>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT DesignVoltage, EstimatedChargeRemaining FROM Win32_Battery");
            if (searcher.Get() == null)
                infoList.Add(new KeyValuePair<string, string>("Type", "AC"));
            else
            {
                infoList.Add(new KeyValuePair<string, string>("Type", "Battery"));
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    string charge = string.Format("{0}%", queryObj["EstimatedChargeRemaining"]);
                    infoList.Add(new KeyValuePair<string, string>("Charge: ", charge));

                    UInt64 v = (UInt64)queryObj["DesignVoltage"];
                    oldBatteryLevel = v;
                    string voltage = string.Format("{0} V", ((double)v)/1000);
                    infoList.Add(new KeyValuePair<string, string>("Voltage: ", voltage));

                    Type t = typeof(System.Windows.Forms.PowerStatus);
                    PropertyInfo[] pi = t.GetProperties();
                    for (int i = 0; i < pi.Length; i++)
                        infoList.Add(new KeyValuePair<string, string>(pi[i].Name, pi[i].GetValue(SystemInformation.PowerStatus, null).ToString()));
                    
                }
            }
            return infoList;
        }

        
    }
}
