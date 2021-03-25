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
        ryhma ryhma = new ryhma();

        private void MainForm_Load(object sender, EventArgs e)
        {// nämä on sulkemis ja minimointi kuvat
            panel3.BackgroundImage = Image.FromFile("../../kuvat/minimoi.png");
            panel4.BackgroundImage = Image.FromFile("../../kuvat/sulje.png");

            getkuvajakayttajanimi();

            // boxi näyttää ryhmän nimet
            getGroups();
        }
        // sulkee ohjelman
        private void buttonSulje_Click(object sender, EventArgs e) //tämä button sulkee ohjelman
        {
            Close();
        }
        // minimoi ohjelman
        private void buttonMinimoi_Click(object sender, EventArgs e) // tämä button minimoi ohjelman
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

            if (table.Rows.Count > 0)
            {
                //näyttää käyttäjän kuvan
                byte[] pic = (byte[])table.Rows[0]["kuva"];
                MemoryStream kuva = new MemoryStream(pic);
                pictureBox1.Image = Image.FromStream(kuva);

                //näyttää käyttäjänimen
                kayttajanimi.Text = "Tervetuloa ( " + table.Rows[0]["kayttajanimi"] + " )";
            }
        }
        // muokkaa kayttäjää
        private void labelMuokkaakayttaja_Click(object sender, EventArgs e)
        {
            Muokkaa_Kayttaja_Data_Form muokkaaKayttajaForm = new Muokkaa_Kayttaja_Data_Form();
            muokkaaKayttajaForm.Show(this);
        }

        
        private void label1_Click(object sender, EventArgs e)
        {

        }
        //lisää uuden ryhmän
        private void buttonLisaaRyhma_Click(object sender, EventArgs e)
        {
            string ryhmannimi = textBoxLisaaRyhmanNimi.Text;

            if (!ryhmannimi.Trim().Equals(""))
            {
                if (!ryhma.groupExists(ryhmannimi, "add", Globals.GlobalkayttajaId))
                {
                    if (ryhma.InsertGroup(ryhmannimi, Globals.GlobalkayttajaId))

                    {
                        MessageBox.Show("new group Inserted", "Lisaaryhma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("group not Inserted", "Lisaaryhma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    getGroups();

                }
                else
                {
                    MessageBox.Show("this Group name akready exists", "Lisaaryhma", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Enter a Group name before inserting", "Lisaaryhma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        

        //
        public void getGroups()
        {
            //ryhmän muokkaus
            comboBoxEditgroupid.DataSource = ryhma.getGroups(Globals.GlobalkayttajaId);
            comboBoxEditgroupid.DisplayMember = "nimi";
            comboBoxEditgroupid.ValueMember = "id";
            
            //ryhmän poisto
            comboBoxRemovegroupid.DataSource = ryhma.getGroups(Globals.GlobalkayttajaId);
            comboBoxRemovegroupid.DisplayMember = "nimi";
            comboBoxRemovegroupid.ValueMember = "id";
        }
        // muokkaa ryhmän nimi
        private void buttonEditGroup_Click(object sender, EventArgs e)
        {
            string newGroupName = textBoxEditGroupName.Text;
            int groupId = Convert.ToInt32(comboBoxEditgroupid.SelectedValue.ToString());

            if (!newGroupName.Trim().Equals(""))
            {
                if (!group.groupExists(newGroupName, "edit", Globals.GlobalkayttajaId, groupId))
                {
                    if (groupId.updateGroup(groupId, newGroupName))
                    {
                        MessageBox.Show("ryhmän nimi päivitetty", "edit group", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ryhmän nimi päivitetty", "edit group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    getGroups();
                }
                else
                {
                    MessageBox.Show("ryhmän nimi on jo olemassa", "edit group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("anna ryhmän nimi ennen muokkamistay", "edit group", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // poistaa valitun ryhmän
        private void buttonRemoveGroup_Click(object sender, EventArgs e)
        {
            int groupid = Convert.ToInt32(comboBoxRemoveGroupId.SelectedValue.ToString());

            if(MessageBox.Show("oletko varma että haluat poistaa tämän ryhmän","remove group", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(group.deleteGroup(groupid))
                {
                    (MessageBox.Show("ryhmä poistettu", "remove group", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    (MessageBox.Show("ryhmää ei poistettu", "remove group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                getGroups();
            }
        }
        //päivitä käyttjän kuva ja nimi
        private void labelRefresh_Click(object sender, EventArgs e)
        {
            getkuvajakayttajanimi();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxLisaaRyhmanNimi_TextChanged(object sender, EventArgs e)
        {

        }
    }       
}
