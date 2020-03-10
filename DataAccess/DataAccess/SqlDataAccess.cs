using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLibrary.DataAccess
{
    public class SqlDataAccess
    {
        private string _connectionString;
        public SqlDataAccess(string connectionString) {
            _connectionString = connectionString;
        }   
        
        public string GetConnectionString(string connectionName = "ForceDB") {
            return _connectionString;
        }

        public List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cn = new SqlConnection(GetConnectionString()))
            {
                return cn.Query<T>(sql).ToList();
            }
        }

        public int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cn = new SqlConnection(GetConnectionString()))
            {
                return cn.ExecuteScalar<int>(sql, data);
            }
        }
    }
}
