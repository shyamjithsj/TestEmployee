using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMPLOYEE_MANAGEMENT.Models;

namespace EMPLOYEE_MANAGEMENT.Controllers
{
    public class apiController : Controller
    {
        DB_EMPLOYEESEntities db = new DB_EMPLOYEESEntities();

        public JsonResult checkEmployeeEmailDuplication(string email) {
            int count = db.tbl_employees.Where(k => k.emp_email == email).Count();
            if (count > 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false,JsonRequestBehavior.AllowGet);
            }
        }

        public class Employeedata
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string gender { get; set; }
            public string email { get; set; }
            public DateTime dob { get; set; }
            public string password { get; set; }
        }

        public class my_response
        {
            public string message { get; set; }
            public string status { get; set; }
        }

        public JsonResult addNewEmployee(Employeedata emp)
        {
            tbl_employees employee = new tbl_employees()
            {
                emp_first_name = emp.firstName,
                emp_last_name = emp.lastName,
                emp_email = emp.email,
                emp_gender = emp.gender,
                emp_password = emp.password,
                emp_dob = emp.dob,
                emp_status = 1
            };
            db.tbl_employees.Add(employee);
            int a = db.SaveChanges();
            if (a > 0)
            {
                my_response res = new my_response() {
                    message="Employee created successfully",
                    status="success"
                };
                return Json(res,JsonRequestBehavior.AllowGet);
            }
            else
            {
                my_response res = new my_response()
                {
                    message = "Employee creation failed.",
                    status = "error"
                };
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}