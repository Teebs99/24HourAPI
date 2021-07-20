using _24HourAPI.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PostService
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
        public IEnumerable<PostListItem> GetPosts()
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
        public PostDetail GetPost(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Posts
                    .Single(q => q.Id == id && q.AuthorId == _userId);
                return new PostDetail()
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Content = entity.Text,
                    Comments = entity.Comments
                };
            }
        }
        public bool UpdatePost(PostEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Posts
                    .Single(q => q.Id == model.Id && q.AuthorId == _userId);
                entity.Title = model.Title;
                entity.Text = model.Content;

                return ctx.SaveChanges() == 1;

            }
        }
        public bool DeletePost(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Posts.Single(e => e.Id == id && e.AuthorId == _userId);
                ctx.Posts.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
