using Microsoft.AspNetCore.Mvc;
using RayPI.Infrastructure.Cors.Attributes;
using RayPI.Infrastructure.Cors.Enums;
using Ray.Infrastructure.Page;
using RayPI.AppService.CommentApp;
using RayPI.AppService.CommentApp.Dtos;
using System.Threading.Tasks;

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
        private readonly ICommentAppService _commentAppService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="commentAppService"></param>
        public CommentController(ICommentAppService commentAppService)
        {
            _commentAppService = commentAppService;
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
        public async Task<PageResultDto<CommentDto>> GetPage([FromQuery] QueryCommentPageDto query)
        {
            return await _commentAppService.GetPageAsync(query);
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