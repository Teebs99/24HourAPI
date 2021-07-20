using Microsoft.AspNet.Identity;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _24HourAPI.Controllers
{
    [Authorize]
    public class ReplyController : ApiController
    {
        private ReplyService CreateReplyService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var ReplyService = new ReplyService(userId);
            return ReplyService;
        }

        public IHttpActionResult Get()
        {
            ReplyService ReplyService = CreateReplyService();
            var Replies = ReplyService.GetReplys();
            return Ok(Replies);
        }

        public IHttpActionResult GetById(int id)
        {
            ReplyService ReplyService = CreateReplyService();
            var Reply = ReplyService.GetReplyById(id);
            return Ok(Reply);
        }
        public IHttpActionResult Post(ReplyCreate Reply)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateReplyService();

            if (!service.CreateReply(Reply))
                return InternalServerError();

            return Ok();
        }
    }
}