using Blog_REST_API.Models;
using PWEntity;
using PWService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blog_REST_API.Controllers
{
    [RoutePrefix("api/Blog")]
    public class BlogController : ApiController
    {
        ServiceFactory services;
        public BlogController()
        {
            services = Injector.container.Resolve<ServiceFactory>();
        }

        [Route("Articles")]
        public IHttpActionResult GetListOfArticle()
        {
            List<Article> articleList = services.articleService.GetAll();
            List<ArticleGist> gistList = new List<ArticleGist>();
            foreach(var item in articleList)
            {
                ArticleGist gist = new ArticleGist();
                gist.id = item.id;
                gist.subject = item.subject;
                gist.date = item.date;
                gist.url = "http://localhost:36773/api/Blog/Article/" + item.id;
                gistList.Add(gist);
            }
            return Ok(gistList);
        }

        [Route("Article/{id}",Name="Article")]
        public IHttpActionResult GetArticle([FromUri]int id)
        {
            Article article = services.articleService.Get(id);
            if (article != null)
            {
                article.comments = services.articleCommentService.GetAll(id);
                return Ok(article);
            }
            else
                return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("Comment")]
        public IHttpActionResult PostComment([FromBody]ArticleComment comment)
        {
            if (ModelState.IsValid)
            {
                comment.date = DateTime.Now;
                services.articleCommentService.Insert(comment);
                return Created(Url.Link("Article", new { id = comment.articleId }), comment);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
        }

    }
}
