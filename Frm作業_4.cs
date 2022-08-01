using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_4 : Form
    {
        public Frm作業_4()
        {
            InitializeComponent();
            productsTableAdapter1.Fill(nwDataSet1.Products);
            ordersTableAdapter1.Fill(nwDataSet1.Orders);
            order_DetailsTableAdapter1.Fill(nwDataSet1.Order_Details);
            dbContext.Database.Log = Console.WriteLine;
            Fillin();
        }
        NorthwindEntities dbContext = new NorthwindEntities();
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
        private void button38_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            System.IO.FileInfo[] files = dir.GetFiles();
            var Q = from i in files
                    group i by i.Length > 1024 ? "Big" : "Small" into g
                    select g;
            dataGridView1.DataSource = Q.ToList();
            foreach (var group in Q)
            {
                TreeNode x = this.treeView1.Nodes.Add(group.Key.ToString());
                foreach (var item in group)
                {
                    x.Nodes.Add(item.ToString());
                }
            }
        }
        void Fillin()
        {
            var Q = from i in dbContext.Orders
                    select i.RequiredDate.Value.Year;
            foreach(var i in Q.Distinct())
            {
                comboBox1.Items.Add(i);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            System.IO.FileInfo[] files = dir.GetFiles();
            var Q = from i in files
                    group i by i.CreationTime.Year into g
                    orderby g.Key
                    select g;
            dataGridView1.DataSource = Q.ToList();
            foreach (var group in Q)
            {
                TreeNode x = this.treeView1.Nodes.Add(group.Key.ToString());
                foreach (var item in group)
                    x.Nodes.Add(item.ToString());
            }
        }
        private string Price(int n)
        {
            if (n > 100) { return "高價位"; }
            else if (n > 50) { return "中價位"; }
            else { return "低價位"; }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            var Q = from i in nwDataSet1.Products
                    group i by Price( (int)i.UnitPrice) into P
                    select P;
            foreach (var group in Q)
            {
                TreeNode x = this.treeView1.Nodes.Add(group.Key);
                foreach (var item in group)
                    x.Nodes.Add(item.ProductName.ToString());
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var Q = from i in nwDataSet1.Orders
                    group i by i.OrderDate.Year into Y
                    select Y;
            foreach(var i in Q)
            {
                TreeNode x = this.treeView1.Nodes.Add(i.Key.ToString());
                foreach (var ie in i)
                    x.Nodes.Add(ie.OrderID.ToString());
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            var Q = from i in nwDataSet1.Orders
                    group i by i.OrderDate.Year into Y
                    select Y;
            var Qs = from i in nwDataSet1.Orders
                    group i by i.OrderDate.Month into Y
                    select Y;
            foreach (var i in Q)
            {
                TreeNode x = this.treeView1.Nodes.Add(i.Key.ToString());
                foreach (var ie in Qs)
                {
                   TreeNode Xs=x.Nodes.Add(ie.Key.ToString());
                    foreach(var iss in ie)
                        Xs.Nodes.Add(iss.OrderID.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var Q =( from i in nwDataSet1.Order_Details
                    select new { s = i.UnitPrice * i.Quantity * (decimal)(1 - i.Discount)})
                    .Sum(S=>S.s);
            label2.Text =$"總銷售額={Q}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Q = from p in nwDataSet1.Orders
                    join i in nwDataSet1.Order_Details
                    on p.OrderID equals i.OrderID
                    group i by p.EmployeeID into E
                    orderby E.Sum(i => i.UnitPrice * i.Quantity * (decimal)(1 - i.Discount))
                    descending
                    select new
                    {
                        EmployeeID = E.Key,
                        Sum = E.Sum(i => i.UnitPrice * i.Quantity * (decimal)(1 - i.Discount))
                    };
            dataGridView1.DataSource =Q.Take(5).ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var Q = from i in dbContext.Products
                    orderby i.UnitPrice
                    select new { i.UnitPrice, i.Category.CategoryName };
            dataGridView1.DataSource =Q.Take(5).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var Q = (from i in dbContext.Products
                    where i.UnitPrice > 300
                    select i).Any();
            MessageBox.Show(""+Q);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") { comboBox1.Text = "1996"; }
            var Qs = from od in dbContext.Order_Details
                     join o in dbContext.Orders
                     on od.OrderID equals o.OrderID
                     join p in dbContext.Products
                     on od.ProductID equals p.ProductID
                     join c in dbContext.Categories
                    on p.CategoryID equals c.CategoryID
                     where o.RequiredDate.Value.Year.ToString()==comboBox1.Text
                     group od by c.CategoryName into g
                     select new
                     {
                         g.Key,
                         sum = g.Sum(i => i.Quantity)
                     };
            var Q = from p in dbContext.Products
                     join od in dbContext.Order_Details
                     on p.ProductID equals od.ProductID
                     join o in dbContext.Orders
                     on od.OrderID equals o.OrderID
                     join c in dbContext.Categories
                     on p.CategoryID equals c.CategoryID
                     join g in Qs
                     on c.CategoryName equals g.Key
                     where o.RequiredDate.Value.Year.ToString() == comboBox1.Text
                     select new
                     {
                         銷售額 = g.sum,
                         p.Category.CategoryName
                     };
            
            dataGridView1.DataSource = Q.OrderByDescending(i=>i.CategoryName)
                .Distinct().ToList();
            chart1.DataSource = Q.Distinct().ToList();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization
                .Charting.SeriesChartType.Column;
            chart1.Series[0].XValueMember = "CategoryName";
            chart1.Series[0].YValueMembers = "銷售額";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var Q = from od in dbContext.Order_Details
                    join o in dbContext.Orders
                    on od.OrderID equals o.OrderID
                    group od by o.OrderDate.Value.Year into g
                    select new
                    {
                        去年 = g.Key,
                        去年銷售額 = g.Sum(p =>(float)p.Quantity * (float)p.UnitPrice *(1 - p.Discount))
                    };
            var Qs = from od in dbContext.Order_Details
                    join o in dbContext.Orders
                    on od.OrderID equals o.OrderID
                    group od by o.OrderDate.Value.Year into g
                    select new
                    {
                        今年 = g.Key+1,
                        今年銷售額 = g.Sum(p =>(float)p.Quantity * (float)p.UnitPrice *(1 - p.Discount))
                    };
            var Qss = from i in Q
                      join Q1 in Qs
                      on i.去年 equals Q1.今年
                      select new
                      {
                          成長率年份 = i.去年
                          ,
                          銷售額 = i.去年銷售額
                          ,
                          該年成長率 = ((Q1.今年銷售額 - i.去年銷售額) / i.去年銷售額)
                      };
            dataGridView2.DataSource = Q.ToList();
            dataGridView1.DataSource = Qss.ToList();
            chart1.DataSource = Qss.ToList();
            chart1.Series[0].XValueMember = "成長率年份";
            chart1.Series[0].YValueMembers = "該年成長率";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

        }
    }
}

