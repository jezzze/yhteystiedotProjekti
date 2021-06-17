using System;
using System.Data;

namespace yhteystiedotProjekti
{
    internal class MySqlDataAdapter
    {
        public MySqlDataAdapter()
        {
        }

        public MySqlDataAdapter(MySqlCommand command)
        {
        }

        public MySqlCommand SelectCommand { get; internal set; }

        internal void Fill(DataTable table)
        {
            throw new NotImplementedException();
        }
    }
}