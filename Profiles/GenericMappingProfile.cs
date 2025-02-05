using AutoMapper;
using MyApi.Dto;
using MyApi.Models;

public class GenericMappingProfile : Profile
{
    public GenericMappingProfile()
    {
        // Define mappings here
        CreateMap<TodoItem, TodoItemDto>(); // Map TodoItem to TodoItemDto
        CreateMap<TodoItemDto, TodoItem>(); // Map TodoItemDto to TodoItem
    }
    public void CreateGenericMap<TSource, TDestination>()
    {
        CreateMap<TSource, TDestination>();
    }
}