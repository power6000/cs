using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using DTO;

namespace DAL
{
    public class UsersRepository : IUser
    {
        public SqlConnection connect = new SqlConnection();
        private string _stringConnection = @"Data Source=DESKTOP-2ABAPV0\LOCAL;
            Initial Catalog=MyBlog;Integrated Security=True;Pooling=False";

        public void OpenConnection()
        {
            connect.ConnectionString = _stringConnection;

            try
            {
                connect.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CloseConnection()
        {
            connect.Close();
        }

        public SqlParameter CmdParam(string parameterName, string value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.Value = value;
            param.SqlDbType = type;

            return param;
        }

        public SqlDataReader CmdDataReader(string strSql, SqlConnection connection)
        {
            SqlCommand myCommand = new SqlCommand(strSql, connection);
            SqlDataReader dr = myCommand.ExecuteReader();

            return dr;
        }

        public SqlDataReader CmdDataReader(string strSql, SqlConnection connection, SqlParameter paramSql)
        {
            SqlCommand myCommand = new SqlCommand(strSql, connection);
            myCommand.Parameters.Add(paramSql);
            SqlDataReader dr = myCommand.ExecuteReader();

            return dr;
        }

      

        public void DataReaderReadAdd(List<UserDTO> allUsers, SqlDataReader dr)
        {           

            while (dr.Read())
            {
                UserDTO tmpUser = new UserDTO();

                tmpUser.ID = (int)dr[0];
                tmpUser.FirstName = dr[1].ToString();
                tmpUser.LastName = dr[2].ToString();
                tmpUser.Email = dr[3].ToString();

                allUsers.Add(tmpUser);
            }
        }

        public List<UserDTO> GetAllUsers()
        {
            List<UserDTO> allUsers = new List<UserDTO>();

            this.OpenConnection();
 
            string strSQL = "Select * From Users Order BY LastName, FirstName";

            SqlDataReader dr = CmdDataReader(strSQL, this.connect);

            DataReaderReadAdd(allUsers, dr);

            this.CloseConnection();

            return allUsers;
        }

        public List<UserDTO> GetUser(string find)
        {
            List<UserDTO> allUsers = new List<UserDTO>();

            this.OpenConnection();

            string strSql = "Select * From Users Where (FirstName Like @find) OR (LastName Like @find) Order BY LastName, FirstName";

            SqlDataReader dr = CmdDataReader(strSql, this.connect, CmdParam("@find", "%" + find + "%", SqlDbType.NVarChar));

            DataReaderReadAdd(allUsers, dr);
          
            this.CloseConnection();

            return allUsers;
        }

        public void AddUserDto(UserDTO addUserDto)
        {
            this.OpenConnection();

            string sql = String.Format("Insert Into Users" +
                                       "(FirstName, LastName, Email) Values('{0}', '{1}', '{2}')",
                                       //"(FirstName, LastName, Email) Values(@FirstNam, @LastName, )",
                                       addUserDto.FirstName, addUserDto.LastName, addUserDto.Email);

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                cmd.Parameters.Add(CmdParam("@FirstName",addUserDto.FirstName, SqlDbType.NVarChar));

                cmd.Parameters.Add(CmdParam("@LastName", addUserDto.LastName, SqlDbType.NVarChar));

                cmd.Parameters.Add(CmdParam("@Email", addUserDto.Email, SqlDbType.NVarChar));

                cmd.ExecuteNonQuery();
            }
            
            this.CloseConnection();
        }

        public void DeleteUser(int idParam)
        {
            this.OpenConnection();

            string strSql = string.Format("Delete from Users where UserID = @UserId", idParam);

            SqlCommand cmd = new SqlCommand(strSql, this.connect);
            
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@UserID";
            param.Value = idParam;
            param.SqlDbType = SqlDbType.Int;

            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();

            this.CloseConnection();
        }

        public UserDTO EditUserQuery(int idParam)
        {
            UserDTO user = new UserDTO();

            this.OpenConnection();

            string strSql = String.Format("Select * From Users Where UserID = {0}", idParam);

            SqlCommand cmd = new SqlCommand(strSql, this.connect);

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@UserID";
            param.Value = idParam;
            param.SqlDbType = SqlDbType.Int;

            cmd.Parameters.Add(param);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                user.FirstName = dr[1].ToString();
                user.LastName = dr[2].ToString();
                user.Email = dr[3].ToString();
            }

            this.CloseConnection();

            return user;
        }

        public void EditUser(int idParam, string firstName, string lastName, string eMail)
        {
            this.OpenConnection();
            //  Марина' delete alltables  seelct * from User

            // declare @UserName varchar
            string strSql = "Update Users Set FirstName = @FName, LastName = @LName, Email = @Email " +
                                          "Where UserID = @Id";

            using (SqlCommand cmd = new SqlCommand(strSql, this.connect))
            {
                cmd.Parameters.Add(CmdParam("@FName", firstName, SqlDbType.NVarChar));

                cmd.Parameters.Add(CmdParam("@LName", lastName, SqlDbType.NVarChar));

                cmd.Parameters.Add(CmdParam("@Email", eMail, SqlDbType.NVarChar));

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = idParam;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
            }

            this.CloseConnection();
        }
    }
}
