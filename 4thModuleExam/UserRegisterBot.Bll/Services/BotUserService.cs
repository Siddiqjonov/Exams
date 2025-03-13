using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegisterBot.Dal;
using UserRegisterBot.Dal.Entities;

namespace UserRegisterBot.Bll.Services;

public class BotUserService : IBotUserService
{
    private MainContext MainContext;

    public BotUserService(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public async Task AddUserAsync(BotUser botUser)
    {
        await MainContext.AddAsync(botUser);
        await MainContext.SaveChangesAsync();
    }

    public async Task DeleteDataAsync(long id)
    {
        var botUser = MainContext.Users.FirstOrDefault(u => u.TelegramUserId == id);
        MainContext.Users.Remove(botUser);
        await MainContext.SaveChangesAsync();
    }

    public Task<BotUser> GetDataAsync(long id)
    {
        var user = MainContext.Users.FirstOrDefault(u => u.TelegramUserId == id);
        return user;
    }
}
