using RayPI.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.IBussiness.Client
{
    interface ICStudentBussiness
    {
        Student GetByName(string name = null);
    }
}
