using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IapdLabWork3
{
    public partial class Form1 : Form
    {
        BatteryInfo batteryInfo = new BatteryInfo();

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;
            label6.BackColor = Color.Transparent;
            batteryInfo.UpdateInfo();
            pictureBox1.Height = (int)((((double)panel1.Height / 100) * (batteryInfo.GetCharge())) * 1.1);
            label1.Text = batteryInfo.GetCharge().ToString() + "%";
            label2.Text = "Power type: " + batteryInfo.GetPowerType();
            label3.Text = batteryInfo.GetRemainingTime();
            label4.Text = "Adapter status: " + batteryInfo.GetAdapterStatus();
            label5.Text = "Battery status: " + batteryInfo.GetBatteryStatus();
            label6.Text = "Voltage: " + (batteryInfo.GetVoltage() / 1000).ToString() + " V";
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            batteryInfo.UpdateInfo();
            pictureBox1.Height = (int)((((double)panel1.Height / 100) * (batteryInfo.GetCharge()))*1.1);
            label1.Text = batteryInfo.GetCharge().ToString()+"%";
            label2.Text = "Power type: " + batteryInfo.GetPowerType();
            label3.Text = batteryInfo.GetRemainingTime();
            label4.Text = "Adapter status: " + batteryInfo.GetAdapterStatus();
            label5.Text = "Battery status: " + batteryInfo.GetBatteryStatus();
            label6.Text = "Voltage: " + ((double)batteryInfo.GetVoltage()/1000).ToString()+" V";
        }
        
    }
}
