using FluentValidation;
using TaskManager.Bll.Dtos;
using TaskManager.Bll.FluentValidations;
using TaskManager.Dal.Entities;
using TaskManager.Repository.Services;

namespace TaskManager.Bll.Services;

public class ToDoItemService : IToDoItemService
{
    private readonly IToDoItemRepository ToDoItemRepository;

    public ToDoItemService(IToDoItemRepository toDoItemRepository)
    {
        ToDoItemRepository = toDoItemRepository;
    }

    public async Task<long> AddToDoItemAsync(ToDoItemCreateDto toDoItemCreateDto)
    {
        var validationRules = new ToDoItemCreateDtoValidator();
        var validationResult = validationRules.Validate(toDoItemCreateDto);

        //if (!validationResult.IsValid)
        //{
        //    throw new Exception(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        //}

        if (!validationResult.IsValid)
        {
            var messages = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                messages.Add(error.ErrorMessage);
            }

            throw new Exception(string.Join("; ", messages));
        }

        var toDoItem = ConvertToToDoItemWhileCreating(toDoItemCreateDto);
        var itemId = await ToDoItemRepository.AddToDoItemAsync(toDoItem);
        return itemId;
    }
    private ToDoItem ConvertToToDoItemWhileCreating(ToDoItemCreateDto doItemCreateDto)
    {
        return new ToDoItem()
        {
            Title = doItemCreateDto.Title,
            Description = doItemCreateDto.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            DueDate = doItemCreateDto.DueDate,
        };
    }

    public async Task DeleteToDoItemByIdAsync(long id)
    {
        await ToDoItemRepository.DeleteToDoItemByIdAsync(id);
    }

    public async Task<ICollection<ToDoItemExtendedDto>> GetAllToDoItemsAsync(int skip, int take)
    {
        var toDoItems = await ToDoItemRepository.SelectAllToDoItemsAsync(skip, take);

        var toDoItemExtendedDtos = toDoItems.Select(i => ConvertToToDoItemExtendedDto(i)).ToList();

        var validationRules = new ToDoItemExtendedDtoValidator();
        foreach (var itemDto in toDoItemExtendedDtos)
        {
            var validationResult = validationRules.Validate(itemDto);
            if (!validationResult.IsValid)
            {
                throw new Exception(string.Join("; ", validationResult.Errors.Select(m => m.ErrorMessage)));
            }
        }

        return toDoItemExtendedDtos;
    }

    private ToDoItemExtendedDto ConvertToToDoItemExtendedDto(ToDoItem toDoItem)
    {
        return new ToDoItemExtendedDto()
        {
            Id = toDoItem.Id,
            Title = toDoItem.Title,
            Description = toDoItem.Description,
            IsCompleted = toDoItem.IsCompleted,
            CreatedAt = toDoItem.CreatedAt,
            DueDate = toDoItem.DueDate,
        };
    }

    public async Task<ICollection<ToDoItemExtendedDto>> GetCompletedToDoItemsAsync(int skip, int take)
    {
        var toToItems = await ToDoItemRepository.SelectCompletedToDoItemsAsync(skip, take);

        return toToItems.Select(i => ConvertToToDoItemExtendedDto(i)).ToList();
    }

    public async Task<ICollection<ToDoItemExtendedDto>> GetIncompletedToDoItemsAsync(int skip, int take)
    {
        var items = await ToDoItemRepository.SelectIncompleteToDoItemsAsync(skip, take);

        return items.Select(i => ConvertToToDoItemExtendedDto(i)).ToList();
    }

    public async Task<ToDoItemExtendedDto> GetToDoItemByIdAsync(long id)
    {
        ToDoItem item = await ToDoItemRepository.SelectToDoItemByIdAsync(id);
        var toDoItemExtendedDto = ConvertToToDoItemExtendedDto(item);
        return toDoItemExtendedDto;
    }

    public async Task<ICollection<ToDoItemExtendedDto>> GetToDoItemsByDueDateAsync(DateTime dueDate)
    {
        var toDoItems = await ToDoItemRepository.SelectByDueDateToDoItemsAsync(dueDate);
        return toDoItems.Select(i => ConvertToToDoItemExtendedDto(i)).ToList();
    }

    public async Task UpdateToDoItemAsync(ToDoItemExtendedDto toDoItemUpdateDto)
    {
        var toDoItem = ConvertToToDoItem(toDoItemUpdateDto);
        await ToDoItemRepository.UpdateToDoItemAsync(toDoItem);
    }

    private ToDoItem ConvertToToDoItem(ToDoItemExtendedDto toDoItemUpdateDto)
    {
        return new ToDoItem()
        {
            Id = toDoItemUpdateDto.Id,
            Title = toDoItemUpdateDto.Title,
            Description = toDoItemUpdateDto.Description,
            IsCompleted = toDoItemUpdateDto.IsCompleted,
            CreatedAt = toDoItemUpdateDto.CreatedAt,
            DueDate = toDoItemUpdateDto.DueDate,
        };
    }
}
