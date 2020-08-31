using Microsoft.AspNetCore.Mvc;
using RayPI.Infrastructure.Cors.Attributes;
using RayPI.Infrastructure.Cors.Enums;
using System.Threading.Tasks;
using MediatR;
using System;
using RayPI.AppService.ArticleApp.Dtos;
using Ray.Infrastructure.Page;

namespace RayPI.OpenApi.Controllers
{
    /// <summary>
    /// 文章Api
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [RayCors(CorsPolicyEnum.Free)]
    public partial class ArticleController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mediator"></param>
        public ArticleController(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    public partial class ArticleController
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageResultDto<ArticleDetailDto>> Get([FromQuery] QueryArticlePageDto query)
        {
            return await _mediator.Send(query, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ArticleDetailDto> Get([FromRoute] Guid id)
        {
            return await _mediator.Send(new QueryArticleDto { Id = id });
        }
    }

    /// <summary>
    /// 新增
    /// </summary>
    public partial class ArticleController
    {
        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ArticleDetailDto> Create([FromBody] CreateArticleDto cmd)
        {
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }
    }

    /// <summary>
    /// 编辑
    /// </summary>
    public partial class ArticleController
    {
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="requset"></param>
        [HttpPut]
        [Route("{id}")]
        public async Task<ArticleDetailDto> Update([FromRoute] Guid id, [FromBody] UpdateArticleDto requset)
        {
            requset.Id = id;
            return await _mediator.Send(requset);
        }
    }

    /// <summary>
    /// 删除
    /// </summary>
    public partial class ArticleController
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("{id}")]
        public async Task<bool> Delete([FromRoute] Guid id)
        {
            return await _mediator.Send(new DeleteArticleDto { Id = id });
        }
    }
}