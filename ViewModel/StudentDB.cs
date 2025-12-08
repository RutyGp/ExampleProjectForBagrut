using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class StudentDB:PersonDB
    {
        public StudentList SelectAll()
    {
            command.CommandText = $"SELECT PersonTBL.*, StudentTbl.tel" +
                    $" FROM (PersonTBL INNER JOIN StudentTbl ON PersonTBL.id = StudentTbl.id)";
        StudentList sList = new StudentList(base.Select());
        return sList;
    }
    protected override BaseEntity CreateModel(BaseEntity entity)
    {
        Student s = entity as Student;
        s.Tel = reader["tel"].ToString();
        base.CreateModel(entity);
        return s;
    }
    public override BaseEntity NewEntity()
    {
        return new Student();
    }
    static private PersonList list = new PersonList();
    public static Person SelectById(int id)
    {
        PersonDB db = new PersonDB();
        list = db.SelectAll();

        Person g = list.Find(item => item.Id == id);
        return g;
    }

   
    protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
    {
        Student c = entity as Student;
        if (c != null)
        {
            string sqlStr = $"Insert INTO  StudentTbl (ID,Tel) VALUES (@id, @tel)";

            command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@id", c.Id));
                command.Parameters.Add(new OleDbParameter("@tel", c.Tel));
             
        }
    }

    protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
    {
        Student c = entity as Student;
        if (c != null)
        {
            string sqlStr = $"UPDATE StudentTbl  SET Tel=@tel WHERE ID=@id";

            command.CommandText = sqlStr;
            command.Parameters.Add(new OleDbParameter("@tel", c.Tel));
            command.Parameters.Add(new OleDbParameter("@id", c.Id));
        }
    }

    protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
    {
        Person c = entity as Person;
        if (c != null)
        {
            string sqlStr = $"DELETE FROM StudentTbl WHERE ID=@id";

            command.CommandText = sqlStr;
            command.Parameters.Add(new OleDbParameter("@id", c.Id));
        }
    }


        public override void Insert(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null & entity.GetType() == reqEntity.GetType())
            {
                inserted.Add(new ChangeEntity(base.CreateInsertdSQL, entity));
                inserted.Add(new ChangeEntity(this.CreateInsertdSQL, entity));
            }
        }



        public override void Update(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null & entity.GetType() == reqEntity.GetType())
            {
                updated.Add(new ChangeEntity(base.CreateUpdatedSQL, entity));
                updated.Add(new ChangeEntity(this.CreateUpdatedSQL, entity));
            }
        }
        public override void Delete(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null & entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
                deleted.Add(new ChangeEntity(base.CreateDeletedSQL, entity));
            }
        }



    }
}
