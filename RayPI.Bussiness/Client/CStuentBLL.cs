using System;
using System.Collections.Generic;
using System.Text;
using RayPI.IRepository;
using RayPI.Entity;
using RayPI.Repository;

namespace RayPI.Bussiness.Client
{
    public class CStuentBLL
    {
        private IStudent iStuendt = new StudentDAL();
        public Student GetByName(string name)
        {
            return iStuendt.GetByName(name);
        }
    }
}
