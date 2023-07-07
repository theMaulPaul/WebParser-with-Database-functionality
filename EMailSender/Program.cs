using EMailSender.Models;

Console.WriteLine("Creating new letter...");

string server = "smtp.gmail.com";
int port = 587;
string username = "strangemanwithwhisky@gmail.com";
string pass = "nbauxhvoshykygrh";

MailSender sender = new MailSender(server, port, username, pass);

string from = username;
string to = "themaulpaul@ukr.net";
string subject = "Test letter";
string body = "This email was sent to you as test";
string attc = "C:\\Users\\stran\\Desktop\\Homework\\HTML_урок_10_ua.pdf";

sender.Send(from, to, subject, body, attc);