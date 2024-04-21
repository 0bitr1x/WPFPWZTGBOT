
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using WPFPWZTGBOT;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

class Program
{
    public static DbConnect? _dbConnect;
    private Object_tg_chat _tg_chat;
    private static void Main()
    {
        Host ghost = new Host("7188326377:AAHAqo_GK68oSs_MwZk9thkDCvVXEN_iJwA");
        ghost.Start();

        ghost.OnMessage += OnMessage;
        InitializeDbContext();
        Console.ReadLine();
    }
    private static void InitializeDbContext()
    {
        _dbConnect = new DbConnect();
    }
    private void LoadDataFromDatabase()
    {
        //sklad = dbConnect.Sklad.Where(u => u.Id_order == 0).ToList();
    }
    private static async void OnMessage(ITelegramBotClient client, Update update)
    {
        if(update.Message.Text == "/start")
        {
            if (_dbConnect.Tg_bot.Any(e => e.Id_chat_tg == update.Message.Chat.Id.ToString()))
            {
                await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "Приветсвую, для проверки прибывших заказов напишите «заказы»");
            }
            else
            {
                await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "Добро пожаловать в бота нашего ПВЗ, для регистрации введите номер телефона по примеру «91234567890»");
            }
        }
        else if (_dbConnect.Tg_bot.Any(e => e.Id_chat_tg == update.Message.Chat.Id.ToString()))
        {
            //здесь проверка на есть ли заказы и проверка коррктности ввода
            await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "заказы:");
        }
        else
        {
            await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "для проверки прибывших заказов нужна регистрация, Введите номер телефона по примеру «91234567890»");
        }
        //await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, update.Message?.Text ?? "[none text]");
    }
}