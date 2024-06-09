using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace FireFight.Functions
{
    public class DBFunctions
    {
        public SqlConnection ArmouryConnection;
        public SqlConnection CharactersConnection;
        public SqlConnection DataTableConnection;

        private string Datalocation
        {
            get
            {
                return System.IO.Directory.GetCurrentDirectory() + @"\Data\";
            }
        }

        public DBFunctions()
        {
            DataTableConnection = ConnectToMDF("DataTables");
            ArmouryConnection = ConnectToMDF("RangedWeapons");
            CharactersConnection = ConnectToMDF("Characters");
        }

        private SqlConnection ConnectToMDF(string MDFName)
        {
            string sqlCon = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
            @"AttachDbFilename=" + Datalocation + MDFName + @".mdf;
            Integrated Security=True";

            return new SqlConnection(sqlCon);
        }

        public DataTable RunSQLStatementDT(SqlConnection Connection, string strSQLQuery)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand(strSQLQuery, Connection);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            Connection.Close();
            return dt;
        }

        //stored proc work

        public void RunStoredProc(SqlConnection Connection, string procname, SqlParameterCollection parameters)
        {
            Connection.Open();
            using (SqlCommand cmd = new SqlCommand(procname, Connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }

                Connection.Open();
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
        }

        public string GetNearestValue(DataTable ToSearch, string ToFind)
        {
            string[] columnNames = ToSearch.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();

            foreach (string col in columnNames)
            {
                if (GeneralFunctions.IsNumeric(col) == true)
                {
                    int number = Convert.ToInt16(col);
                    int intToFind = Convert.ToInt16(ToFind);

                    if ((intToFind -= number) <= 0)
                        return number.ToString();
                }
            }
            return "Not Found";
        }

        ~DBFunctions()
        {
            if (ArmouryConnection.State != ConnectionState.Closed) ArmouryConnection.Close();
            if (CharactersConnection.State != ConnectionState.Closed) CharactersConnection.Close();
            if (DataTableConnection.State != ConnectionState.Closed) DataTableConnection.Close();
        }
    }
}