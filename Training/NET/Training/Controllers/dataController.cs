using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Helpers;
using System.Web.Http;
using Training.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Training.Controllers
{

    public class dataController : ApiController
    {

        [Route("showData")]
        [HttpGet]
        public ResponseResult ShowData()
        {
            ResponseResult obj = new ResponseResult();
            obj.Data = "Welcome to Net Solutions!";
            obj.Message = "Data displayed successfully";
            obj.Status = (int)HttpStatusCode.OK;
            return obj;
        }



        [Route("getAllUsers")]
        [HttpGet]
        public List<UserDetails> getAllUsers()
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            List<UserDetails> userList = new List<UserDetails>();
           

            string query = "SELECT * from userDetails";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            IDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                UserDetails obj = new UserDetails();
                obj.name = data["name"].ToString();
                obj.email = data["email"].ToString();
                obj.id = Convert.ToInt32(data["Id"]);
                userList.Add(obj);
            }

            con.Close();
            return userList;

        }


        [Route("getUser/{userId}")]
        [HttpGet]
        public UserDetails getUser(int userId)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            UserDetails obj = new UserDetails();

            string query = "SELECT * from userDetails WHERE Id = '" + userId + "'";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            IDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                obj.name = data["name"].ToString();
                obj.email = data["email"].ToString();
                obj.id = Convert.ToInt32(data["Id"]);
            }
            con.Close();
            return obj;
        }


        [Route("postUserDetail")]
        [HttpPost]
        public UserDetails PostUserDetail(UserDetails userDetails)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);         

            UserDetails obj = new UserDetails();
            obj.name = userDetails.name;
            obj.email = userDetails.email;

            string query = "INSERT INTO userDetails(name, email) values ('" + obj.name + "','" + obj.email + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                obj.status = (int)HttpStatusCode.OK;
                return obj;
            }
            else
            {
                obj.status = (int)HttpStatusCode.Conflict;
                return obj;
            }

        }






        [Route("deleteUser/{userId}")]
        [AllowAnonymous]
        [HttpDelete]
        public HttpResponseMessage deleteUser(int userId)
        {
            UserDetails obj = new UserDetails();
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = "DELETE FROM userDetails Where Id = '" + userId + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return Request.CreateResponse<string>(HttpStatusCode.OK, userId.ToString());
        }


        [Route("updateUser")]
        [HttpPut]
        public UserDetails upateUserDetails(UserDetails userDetails)
        {
            UserDetails obj = new UserDetails();
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString; 
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            //This is my update query in which i am taking input from the user through windows forms and update the record.  
            string Query = "update userDetails set name='" + userDetails.name + "',email='" + userDetails.email + "' where Id='" + userDetails.id + "';";

            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader MyReader2;
            MyReader2 = cmd.ExecuteReader();
            while (MyReader2.Read())
            {
            }

            con.Close();
            return userDetails;

        }



    }

}

        
    



