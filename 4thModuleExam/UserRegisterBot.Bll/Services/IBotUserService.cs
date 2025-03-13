using UserRegisterBot.Dal.Entities;

namespace UserRegisterBot.Bll.Services;

public interface IBotUserService
{
    Task AddUserAsync(BotUser botUser);
    Task<BotUser> GetDataAsync(long id);
    Task DeleteDataAsync(long id);
}