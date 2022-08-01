using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            ordersTableAdapter1.Fill(nwDataSet1.Orders);
            order_DetailsTableAdapter1.Fill(nwDataSet1.Order_Details);
           
        }
        public void Fillin()
        {
            var Q = nwDataSet1.Orders.Select(i => i.OrderDate.Year).Distinct();
            foreach (int time in Q.ToList())
                comboBox1.Items.Add(time);
            comboBox2.Items.Add(100);
            comboBox2.Items.Add(10);
            comboBox2.Items.Add(1);
        }

        private void Frm作業_1_Load(object sender, EventArgs e)
        {
            Fillin();
        }
       
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") { comboBox1.Text = "1996"; }
            if (comboBox2.Text == "") { comboBox2.Text = "10"; }
            int C = Convert.ToInt32(comboBox2.Text);
            ClickCount++;
            if (ClickCount -1 >= 0)
            {
                var Q = nwDataSet1.Orders.
                   Where(i => i.OrderDate.Year == Convert.ToInt32(comboBox1.Text)).
                   Select(i => i);
                var Q2 = from i in nwDataSet1.Order_Details
                          join p in nwDataSet1.Orders
                          on i.OrderID equals p.OrderID
                          where p.OrderDate.Year == Convert.ToInt32(comboBox1.Text)
                          select i;
                if (C * ClickCount > Q.Count()) { return; }
                dataGridView1.DataSource = Q.Take(C * ClickCount).Skip(C * (ClickCount - 1)).ToList();
                dataGridView2.DataSource = Q2.Take(C * ClickCount).Skip(C * (ClickCount - 1)).ToList();                 
            }
            else
                ClickCount = 1;
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");


        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.FileInfo[] files = dir.GetFiles();
            var Q = files.Where(i => i.Extension.ToLower().Contains(".log".ToLower()));
            this.dataGridView1.DataSource = Q.ToList();
            ClickCount = 0;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?						

            // 找出 前面三個 的學員所有科目成績					
            // 找出 後面兩個 的學員所有科目成績					

            // 找出 Name 'aaa','bbb','ccc' 的學成績						

            // 找出學員 'bbb' 的成績	                          

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	


            // 數學不及格 ... 是誰 
            #endregion

        }

        private void button37_Click(object sender, EventArgs e)
        {
            //new {.....  Min=33, Max=34.}
            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |		

            //個人 所有科的  sum, min, max, avg


        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.FileInfo[] files = dir.GetFiles();
            var Q = files.Where(i => i.CreationTime.Year == 2019);
            this.dataGridView1.DataSource = Q.ToList();
            ClickCount = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.FileInfo[] files = dir.GetFiles();
            var Q = files.Where(i => i.Length > 1000000);
            this.dataGridView1.DataSource = Q.ToList();
            ClickCount = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var Q = nwDataSet1.Orders.Select(i => i);
            dataGridView1.DataSource = Q.ToList();
            ClickCount = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Q = nwDataSet1.Orders.
                Where(i => i.OrderDate.Year == Convert.ToInt32(comboBox1.Text)).
                Select(i => i);
            var Q2 = from i in nwDataSet1.Order_Details
                     join p in nwDataSet1.Orders
                     on i.OrderID equals p.OrderID
                     where p.OrderDate.Year == Convert.ToInt32(comboBox1.Text)
                     select i;
            if (comboBox1.Text == "") { comboBox1.Text = "1996"; }
            dataGridView1.DataSource = Q.ToList();
            dataGridView2.DataSource = Q2.ToList();
         }
        int ClickCount = 1;
        private void button12_Click(object sender, EventArgs e)
        {
            
            if (comboBox2.Text=="") { comboBox2.Text = "10"; }
            if (comboBox1.Text == "") { comboBox1.Text = "1996"; }
            int C = Convert.ToInt32(comboBox2.Text);
            ClickCount--;
            if (ClickCount - 1 >= 0)
            {
                var Q = nwDataSet1.Orders.
                   Where(i => i.OrderDate.Year == Convert.ToInt32(comboBox1.Text)).
                   Select(i => i).Take(C * ClickCount).Skip(C * (ClickCount - 1));
                var Q2 = (from i in nwDataSet1.Order_Details
                          join p in nwDataSet1.Orders
                          on i.OrderID equals p.OrderID
                          where p.OrderDate.Year == Convert.ToInt32(comboBox1.Text)
                          select i).
                         Take(C * ClickCount).Skip(C *( ClickCount - 1));
                dataGridView1.DataSource = Q.ToList();
                dataGridView2.DataSource = Q2.ToList();
            }
            else
                ClickCount = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }

