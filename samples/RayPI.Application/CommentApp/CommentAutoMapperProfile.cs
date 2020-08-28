using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RayPI.AppService.CommentApp.Dtos;
using RayPI.Domain.Entity;

namespace RayPI.Application.CommentApp
{
    public class CommentAutoMapperProfile : Profile
    {
        public CommentAutoMapperProfile()
        {
            CreateMap<CommentDto, Comment>();
            CreateMap<Comment, CommentDto>();
        }
    }
}
