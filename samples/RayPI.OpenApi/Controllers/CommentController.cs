using Microsoft.AspNetCore.Mvc;
using RayPI.Infrastructure.Cors.Attributes;
using RayPI.Infrastructure.Cors.Enums;
using Ray.Infrastructure.Page;
using RayPI.AppService.CommentApp;
using RayPI.AppService.CommentApp.Dtos;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

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
        public async Task<PageResultDto<CommentDto>> GetPage([FromQuery]QueryCommentPageDto query)
        {
            return await _commentAppService.GetPageAsync(query);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<CommentDto> Get([FromRoute]Guid id)
        {
            return await _commentAppService.GetAsync(id);
        }
    }

    /// <summary>
    /// 新增
    /// </summary>
    public partial class CommentController
    {
        [HttpPost]
        public async Task<Guid> Add([FromBody]CommentDto request)
        {
            var re= await _commentAppService.CreateAsync(request);
            return Guid.NewGuid();
        }
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