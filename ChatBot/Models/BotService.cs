using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using WebParser.Models;
using WebParser.Service;
using Telegram.Bot.Types;

namespace ChatBot.Models
{
    public class BotService
    {
        private ProductsContext _productsContext;
        private WriteToFile _fileWriter;

        public BotService(ProductsContext productsContext, WriteToFile toFile)
        {
            _productsContext = productsContext;
            _fileWriter = toFile;
        }

        private async Task SendFile(ITelegramBotClient bot, Update up, CancellationToken token)
        {
            var file = Guid.NewGuid().ToString("D") + ".xlsx";
            _fileWriter.SaveAs(file, await _productsContext.Products.ToListAsync());
            await using (Stream stream = System.IO.File.OpenRead(file))
            {
                await bot.SendDocumentAsync(
                chatId: up.Message.Chat.Id,
                document: InputFile.FromStream(stream, file),
                caption: "Product list");
            }
            await Task.Run(() => System.IO.File.Delete(file), token);
        }

        private async Task FindProduct(ITelegramBotClient bot, Update up, CancellationToken token)
        {
            var code = up.Message.Text.Trim();
            var product = _productsContext.Products.FirstOrDefault(y => y.Code == code);

            if (product != null)
            {
                await bot.SendPhotoAsync(
                    chatId: up.Message.Chat.Id,
                    photo: InputFile.FromUri(product.ImageUrl),
                    caption: $"<b>{product.Title}</b>\n" +
                    $"<b>{product.Description}</b>\n" +
                    $"${product.Price}\n",
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                    replyMarkup: new InlineKeyboardMarkup(
                        InlineKeyboardButton.WithUrl(text: "Open website", url: product.ProductUrl)));
            }
            else
            {
                ReplyKeyboardMarkup reply = new(new[] { new KeyboardButton[] { "Download file" }, }) { ResizeKeyboard = true };
                await bot.SendTextMessageAsync(chatId: up.Message.Chat.Id, text: "Not Found", replyMarkup: reply);
            }
        }

        public async Task Start(ITelegramBotClient bot, Update up, CancellationToken token)
        {
            ReplyKeyboardMarkup reply = new(new[] { new KeyboardButton[] { "Download file" } }) { ResizeKeyboard = true };
            await bot.SendTextMessageAsync(chatId: up.Message.Chat.Id, text: "Welcome to this chat!", replyMarkup: reply);
        }
        public async Task HandleUpdate(ITelegramBotClient bot, Update up, CancellationToken token)
        {
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(up));
            if (up.Message != null)
            {
                await bot.SendChatActionAsync(chatId: up.Message.Chat.Id, chatAction: Telegram.Bot.Types.Enums.ChatAction.Typing);

                var text = up.Message.Text;
                if (text == "Download file") { await SendFile(bot, up, token); }
                else if (text == "/start") { await Start(bot, up, token); }
                else { await FindProduct(bot, up, token); }
                return;
            }
        }
    }
}
