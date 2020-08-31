using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.Entity;

namespace RayPI.Application.ArticleApp
{
    /// <summary>
    /// AutoMapper映射配置
    /// </summary>
    public class ArticleAutoMapperProfile : Profile
    {
        public ArticleAutoMapperProfile()
        {
            CreateMap<Article, ArticleDetailDto>();
            CreateMap<CreateArticleDto, Article>();
            CreateMap<UpdateArticleDto, Article>();
        }
    }
}
