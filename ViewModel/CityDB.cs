using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CityDB : BaseDB
    {
        public CityList SelectAll()
        {
            command.CommandText = $"SELECT * FROM CityTbl";
            CityList groupList = new CityList(base.Select());
            return groupList;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            City ct = entity as City;
            ct.CityName = reader["cityName"].ToString();
            base.CreateModel(entity);
            return ct;
        }
        public override BaseEntity NewEntity()
        {
            return new City();
        }
        static private CityList list = new CityList();

        public static City SelectById(int id)
        {
            CityDB db = new CityDB();
            list = db.SelectAll();

            City g = list.Find(item => item.Id == id);
            return g;
        }

        //שלב ב
        //protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        //{
        //    City c = entity as City;
        //    if (c != null)
        //    {
        //        string sqlStr = $"DELETE FROM CityTbl where id=@pid";

        //        command.CommandText = sqlStr;
        //        command.Parameters.Add(new OleDbParameter("@pid", c.Id));
        //    }
        //}
        //protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        //{
        //    City c = entity as City;
        //    if (c != null)
        //    {
        //        string sqlStr = $"Insert INTO  CityTbl (CityName) VALUES (@cName)";

        //        command.CommandText = sqlStr;
        //        command.Parameters.Add(new OleDbParameter("@cName", c.CityName));
        //    }
        //}

        //protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        //{
        //    City c = entity as City;
        //    if (c != null)
        //    {
        //        string sqlStr = $"UPDATE CityTbl  SET CityName=@cName WHERE ID=@id";

        //        command.CommandText = sqlStr;
        //        command.Parameters.Add(new OleDbParameter("@cName", c.CityName));
        //        command.Parameters.Add(new OleDbParameter("@id", c.Id));
        //    }
        //}
    }
}
