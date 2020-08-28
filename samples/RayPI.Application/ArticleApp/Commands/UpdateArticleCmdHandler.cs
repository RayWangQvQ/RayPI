using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepositories;

namespace RayPI.AppService.ArticleApp.Commands
{
    public class UpdateArticleCmdHandler : IRequestHandler<UpdateArticleDto, Guid>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public UpdateArticleCmdHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            this._articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateArticleDto request, CancellationToken cancellationToken)
        {
            var entity = await _articleRepository.GetAsync(x => x.Id == request.Id);
            if (entity == null) throw new Exception("不存在");

            entity.Title = request.Title;
            entity.SubTitle = request.SubTitle;
            entity.Content = request.Content;

            //_mapper.Map<UpdateArticleDto, Article>(request, entity);

            var result = await _articleRepository.UpdateAsync(entity);

            return result.Id;
        }
    }
}
