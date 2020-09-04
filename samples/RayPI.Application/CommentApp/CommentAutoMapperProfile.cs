using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RayPI.Application.CommentApp.Dtos;
using RayPI.Domain.Aggregates.CommentAggregate;

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
