using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yhteystiedotProjekti
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile("../../kuvat/minimoi.png");
            panel4.BackgroundImage = Image.FromFile("../../kuvat/sulje.png");
        }

        private void buttonSulje_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonMinimoi_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
