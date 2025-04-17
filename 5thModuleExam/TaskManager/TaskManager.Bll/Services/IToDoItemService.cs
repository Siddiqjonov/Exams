using TaskManager.Bll.Dtos;

namespace TaskManager.Bll.Services;

public interface IToDoItemService
{
	Task<long> AddToDoItemAsync(ToDoItemCreateDto doItemCreateDto);
    Task DeleteToDoItemByIdAsync(long id);
    Task UpdateToDoItemAsync(ToDoItemExtendedDto toDoItemUpdateDto);
    Task<ICollection<ToDoItemExtendedDto>> GetAllToDoItemsAsync(int skip, int take);
    Task<ToDoItemExtendedDto> GetToDoItemByIdAsync(long id);
    Task<ICollection<ToDoItemExtendedDto>> GetToDoItemsByDueDateAsync(DateTime dueDate);
    Task<ICollection<ToDoItemExtendedDto>> GetCompletedToDoItemsAsync(int skip, int take);
    Task<ICollection<ToDoItemExtendedDto>> GetIncompletedToDoItemsAsync(int skip, int take);
}