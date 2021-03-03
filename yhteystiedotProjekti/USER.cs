using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace yhteystiedotProjekti
{
    class USER
    {

        MY_DB db = new MY_DB();

        public bool kayttajanimiExists(string kayttajanimi)
        {
            string query = "SELECT * FROM `kayttaja` WHERE `käyttäjänimi`=@un";

            MySqlCommand command = new MySqlCommand(query, db.getConnection);

            command.Parameters.Add("@un", MySqlDbType.VarChar).Value = kayttajanimi;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);
            // jos käittäjänimi on olemassa niin silloin return on true
            if(table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //sijoitan uuden käyttäjän
        public bool insertUser(string etunimi, string sukunimi, string kayttajanimi, string salasana, MemoryStream kuva)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `kayttaja`(`etunimi`, `sukunimi`, `käyttäjänimi`, `salasana`, `kuva`) VALUES (@fn,@ln,@un,@pass,@pic)", db.getConnection);

            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = etunimi;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = sukunimi;
            command.Parameters.Add("@un", MySqlDbType.VarChar).Value = kayttajanimi;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = salasana;
            command.Parameters.Add("@pic", MySqlDbType.Blob).Value = kuva.ToArray();


            db.avaaConnection();
            if(command.ExecuteNonQuery() == 1)
            {
                db.suljeConnection();
                return true;
            }
            else
            {
                db.suljeConnection();
                return false;
            }

        }
    }
}
