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
    public class ReplyService
    {
        private readonly Guid _userId;

        public ReplyService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateReply(ReplyCreate model)
        {
            var entity = new Reply()
            {
                AuthorId = _userId,
                Text = model.Content,
                CreatedUtc = DateTimeOffset.Now,
                CommentId = model.CommentId
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Replys.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ReplyListItem> GetReplys()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Replys
                    .Where(e => e.AuthorId == _userId)
                    .Select(e => new ReplyListItem
                    {
                        ReplyId = e.Id,
                        Content = e.Text,
                        CreatedUtc = e.CreatedUtc
                    });
                return query.ToArray();
            }
        }
        public ReplyDetail GetReplyById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Replys
                    .Single(e => e.Id == id && e.AuthorId == _userId);
                return new ReplyDetail
                {
                    ReplyId = entity.Id,
                    Content = entity.Text,
                    CreatedUtc = entity.CreatedUtc,
                    //ModifiedUtc = entity.ModifiedUtc
                };
            }
        }
        //public bool UpdateReply(ReplyEdit model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity =
        //            ctx
        //            .Replys
        //            .Single(e => e.Id == model.ReplyId && e.AuthorId == _userId);

        //        entity.Text = model.Content;
        //        //entity.ModifiedUtc = DateTimeOffset.UtcNow;

        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        public bool DeleteReply(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Replys
                    .Single(e => e.Id == commentId && e.AuthorId == _userId);

                ctx.Replys.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}