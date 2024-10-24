﻿using Google.Protobuf.WellKnownTypes;
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


        public DataTable GetData(string strSelect, params (string, string)[] parameters)
        {
            DataTable dtTable = new DataTable();
            try
            {
                this.OpenConnection();

                using (MySqlCommand cmd = new MySqlCommand(strSelect, _sqlConnection))
                {
                    foreach ((string, string) parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                    }
                    using (MySqlDataAdapter daDataAdapter = new MySqlDataAdapter(cmd))
                    {
                        daDataAdapter.Fill(dtTable);
                    }
                }

                _sqlConnection.Close();
            }
            catch (Exception){ }

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


        public DataTable CallProcedure(string procedureName, params (string, string)[] parameters)
        {
            DataTable dtTable = new DataTable();
            try
            {
                this.OpenConnection();

                using (MySqlCommand cmd = new MySqlCommand(procedureName, _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số
                    foreach ((string, string) parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                    }

                    using (MySqlDataAdapter daDataAdapter = new MySqlDataAdapter(cmd))
                    {
                        daDataAdapter.Fill(dtTable);
                    }
                }

                _sqlConnection.Close();
            }
            catch (Exception) { }

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
