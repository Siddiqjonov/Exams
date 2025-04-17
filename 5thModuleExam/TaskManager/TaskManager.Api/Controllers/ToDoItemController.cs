using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TaskManager.Bll.Dtos;
using TaskManager.Bll.Services;
using TaskManager.Dal.Entities;

namespace TaskManager.Api.Controllers;

[Route("api/toDoItem")]
[ApiController]
public class ToDoItemController : ControllerBase
{
    private readonly IToDoItemService ToDoItemService;

    public ToDoItemController(IToDoItemService toDoItemService)
    {
        this.ToDoItemService = toDoItemService;
    }

    [HttpPost("add")]
    public async Task AddToDoItem(ToDoItemCreateDto toDoItemCreateDto)
    {
        await ToDoItemService.AddToDoItemAsync(toDoItemCreateDto);
    }

    [HttpGet("getAll")]
    public async Task<ICollection<ToDoItemExtendedDto>> GetAll(int skip, int take)
    {
        return await ToDoItemService.GetAllToDoItemsAsync(skip, take);
    }

    [HttpDelete("delete")]
    public async Task Delete(long id)
    {
        await ToDoItemService.DeleteToDoItemByIdAsync(id);
    }

    [HttpPut("update")]
    public async Task Update(ToDoItemExtendedDto toDoItem)
    {
        await ToDoItemService.UpdateToDoItemAsync(toDoItem);
    }

    [HttpGet("getById")]
    public async Task<ToDoItemExtendedDto> GetById(long id)
    {
        return await ToDoItemService.GetToDoItemByIdAsync(id);
    }

    [HttpGet("getByDueDate")]
    public async Task<ICollection<ToDoItemExtendedDto>> GetByDueDate(DateTime dueDate)
    {
        return await ToDoItemService.GetToDoItemsByDueDateAsync(dueDate);
    }

    [HttpGet("getComplited")]
    public async Task<ICollection<ToDoItemExtendedDto>> GetComplited(int skip, int take)
    {
        return await ToDoItemService.GetCompletedToDoItemsAsync(skip, take);
    }

    [HttpGet("getIncompleted")]
    public async Task<ICollection<ToDoItemExtendedDto>> GetIncompleted(int skip, int take)
    {
        return await ToDoItemService.GetIncompletedToDoItemsAsync(skip, take);
    }
}
