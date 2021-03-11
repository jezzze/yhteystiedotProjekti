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
using MySql.Data.MySqlClient;

namespace yhteystiedotProjekti
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        MY_DB mydb = new MY_DB();

        private void MainForm_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile("../../kuvat/minimoi.png");
            panel4.BackgroundImage = Image.FromFile("../../kuvat/sulje.png");

            getkuvajakayttajanimi();
        }

        private void buttonSulje_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonMinimoi_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        //teen function joka näyttää kirjautuneen kuvan ja käyttäjänimen
        public void getkuvajakayttajanimi()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `kayttaja` WHERE `id`=@uid", mydb.getConnection);

            command.Parameters.Add("@uid", MySqlDbType.Int32).Value = Globals.GlobalkayttajaId;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                //näyttää käyttäjän kuvan
                byte[] pic = (byte[])table.Rows[0]["kuva"];
                MemoryStream kuva = new MemoryStream(pic);
                pictureBox1.Image = Image.FromStream(kuva);

                //näyttää käyttäjänimen
                label1.Text = "Tervetuloa ( " + table.Rows[0]["kayttajanimi"] + " )";
            }
        }

        
    }
}
