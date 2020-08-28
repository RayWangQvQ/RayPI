using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ray.Infrastructure.Helpers;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepositories;

namespace RayPI.AppService.ArticleApp.Queries
{
    public class ArticlePageQueryHandler : IRequestHandler<QueryArticlePageDto, List<ResponseQueryArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 构造
        /// </summary>
        public ArticlePageQueryHandler(IArticleRepository articleRepository
            , IMapper mapper)
        {
            this._articleRepository = articleRepository;
            _mapper = mapper;
        }

        public Task<List<ResponseQueryArticleDto>> Handle(QueryArticlePageDto request, CancellationToken cancellationToken)
        {
            IQueryable<Article> queryable = _articleRepository.GetQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
                queryable = queryable.Where(x => x.Title.Contains(request.Title));

            List<Article> entityList = queryable.ToList();

            var list = _mapper.Map<List<Article>, List<ResponseQueryArticleDto>>(entityList);
            return Task.FromResult(list);
        }
    }
}
