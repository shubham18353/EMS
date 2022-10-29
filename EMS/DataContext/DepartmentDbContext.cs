using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace EMS.Models
{
    public class DepartmentDbContext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public List<Department> GetDepartment()
        {
            List<Department> depList = new List<Department>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spGetDepartment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Department dep = new Department();
                dep.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                dep.Manager =(dr.GetValue(2).ToString());
                dep.Name = (dr.GetValue(1).ToString());
               
                depList.Add(dep);
            }
            con.Close();
            return depList;
        }
        //public Employee GetEmployeeByDep()
        //{
        //    Employee emp = new Employee();
        //    SqlConnection con = new SqlConnection(cs);
        //    SqlCommand cmd = new SqlCommand("spGetEmployeeByDep", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    con.Open();
        //    SqlDataReader dr = cmd.ExecuteReader();

        //    emp.Id = Convert.ToInt32(dr.GetValue(0).ToString());
        //    emp.Compentency = Convert.ToInt32(dr.GetValue(1).ToString());
        //    emp.Name = (dr.GetValue(2).ToString());
        //    emp.Salary = Convert.ToInt32(dr.GetValue(3).ToString());
        //    emp.Department = (dr.GetValue(4).ToString());


        //    con.Close();
        //    return emp;
        //}
        public bool AddDepartment(Department dep)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spAddDepartment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", dep.Id);
            cmd.Parameters.AddWithValue("@name", dep.Name);
            cmd.Parameters.AddWithValue("@manager", dep.Manager);
           
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 0)
                return false;
            else
            {
                return true;
            }
        }
        public bool UpdateDepartment(Department dep)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spUpdateDepartment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", dep.Id);
            cmd.Parameters.AddWithValue("@name", dep.Name);
            cmd.Parameters.AddWithValue("@manager", dep.Manager);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 0)
                return false;
            else
            {
                return true;
            }
        }
        public bool DeleteDepartment(int id)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spDeleteDepartment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 0)
                return false;
            else
            {
                return true;
            }
        }
    }
}