using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UserRegisterBot.Bll.Services;
using UserRegisterBot.Dal.Entities;

namespace UserRegisterBot;

public class BotListenerService
{
    private readonly string botToken;
    private readonly TelegramBotClient botClient;
    private readonly IBotUserService botUserService;

    public BotListenerService(IBotUserService botUserService)
    {
        botToken = "7590777961:AAEiAFJW58PwupeLGyt08ApmeWs5gebaWGo";
        botClient = new TelegramBotClient(botToken);
        this.botUserService = botUserService;
    }
    public async Task StartBot()
    {
        botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync);
        Console.WriteLine("Bot is runing");
        Console.ReadKey();
    }
    private async Task HandleErrorAsync(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
    }

    private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken token)
    {
        if (update.Type == UpdateType.Message)
        {
            var user = update.Message.Chat;
            var message = update.Message;

            var fillData = "Fill data";
            var getData = "Get data";
            var deleteData = "Delete data";

            if (message.Text == "/start")
            {

                var menu = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton(fillData),
                        new KeyboardButton(getData)
                    },
                    [
                        new KeyboardButton(deleteData)
                    ]
                })
                {
                    ResizeKeyboard = true,
                    OneTimeKeyboard = true
                };

                await bot.SendMessage(user.Id, "Welcome to the bot", replyMarkup: menu);
                return;
            }
            var infoOrder = $"Fill data\nFistName:\nLastName:\nEmail:\nPhoneNumber:\nAddress:\nDateOfBirth: (yyyy-mm-dd)";
            var infoExample = "For Example\n\nFill date\nSamandar\nUsmonov\nsamandarUsmonov@gmail.com\n+998941234567\nA.Temur 12\n2000-04-24";

            if (message.Text == fillData)
            {
                await bot.SendMessage(user.Id, "Send you date in the followitg order:");
                await bot.SendMessage(user.Id, infoOrder);
                await bot.SendMessage(user.Id, infoExample);
            }

            else if (message.Text == getData)
            {
                var userInfo = botUserService.GetDataAsync(user.Id);
                var jsonInfo = JsonSerializer.Serialize(userInfo);
                await bot.SendMessage(user.Id, jsonInfo);

            }
            else if (message.Text == deleteData)
            {
                await botUserService.DeleteDataAsync(user.Id);
                await bot.SendMessage(user.Id, "Your data has been deleted");
            }

            if (message.Text.StartsWith(fillData))
            {
                var lines = message.Text.Split('\n');
                if (lines.Length >= 7)
                {
                    var firstName = lines[1].Trim();
                    var lastName = lines[2].Trim();
                    var email = lines[3].Trim();
                    var phoneNumber = lines[4].Trim();
                    var address = lines[5].Trim();
                    var dateOfBirth = lines[6].Trim();

                    BotUser botUser = new BotUser()
                    {
                        TelegramUserId = user.Id,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        PhoneNumber = phoneNumber,
                        Address = address,
                        DateOfBirth = Convert.ToDateTime(dateOfBirth)
                    };
                    await botUserService.AddUserAsync(botUser);
                    await bot.SendMessage(user.Id, "Your data has been saved successfully.");
                }
                else
                {
                    await bot.SendMessage(user.Id, "Invalid data format. Please follow the correct format.");
                }
            }

        }
    }
}
