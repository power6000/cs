using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using BLL;
using DAL;
using MyBlog.Models.Users;

namespace MyBlog.Controllers
{
    public class UsersController : Controller
    {
        private UserManager _usersManager;

        public UsersController()
        {
            _usersManager = new UserManager(new UsersRepository());
        }

        public ActionResult PrintAllUsers()
        {         
            var model = new UserModel();

            model.AllUsers = _usersManager.GetAllUsers();

            return View(model);
        }


        public ActionResult FindUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindUser(UserModel user)
        {
            user.AllUsers = _usersManager.GetUser(user.Find);

            return PartialView("FoundUsers", user);
        }

        [HttpPost]
        public ActionResult AddUser(UserModel userModel)
        {
            _usersManager.AddUser(userModel.FirstName, userModel.LastName, userModel.Email);

            var model = new UserModel();

            model.AllUsers = _usersManager.GetAllUsers();

            return PartialView("FoundUsers", model);

        }

       [HttpPost]
        public ActionResult DeleteUser(int idParam)
        {
            _usersManager.DeleteUser(idParam);

            return Json(new { id = idParam, ok = true, message="User was deleted"});         
        }


        [HttpPost]
        public ActionResult EditUserQuery(int idParam)
        {
            User user = new User();

            user = _usersManager.EditUserQuery(idParam);

            return Json(new
            {
                id = idParam, firstName = user.FirstName,
                lastName = user.LastName, 
                eMail = user.Email,
                ok = true,
            });
        }


        public ActionResult EditUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditUser(UserModel userModel)
        {
            _usersManager.EditUser(userModel.ID, userModel.FirstName, userModel.LastName, userModel.Email);

            userModel.AllUsers = _usersManager.GetAllUsers();

            return PartialView("FoundUsers", userModel);
        }

        public ActionResult UserManagement()
        {
            var userModel = new UserModel();

            userModel.AllUsers = _usersManager.GetAllUsers();

            return View(userModel);
        }

    }
}