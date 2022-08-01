using MyHomeWork;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Frm作業_1 FRM = new Frm作業_1();

            FRM.MdiParent = this;
            FRM.WindowState = FormWindowState.Maximized;

            FRM.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Frm作業_2 FRM = new Frm作業_2();

            FRM.MdiParent = this;
            FRM.WindowState = FormWindowState.Maximized;

            FRM.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Frm作業_3 FRM = new Frm作業_3();

            FRM.MdiParent = this;
            FRM.WindowState = FormWindowState.Maximized;

            FRM.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Frm作業_4 FRM = new Frm作業_4();

            FRM.MdiParent = this;
            FRM.WindowState = FormWindowState.Maximized;

            FRM.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            while (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
