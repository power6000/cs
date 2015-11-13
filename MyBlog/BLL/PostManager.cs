using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class PostManager
    {
        private IRepository _repository;

        public PostManager() { }

        public PostManager(IRepository repository)
        {
            _repository = repository;
        }

        public List<Post> GetAllPosts()
        {
            List<PostDTO> allPosts = _repository.GetPosts();

            return allPosts.Select(postDTO => new Post()
                {
                    Body = postDTO.Body,
                    AddeDate = postDTO.AddedDate
                }
                ).ToList();
        }
    }
}
