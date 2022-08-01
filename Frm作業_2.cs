using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            productPhotoTableAdapter1.Fill(awDataSet1.ProductPhoto);
        }
        private void Fillin()
        {
            var Q = awDataSet1.ProductPhoto.Select(i => i.ModifiedDate.Year).Distinct();
            foreach (var item in Q)
            {
                cbx_year.Items.Add(item);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var Q = awDataSet1.ProductPhoto.Select(i => i);
            dataGridView1.DataSource = Q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var Q = awDataSet1.ProductPhoto.
                Where(i => i.ModifiedDate >= dateTimePicker1.Value && i.ModifiedDate <= dateTimePicker2.Value).
                Select(o => o);
            dataGridView1.DataSource = Q.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var Q = awDataSet1.ProductPhoto.
                Where(i => i.ModifiedDate.Year.ToString() == cbx_year.Text);
            dataGridView1.DataSource = Q.ToList();
        }

        private void Frm作業_2_Load(object sender, EventArgs e)
        {
            Fillin();
            int[] month = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            foreach (int i in month.Take(3).ToArray()) { dic.Add(i, "第一季"); }
            foreach (int i in month.Skip(3).Take(3).ToArray()) { dic.Add(i, "第二季"); }
            foreach (int i in month.Skip(6).Take(3).ToArray()) { dic.Add(i, "第三季"); }
            foreach (int i in month.Skip(9).Take(3).ToArray()) { dic.Add(i, "第四季"); }
        }
       public Dictionary<int, string> dic = new Dictionary<int, string>();
        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "") { comboBox2.Text = "第一季"; }
            var Q = from i in awDataSet1.ProductPhoto
                    where dic[i.ModifiedDate.Month] == comboBox2.Text
                    select i;
            dataGridView1.DataSource = Q.ToList();
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Byte[] bytes = (Byte[])dataGridView1.CurrentRow.Cells["LargePhoto"].Value;
            MemoryStream memoryStream = new MemoryStream(bytes);
            pictureBox1.Image = Image.FromStream(memoryStream);
            memoryStream.Close();
        }
    }
}
