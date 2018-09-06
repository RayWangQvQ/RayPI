using RayPI.Entity;
using RayPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.IRepository
{
    public interface IStudent:IBase<Student>
    {
        Student GetByName(string name);
    }
}
