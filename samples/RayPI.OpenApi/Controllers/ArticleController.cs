using Microsoft.AspNetCore.Mvc;
using RayPI.Infrastructure.Cors.Attributes;
using RayPI.Infrastructure.Cors.Enums;
using System.Threading.Tasks;
using MediatR;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using System;
using RayPI.AppService.Article.Dtos;

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
        public async Task<List<ResponseQueryArticleDto>> Get([FromQuery]QueryArticlePageDto query)
        {
            return await _mediator.Send(query, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("Id")]
        public async Task<ResponseQueryArticleDto> Get(Guid id)
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
        public async Task<Guid> Create(CreateArticleDto cmd)
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
        public void Update(UpdateArticleDto requset)
        {
            _mediator.Send(requset);
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
        public void Delete(Guid id)
        {
            _mediator.Send(new DeleteArticleDto { Id = id });
        }
    }
}