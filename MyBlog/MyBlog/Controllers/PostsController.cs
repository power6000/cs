using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using DTO;
using MyBlog.Models.Posts;

namespace MyBlog.Controllers
{
    public class PostsController : Controller
    {
        //
        // GET: /Posts/
        public ActionResult Index()
        {
            var postManager=new PostManager();

            var model = new IndexModel();

            model.Posts = postManager.GetAllPosts();

            return View(model);
        }
	}
}