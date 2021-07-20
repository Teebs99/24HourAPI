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
    public class PostController : ApiController
    {
        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PostService(userId);
            return service;

        }
        
        [HttpGet]
        public IHttpActionResult Get()
        {
            var service = CreatePostService();
            var posts = service.GetPosts();
            if (posts == null)
                return NotFound();
            return Ok(posts);

        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var service = CreatePostService();
            var PostDetail = service.GetPost(id);
            if (PostDetail == null)
                return NotFound();
            return Ok(PostDetail);
        }
        [HttpPost]
        public IHttpActionResult Post(CreatePost model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreatePostService();
            if (!service.CreatePost(model))
                return InternalServerError();
            return Ok();

        }
        [HttpPut]
        public IHttpActionResult Put(PostEdit model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreatePostService();
            if (!service.UpdatePost(model))
                return InternalServerError();
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreatePostService();
            if (service.DeletePost(id))
                return Ok();
            return InternalServerError();
        }
    }
}
