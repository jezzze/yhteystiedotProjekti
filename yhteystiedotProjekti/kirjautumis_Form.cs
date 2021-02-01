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
    public partial class kirjautumis_Form : Form
    {
        public kirjautumis_Form()
        {
            InitializeComponent();
        }

        private void kirjautumis_Form_Load(object sender, EventArgs e)
        {
            //lisään tässä kaksi kuvaa panel3een ja panel4jään
            panel3.BackgroundImage = Image.FromFile("../../kuvat/minimoi.png");
            panel4.BackgroundImage = Image.FromFile("../../kuvat/sulje.png");
        }

        //registeröinti nappi
        private void button_registeroidy_Click(object sender, EventArgs e)
        {

        }

        //selaa nappi
        private void button_selaa_Click(object sender, EventArgs e)
        {

        }

        //label mene rekisteröinti kohtaan
        private void labelMeneRek_Click(object sender, EventArgs e)
        {
            timer1.Start();
            labelMeneRek.Enabled = false;
            labelMeneKirjaut.Enabled = false;
        }

        //label mene kirjautumis kohtaan
        private void labelMeneKirjaut_Click(object sender, EventArgs e)
        {

        }

        //nappi joka sulkee
        private void buttonSulje_Click(object sender, EventArgs e)
        {
            Close();
        }

        //nappi joka minimoi
        private void buttonMinimoi_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //kun tämä timer alkaa niin näytämme vain rekisteröinti kohdan
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(panel2.Location.X > -289)
            {
                panel2.Location = new Point(panel2.Location.X - 1, panel2.Location.Y);
            }
            else
            {
                timer1.Stop();
                labelMeneKirjaut.Enabled = true;
                labelMeneRek.Enabled = true;
            }
        }

        //kun tämä timer alkaa niin näytämme vain kirjautumis kohdan
        private void timer2_Tick(object sender, EventArgs e)
        {

        }
    }
}
