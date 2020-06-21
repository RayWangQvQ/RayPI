using Microsoft.AspNetCore.Mvc;
using RayPI.Infrastructure.Cors.Attributes;
using RayPI.Infrastructure.Cors.Enums;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Ray.Infrastructure.Page;
using RayPI.AppService.Comment.Dtos;

namespace RayPI.OpenApi.Controllers
{
    /// <summary>
    /// 评论Api
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [RayCors(CorsPolicyEnum.Free)]
    public partial class CommentController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mediator"></param>
        public CommentController(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    public partial class CommentController
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageResultDto<CommentDto>> GetPage([FromQuery]QueryCommentPageDto query)
        {
            return await _mediator.Send(query, HttpContext.RequestAborted);
        }
    }

    /// <summary>
    /// 新增
    /// </summary>
    public partial class CommentController
    {

    }

    /// <summary>
    /// 编辑
    /// </summary>
    public partial class CommentController
    {

    }

    /// <summary>
    /// 删除
    /// </summary>
    public partial class CommentController
    {

    }
}