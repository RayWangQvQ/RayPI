using System;
using System.Collections.Generic;
using System.Text;
using RayPI.IRepository;
using RayPI.Entity;
using RayPI.Model;

namespace RayPI.Bussiness.Client
{
    public class CStuentBussiness
    {
        private IStudentRepository _studentRepository;
        public CStuentBussiness(IStudentRepository stuendt)
        {
            _studentRepository = stuendt;
        }

        public Student GetByName(string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new MyException("参数异常", 400);
            }
            return _studentRepository.GetByName(name);
        }
    }
}
