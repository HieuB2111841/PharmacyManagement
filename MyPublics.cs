using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLNhaThuoc
{
    /// <summary>
    ///     Lớp này dùng để kết nối đến csdl mySQL "quanlynhathuoc".
    ///     <br/>
    ///     Sử dụng MyPublics.Instance để truy cập đến đối tượng kết nối đến csdl.
    /// </summary>
    public class MyPublics
    {
        private static MyPublics _instance = new MyPublics();

        /// <summary>
        ///     Đối tượng kết nối đến csdl
        /// </summary>
        public static MyPublics Instance => _instance;

        private MySqlConnection _sqlConnection;

        private string _server = "localhost";
        private string _database = "quanlynhathuoc";
        private string _uid = "root";
        private string _password = "Hieub2111841";

        /// <summary>
        ///     Tránh khởi tạo đối tượng MyPublics ở nơi khác
        /// </summary>
        private MyPublics() { }


        public bool ConnectDatabase() => ConnectDatabase(out string _);
        public bool ConnectDatabase(out string message)
        {
            
            string connectionString = $"Server={_server};Database={_database};Uid={_uid};Pwd={_password};";
            _sqlConnection = new MySqlConnection();
            _sqlConnection.ConnectionString = connectionString;
            try
            {
                _sqlConnection.Open();
                message = "Success";
                return true;
            }
            catch (Exception e)
            {
                message = "Error: " + e.Message;
            }
            _sqlConnection.Close();
            return false;
        }


        public DataTable GetData(string strSelect) => GetData(strSelect, out string _);
        public DataTable GetData(string strSelect, out string message)
        {
            DataTable dtTable = new DataTable();
            try
            {
                this.OpenConnection();

                using (MySqlCommand cmd = new MySqlCommand(strSelect, _sqlConnection))
                {
                    using (MySqlDataAdapter daDataAdapter = new MySqlDataAdapter(cmd))
                    {
                        daDataAdapter.Fill(dtTable);
                    }
                }

                message = "Success";
                _sqlConnection.Close();
            }
            catch (Exception e) 
            {
                message = "Error: " + e.Message;
            }

            return dtTable;
        }

        public DataTable GetDataWithParameter(string strSelect, string value)
            => GetDataWithParameter(strSelect, value, out string _);
        public DataTable GetDataWithParameter(string strSelect, string value, out string message)
        {
            DataTable dtTable = new DataTable();
            try
            {
                this.OpenConnection();

                using (MySqlCommand cmd = new MySqlCommand(strSelect, _sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@Value", value);
                    using (MySqlDataAdapter daDataAdapter = new MySqlDataAdapter(cmd))
                    {
                        daDataAdapter.Fill(dtTable);
                    }
                }

                message = "Success";
                _sqlConnection.Close();
            }
            catch (Exception e)
            {
                message = "Error: " + e.Message;
            }

            return dtTable;
        }


        public DataSet GetDataSet(string strSelect, string strTableName)
            => GetDataSet(strSelect, strTableName, out string _);
        public DataSet GetDataSet(string strSelect, string strTableName, out string message)
        {
            DataSet dsDatabase = new DataSet();
            try
            {
                this.OpenConnection();

                using (MySqlCommand cmd = new MySqlCommand(strSelect, _sqlConnection))
                {
                    using (MySqlDataAdapter daDataAdapter = new MySqlDataAdapter(cmd))
                    {
                        daDataAdapter.Fill(dsDatabase, strTableName);
                    }
                }

                message = "Success";
                _sqlConnection.Close();
            }
            catch (Exception e)
            {
                message = "Error: " + e.Message;
            }

            return dsDatabase;
        }

        public bool PrimaryKeyExists(string value, string fieldName, string tableName)
        {
            bool isExist = false;
            try
            {
                this.OpenConnection();

                string sqlSelect = $"SELECT 1 FROM {tableName} WHERE {fieldName} = @Value";
                using (MySqlCommand cmdCommand = new MySqlCommand(sqlSelect, _sqlConnection))
                {
                    cmdCommand.Parameters.AddWithValue("@Value", value);
                    using (MySqlDataReader drReader = cmdCommand.ExecuteReader())
                    {
                        if (drReader.HasRows)
                        {
                            isExist = true;
                        }
                    }
                }

                _sqlConnection.Close();
            }
            catch (Exception) { }

            return isExist;
        }

        public DataTable CallProcedure(string procedureName)
            => CallProcedure(procedureName, out string _);
        public DataTable CallProcedure(string procedureName, out string message)
        {
            DataTable dtTable = new DataTable();
            try
            {
                this.OpenConnection();

                using (MySqlCommand cmd = new MySqlCommand(procedureName, _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (MySqlDataAdapter daDataAdapter = new MySqlDataAdapter(cmd))
                    {
                        daDataAdapter.Fill(dtTable);
                    }
                }

                message = "Success";
                _sqlConnection.Close();
            }
            catch (Exception e)
            {
                message = "Error: " + e.Message;
            }

            return dtTable;
        }


        private void OpenConnection()
        {
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }
        }
    }
}
