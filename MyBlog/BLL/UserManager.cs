using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class UserManager
    {
        private IUser _user;

        public UserManager(IUser user)
        {
            _user = user;
        }

        public List<User> GetAllUsers()
        {
            List<UserDTO> allUsers = _user.GetAllUsers();
           
            return allUsers.Select(userDTO => new User()
            {
                ID = userDTO.ID,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email
            }).ToList();
        }

        public List<User> GetUser(string user)
        {
            List<UserDTO> allUsers = _user.GetUser(user);

            return allUsers.Select(userDTO => new User()
            {
                ID = userDTO.ID,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email
            }).ToList();

        }

        public void AddUser(string FirstName, string LastName, string Email)
        {
            UserDTO addUserDTO = new UserDTO()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };

            _user.AddUserDto(addUserDTO);
        }

        public void DeleteUser(int idParam)
        {
            _user.DeleteUser(idParam);
        }

        public User EditUserQuery(int idParam)
        {
            UserDTO userDto = _user.EditUserQuery(idParam);

             User user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                ID = idParam
            };

            return user;
        }

        public void EditUser(int idParam, string firstName, string lastName, string eMail)
        {
            _user.EditUser(idParam, firstName, lastName, eMail);
        }
    }
}
