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
        List<KeyValuePair<string, string>> infoList;
        int gridRowIndex = 0;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            infoList = batteryInfo.GetShortInfo();
            var bindingList = new BindingList<KeyValuePair<string, string>>(infoList);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.FirstDisplayedScrollingRowIndex = gridRowIndex;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            infoList = batteryInfo.GetShortInfo();
            var bindingList = new BindingList<KeyValuePair<string, string>>(infoList);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;

        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            gridRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
        }
    }
}
