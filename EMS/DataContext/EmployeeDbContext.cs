using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace EMS.Models
{
    public class EmployeeDbContext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public List<Employee> GetEmployees()
        {
            List<Employee> employeeList = new List<Employee>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spGetEmployees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Employee emp = new Employee();
                emp.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                emp.Compentency = Convert.ToInt32(dr.GetValue(1).ToString());
                emp.Name = (dr.GetValue(2).ToString());
                emp.Salary = Convert.ToInt32(dr.GetValue(3).ToString());
                emp.Department = (dr.GetValue(4).ToString());
                employeeList.Add(emp);
            }
            con.Close();
            return employeeList;
        }
        public List<Employee> GetEmployeeByDep(string name)
        {
            List<Employee> employee = new List<Employee>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spGetEmployeeByDep", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dep",name);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Employee emp=new Employee();
                emp.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                emp.Compentency = Convert.ToInt32(dr.GetValue(1).ToString());
                emp.Name = (dr.GetValue(2).ToString());
                emp.Salary = Convert.ToInt32(dr.GetValue(3).ToString());
                emp.Department = (dr.GetValue(4).ToString());
                employee.Add(emp);
            }
            con.Close();
            return employee;
        }
        public bool AddEmployee(Employee emp)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spAddEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@comp", emp.Compentency);
            cmd.Parameters.AddWithValue("@name", emp.Name);
            cmd.Parameters.AddWithValue("@salary", emp.Salary);
            cmd.Parameters.AddWithValue("@dep", emp.Department);
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
        public bool UpdateEmployee(Employee emp)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", emp.Id);
            cmd.Parameters.AddWithValue("@comp", emp.Compentency);
            cmd.Parameters.AddWithValue("@name", emp.Name);
            cmd.Parameters.AddWithValue("@salary", emp.Salary);
            cmd.Parameters.AddWithValue("@dep", emp.Department);
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
        public bool DeleteEmployee(int id)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
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