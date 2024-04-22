
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using WPFPWZTGBOT;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

class Program
{
    public static DbConnect? _dbConnect;
    //private Object_tg_chat _tg_chat;
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
        Regex regex = new Regex(@"^9\d{9}$");
        string chat_id = update.Message?.Chat.Id.ToString()!;
        Object_tg_chat user_tg = _dbConnect?.Tg_bot.FirstOrDefault(e => e.Id_chat_tg == chat_id)!;
        switch(update.Message!.Text){
            case "/start":
                    if(user_tg == null) {
                        await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "Добро пожаловать в бота нашего ПВЗ, для регистрации введите номер телефона по примеру «9123456789»");
                    } else {
                        await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "Приветсвую, для проверки прибывших заказов напишите «Мои заказы»");
                    }
                    break;
            case "Мои заказы": 
                    var orders = _dbConnect.History_orders.Where(e => e.Num_phone == user_tg.Num_phone);
                    string list_orders = "у вас пока нету заказов";
                    if(orders != null){
                        list_orders = "--------------------------\n";
                        foreach(var i in orders){
                        var dateNow = DateTime.Now;
                        var srok_life = i.Data_end - dateNow; 
                        list_orders += $"заказ № {i.Id_order}\nсрок хранения(в днях): {srok_life.Days} \n -------------------------- \n";
                        }
                    }
                    if(list_orders == "--------------------------\n"){
                        await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "у вас пока нету заказов");
                    }
                    else {
                        await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, list_orders);
                    }
                    break;  
            default:
                if(regex.IsMatch(update.Message.Text! ?? "[none text]"))
                {
                    bool _bool = false;
                    if(_dbConnect?.Tg_bot.FirstOrDefault(e => e.Num_phone == update.Message.Text) != null) _bool = true;
                    if(user_tg == null && _bool == false) {
                        var add_client = new Object_tg_chat(update.Message!.Text!, update.Message?.Chat.Id.ToString()!);
                        _dbConnect?.Tg_bot.Add(add_client);
                        _dbConnect?.SaveChangesAsync();
                        InitializeDbContext();
                        await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "Номер зарегистрирован");
                    } else {
                        await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "номер уже зарегистрирован, введите другой или введите другую команду");
                    } 
                } else {
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "я вас не понял, я бот который работаю по заданным условиям");
                }
                    break;    
        }
        // if(update.Message!.Text == "/start")
        // {
        //     if (_dbConnect!.Tg_bot.Any(e => e.Id_chat_tg == chat_id))
        //     {
        //         await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "Приветсвую, для проверки прибывших заказов напишите «заказы»");
        //     }
        //     else
        //     {
        //         await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "Добро пожаловать в бота нашего ПВЗ, для регистрации введите номер телефона по примеру «9123456789»");
        //     }
        // } if(regex.IsMatch(update.Message?.Text!))
        // {
        //     Object_tg_chat user_tg = _dbConnect.Tg_bot.FirstOrDefault(e => e.Num_phone == chat_id)!;
        //     user_tg.Id_chat_tg = chat_id;
        //     _dbConnect?.SaveChangesAsync();
        // }
        // else if (_dbConnect!.Tg_bot.Any(e => e.Id_chat_tg == chat_id))
        // {
        //     //здесь проверка на есть ли заказы и проверка коррктности ввода
        //     await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "заказы:");
        // }
        // else
        // {
        //     await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "для проверки прибывших заказов нужна регистрация, Введите номер телефона по примеру «91234567890»");
        // }
        //await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, update.Message?.Text ?? "[none text]");
    }
}