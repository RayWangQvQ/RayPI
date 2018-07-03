using System;
using System.Collections.Generic;
using System.Text;
using RayPI.IService;
using RayPI.Entity;
using RayPI.Service;

namespace RayPI.Bussiness.Client
{
    public class CStuentBLL
    {
        private IStudent iService = new StudentService();
        public Student GetByName(string name)
        {
            return iService.GetByName(name);
        }
    }
}
