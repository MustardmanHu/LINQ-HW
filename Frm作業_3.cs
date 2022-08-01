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
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
            students_scores = new List<Student>()
            {
              new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
              new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
              new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
              new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
              new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
              new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
             

            };
            Fillin();
        }
        List<Student> students_scores;
        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }
        public void Fillin()
        {
            comboBox1.Items.Add("國文");
            comboBox1.Items.Add("英文");
            comboBox1.Items.Add("數學");
            if (comboBox1.Text == "") { comboBox1.Text = "國文"; }
            var Q = from i in students_scores
                    select i.Name; 
            if (comboBox2.Text == "") { comboBox2.Text = "aaa"; }
            foreach (string i in Q.ToList())
                comboBox2.Items.Add(i);
            
        }


        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 數學成績 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
        }

        private void button36_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = students_scores;
            if (comboBox1.Text == "國文")
            {
                chart1.DataSource = students_scores.Select(i => new { i.Name, i.Chi });
                chart1.Series[0].YValueMembers = "Chi";
            }
            if (comboBox1.Text == "英文")
            {
                chart1.DataSource = students_scores.Select(i => new { i.Name, i.Eng });
                chart1.Series[0].YValueMembers = "Eng";
            }
            if (comboBox1.Text == "數學")
            {
                chart1.DataSource = students_scores.Select(i => new { i.Name, i.Math });
                chart1.Series[0].YValueMembers = "Math";
            }
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.
                SeriesChartType.Column;
            chart1.Series[0].XValueMember= "Name";
        }

        private void button37_Click(object sender, EventArgs e)
        {
            var Q = students_scores.Where(i=>i.Name==comboBox2.Text).
                Select(i => new { i.Name, i.Math,i.Chi,i.Eng});
            chart1.DataSource = Q;
            chart1.Series.Clear();
            chart1.Series.Add(comboBox2.Text);
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.
                SeriesChartType.Column;
            foreach (var row in Q)
            {
                chart1.Series[0].Points.AddXY("Chi", row.Chi);
                chart1.Series[0].Points.AddXY("Math", row.Math);
                chart1.Series[0].Points.AddXY("Eng", row.Eng);
            }
        }
    }
    }
