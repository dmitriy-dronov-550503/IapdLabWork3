using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerPolicy;

namespace IapdLabWork3
{
    public partial class Form1 : Form
    {
        BatteryInfo batteryInfo = new BatteryInfo();
        string previousAdapterStatus = "Offline";
        Boolean isAdapter = false;
        int screenOffOldValue = -1, sleepModeOldValue = -1;

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
            dateTimePicker1.Format = dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = dateTimePicker2.CustomFormat = "HH:mm:ss";
            dateTimePicker1.ShowUpDown = dateTimePicker2.ShowUpDown = true;
            batteryInfo.UpdateInfo();
            pictureBox1.Height = (int)(((double)panel1.Height / 100) * (batteryInfo.GetCharge())) + 15;
            label1.Text = batteryInfo.GetCharge().ToString() + "%";
            label2.Text = "Power type: " + batteryInfo.GetPowerType();
            if (batteryInfo.GetPowerType().Equals("AC"))
            {
                MessageBox.Show("You are using computer without battery, or system battery information is not available.", "Battery not detected",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                dateTimePicker1.Enabled = dateTimePicker2.Enabled = false;
                trackBar1.Enabled = trackBar2.Enabled = false;
                linkLabel1.Enabled = linkLabel2.Enabled = false;
                dateTimePicker1.CustomFormat = dateTimePicker2.CustomFormat = "N/A";
                timer1.Stop();
            }
            label3.Text = batteryInfo.GetRemainingTime();
            previousAdapterStatus = batteryInfo.GetAdapterStatus();
            label4.Text = "Adapter status: " + previousAdapterStatus;
            label5.Text = "Battery status: " + batteryInfo.GetBatteryStatus();
            label6.Text = "Voltage: " + (batteryInfo.GetVoltage() / 1000).ToString() + " V";
            isAdapter = "Online".Equals(batteryInfo.GetAdapterStatus());
            configureTimeTracks();
        }

        private void configureTimeTracks()
        {
            trackBar1.Value = (int)(isAdapter ? PowerPlan.getScreenOffTimeoutAC() : PowerPlan.getScreenOffTimeoutDC());
            screenOffOldValue = screenOffOldValue == -1 ? trackBar1.Value : screenOffOldValue;
            sleepModeOldValue = trackBar2.Value = (int)(isAdapter ? PowerPlan.getSleepModeTimeoutAC() : PowerPlan.getSleepModeTimeoutDC());
            sleepModeOldValue = sleepModeOldValue == -1 ? trackBar2.Value : sleepModeOldValue;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            batteryInfo.UpdateInfo();
            pictureBox1.Height = (int)((((double)panel1.Height / 100) * (batteryInfo.GetCharge()))) + 15;
            label1.Text = batteryInfo.GetCharge().ToString() + "%";
            label2.Text = "Power type: " + batteryInfo.GetPowerType();
            label3.Text = batteryInfo.GetRemainingTime();
            label4.Text = "Adapter status: " + batteryInfo.GetAdapterStatus();
            isAdapter = "Online".Equals(batteryInfo.GetAdapterStatus());
            if (!batteryInfo.GetAdapterStatus().Equals(previousAdapterStatus))
                configureTimeTracks();
            previousAdapterStatus = batteryInfo.GetAdapterStatus();
            label5.Text = "Battery status: " + batteryInfo.GetBatteryStatus();
            label6.Text = "Voltage: " + ((double)batteryInfo.GetVoltage() / 1000).ToString() + " V";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (isAdapter)
            {
                PowerPlan.setScreenOffTimeoutAC((uint)trackBar1.Value);
                PowerPlan.setSleepModeTimeoutAC((uint)trackBar2.Value);
            }
            else
            {
                PowerPlan.setScreenOffTimeoutDC((uint)trackBar1.Value);
                PowerPlan.setSleepModeTimeoutDC((uint)trackBar2.Value);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar1.Value != 0)
            {
                dateTimePicker1.CustomFormat = "HH:mm:ss";
                TimeSpan t = TimeSpan.FromSeconds(trackBar1.Value);
                dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, t.Hours, t.Minutes, t.Seconds);
            }
            else dateTimePicker1.CustomFormat = "Never";
            trackBar2.Value = trackBar1.Value >= trackBar2.Value ? trackBar1.Value : trackBar2.Value;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar2.Value != 0)
            {
                dateTimePicker2.CustomFormat = "HH:mm:ss";
                TimeSpan t = TimeSpan.FromSeconds(trackBar2.Value);
                dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, t.Hours, t.Minutes, t.Seconds);
            }
            else dateTimePicker2.CustomFormat = "Never";
            trackBar1.Value = trackBar1.Value >= trackBar2.Value ? trackBar2.Value : trackBar1.Value;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime t = dateTimePicker1.Value;
            int seconds = t.Hour * 60 * 60 + t.Minute * 60 + t.Second;
            trackBar1.Value = seconds;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime t = dateTimePicker2.Value;
            int seconds = t.Hour * 60 * 60 + t.Minute * 60 + t.Second;
            trackBar2.Value = seconds;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            trackBar1.Value = screenOffOldValue;
            trackBar2.Value = sleepModeOldValue;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isAdapter)
            {
                PowerPlan.setScreenOffTimeoutAC((uint)screenOffOldValue);
                PowerPlan.setSleepModeTimeoutAC((uint)sleepModeOldValue);
            }
            else
            {
                PowerPlan.setScreenOffTimeoutDC((uint)screenOffOldValue);
                PowerPlan.setSleepModeTimeoutDC((uint)sleepModeOldValue);
            }
        }
    }
}
