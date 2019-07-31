using RayPI.Entity;
using RayPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.IRepository
{
    public interface IStudentRepository:IBaseRepository<Student>
    {
        Student GetByName(string name);
    }
}
