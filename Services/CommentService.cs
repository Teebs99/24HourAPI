using _24HourAPI.Models;
using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CommentService
    {
        private readonly Guid _userId;

        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(CommentCreate model)
        {
            var entity = new Comment()
            {
                AuthorId = _userId,
                Text = model.Content,
                CreatedUtc = DateTimeOffset.Now
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CommentListItem> GetComments()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Comments
                    .Where(e => e.AuthorId == _userId)
                    .Select(e => new CommentListItem
                    {
                        CommentId = e.Id,
                        Content = e.Text,
                        CreatedUtc = e.CreatedUtc
                    });
                return query.ToArray();
            }
        }
        public CommentDetail GetCommentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Comments
                    .Single(e => e.Id == id && e.AuthorId == _userId);
                return new CommentDetail
                {
                    CommentId = entity.Id,
                    Content = entity.Text,
                    CreatedUtc = entity.CreatedUtc,
                    //ModifiedUtc = entity.ModifiedUtc
                };
            }
        }
        public bool UpdateComment(CommentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.Id == model.CommentId && e.AuthorId == _userId);

                entity.Text = model.Content;
                //entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteComment(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Comments
                    .Single(e => e.Id == commentId && e.AuthorId == _userId);

                ctx.Comments.Remove(entity);

                return ctx.SaveChanges() == 1; 
            }
        }

    }
}
