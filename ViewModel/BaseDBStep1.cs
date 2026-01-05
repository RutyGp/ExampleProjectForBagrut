using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public abstract class BaseDB1
    {
        protected static string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\nirgo\\source\\repos\\Model\\ViewModel\\ExampleProject.accdb";


        //protected static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
        //              + System.IO.Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location
        //              + "/../../../../../VViewModel/ExampleProjectBagrutGrades.accdb");




        protected static OleDbConnection connection;
        protected OleDbCommand command;
        protected OleDbDataReader reader;
        ////public static string Path()
        ////{
        ////    String[] args = Environment.GetCommandLineArgs();
        ////    string s;
        ////    if (args.Length == 1)
        ////    {
        ////        s = args[0];
        ////    }
        ////    else
        ////    {
        ////        s = args[1];
        ////        s = s.Replace("/service:", "");
        ////    }
        ////    string[] st = s.Split('\\');
        ////    int x = st.Length - 6;
        ////    st[x] = "VViewModel";
        ////    Array.Resize(ref st, x + 1);
        ////    string str = String.Join('\\', st);
        ////    return str;
        ////}
        public BaseDB1()
        {
           
            connection ??= new OleDbConnection(connectionString);
            command = new OleDbCommand();
            command.Connection = connection;
        }

        public abstract BaseEntity NewEntity();



        protected List<BaseEntity> Select()
        {
            List<BaseEntity> list = new List<BaseEntity>();
            try
            {
                command.Connection = connection;
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BaseEntity entity = NewEntity();
                    list.Add(CreateModel(entity));
                }
            }
            catch (Exception e)
            {

                throw new Exception(
                    e.Message + "\nSQL:" + command.CommandText);
            }
            finally
            {
                if (reader != null) reader.Close();
                //   if (connection.State == ConnectionState.Open) connection.Close();
            }
            return list;
        }

      

        protected virtual BaseEntity CreateModel(BaseEntity entity)
        {
            entity.Id = (int)reader["id"];
            return entity;
        }


    
        

    }
}