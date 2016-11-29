using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastSQL.BaseModel;
using FastSQL.Exceptions;

namespace FastSQL
{
    public class StoredProcedure
    {
        public static SqlConnection DefaultConnection { get; set; }
        protected SqlDataAdapter _adapt = new SqlDataAdapter();
        protected SqlConnection _con = new SqlConnection();
        public SqlConnection Connection
        {
            get
            {
                if(DefaultConnection != null)
                    return DefaultConnection;
                return _con;
            }
            set
            {
                if (DefaultConnection == null)
                    _con = value;
                else
                {
                    throw new ConnectionAlreadyAssingedException();
                }
            }
        }

        protected StoredProcedure() { }

        public StoredProcedure(string name, params SqlParameter[] args)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = name,
                Connection = Connection
            };
            if (args.Length > 0)
                cmd.Parameters.AddRange(args);
            _adapt.SelectCommand = cmd;
        }

        public StoredProcedure(string connectionString, string name, params SqlParameter[] args)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = name,
                Connection = new SqlConnection(connectionString)
            };
            if (args.Length > 0)
                cmd.Parameters.AddRange(args);
            _adapt.SelectCommand = cmd;
        }
        
        public StoredProcedure(ConnectionString connectStr, string name, params SqlParameter[] args)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = name,
                Connection = new SqlConnection(connectStr.GetConnectionString())
            };
            if (args.Length > 0)
                cmd.Parameters.AddRange(args);
            _adapt.SelectCommand = cmd;
        }

        public DataSet ExecuteToDataSet()
        {
            try
            {
                Connection.Open();
                DataSet ds = new DataSet();
                _adapt.Fill(ds);
                Connection.Close();
                return ds;
            }
            catch (Exception)
            {
                Connection.Close();
                throw;
            }
        }
        
        public List<T> ExecuteToList<T>() where T:TableModelBase,new()
        {
            try
            {
                Connection.Open();
                DataSet ds = new DataSet();
                List<T> res = new List<T>();
                _adapt.Fill(ds);
                Connection.Close();
                if (ds?.Tables?.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        T t = new T();
                        t.Parse(row);
                        res.Add(t);
                    }
                }
                return res;
            }
            catch (Exception)
            {
                Connection.Close();
                throw;
            }
        }

        public object ExecuteSingleObject()
        {
            object obj;
            try
            {
                Connection.Open();
                obj = _adapt.SelectCommand.ExecuteScalar();
                Connection.Close();
            }
            catch (Exception)
            {
                Connection.Close();
                throw;
            }
            return obj;
        }

        public void Execute()
        {
            try
            {
                _adapt.SelectCommand.Connection.Open();
                _adapt.SelectCommand.ExecuteNonQuery();
                _adapt.SelectCommand.Connection.Close();
            }
            catch (Exception)
            {
                Connection.Close();
                throw;
            }
        }
    }

    public class StoredProcedure<TP>:StoredProcedure where TP : ParameterModelBase,new()
    {
        public StoredProcedure(string name, TP paramModel)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = name,
                Connection = Connection
            };
            if (paramModel != null)
                cmd.Parameters.AddRange(paramModel.GetParameters().ToArray());
            _adapt.SelectCommand = cmd;
        }

        public StoredProcedure(string connectStr, string name, TP paramModel)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = name,
                Connection = new SqlConnection(connectStr)
            };
            if (paramModel != null)
                cmd.Parameters.AddRange(paramModel.GetParameters().ToArray());
            _adapt.SelectCommand = cmd;
        }

        public StoredProcedure(ConnectionString connectStr,string name, TP paramModel)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = name,
                Connection = new SqlConnection(connectStr.GetConnectionString())
            };
            if (paramModel != null)
                cmd.Parameters.AddRange(paramModel.GetParameters().ToArray());
            _adapt.SelectCommand = cmd;
        }
    }
}
