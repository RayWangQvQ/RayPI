using System;
using System.Collections.Generic;
using System.Text;
using RayPI.IRepository;
using RayPI.Entity;
using RayPI.Repository;
using RayPI.Model;

namespace RayPI.Bussiness.Client
{
    public class CStuentBussiness
    {
        private IStudentRepository iStuendt = new StudentRepository();
        public Student GetByName(string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new MyException("参数异常", 400);
            }
            return iStuendt.GetByName(name);
        }
    }
}
