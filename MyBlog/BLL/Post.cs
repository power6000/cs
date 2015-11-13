using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Post
    {
        private IRepository _repository;
        public string Body { get; set; }
        public DateTime AddeDate { get; set; }

        public Post() { }

        public Post(IRepository repository)
        {
            _repository = repository;
        }

        public void CreatePost()
        {
            _repository.SavePost(Body, AddeDate);
        }

    }
}
