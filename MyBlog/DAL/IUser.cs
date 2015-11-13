using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public interface IUser
    {
        void OpenConnection();

        void CloseConnection();

        List<UserDTO> GetAllUsers();

        List<UserDTO> GetUser(string user);

        void AddUserDto(UserDTO addUserDto);

        void DeleteUser(int idParam);

        UserDTO EditUserQuery(int idParam);
        void EditUser(int idParam, string firstName, string lastName, string eMail);
    }
}
