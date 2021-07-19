using _24HourAPI.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    class PostService
    {
        private readonly Guid _userId;

        public PostService(Guid UserId)
        {
            _userId = UserId;
        }

        public bool CreatePost(CreatePost model)
        {
            var entity = new Post() { AuthorId = _userId, Title = model.Title, Text = model.Content };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() == 1;

            }
        }
        public IEnumerable<PostListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.
                    Posts
                    .Where(q => q.AuthorId == _userId)
                    .Select(
                        q => 
                        new PostListItem { PostId = q.Id, Title = q.Title });
                return query.ToArray();
            }
        }
    }
}
