using RayPI.Entity;
using RayPI.IRepository;
using RayPI.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Repository
{
    public class StudentDAL : BaseDAL<Student>, IStudent
    {
        public Student GetByName(string name)
        {
            return db.Queryable<Student>().Where(it => it.Name == name).First();
        }
    }
}
