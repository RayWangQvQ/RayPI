using Microsoft.AspNetCore.Mvc;
using RayPI.Infrastructure.Cors.Attributes;
using RayPI.Infrastructure.Cors.Enums;
using System.Threading.Tasks;
using MediatR;
using RayPI.AppService.Commands;

namespace RayPI.OpenApi.Controllers
{
    /// <summary>
    /// 文章Api
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [RayCors(CorsPolicyEnum.Free)]
    public class ArticleController : Controller
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> Create(CreateArticleCmd cmd)
        {
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }

    }
}