using LouigisSP.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LouigisSP.DL;

namespace LouigisSP.SL
{
    public  class EmployeeOperations
    {

        public List<Employee> getEmployees() {
            EmployeeDAL empDAL = new EmployeeDAL();
           return  empDAL.getAllEmployees();
        
        }
    }
}
