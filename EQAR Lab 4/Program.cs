using System;

public interface Notification
{
    void Send(string title, string message);
}

public class EmailNotification : Notification
{
    private string adminEmail;

    public EmailNotification(string adminEmail)
    {
        this.adminEmail = adminEmail;
    }

    public void Send(string title, string message)
    {
        Console.WriteLine($"From '{adminEmail}', NEW Email --- Title '{title}' \nText ->    '{message}'\n");
    }
}

public class Slack
{
    private string login;
    private string apiKey;
    private string chatId;

    public Slack(string login, string apiKey, string chatId)
    {
        this.login = login;
        this.apiKey = apiKey;
        this.chatId = chatId;
    }

    public void SendMessage(string title, string message)
    {
        Console.WriteLine($"ChatId '{chatId}', NEW MESSAGE --- Title '{title}' \n Text ->  '{message}'\n");
    }
}

public class Sms
{
    private string phone;
    private string sender;

    public Sms(string phone, string sender)
    {
        this.phone = phone;
        this.sender = sender;
    }

    public void SendSms(string title, string message)
    {
        Console.WriteLine($"From '{sender}', NEW SMS --- to phone number '{phone}' \n Text ->  '{message}'\n");
    }
}


public class SlackNotificationAdapter : Notification
{
    private Slack slackService;

    public SlackNotificationAdapter(string login, string apiKey, string chatId)
    {
        this.slackService = new Slack(login, apiKey, chatId);
    }

    public void Send(string title, string message)
    {
        slackService.SendMessage(title, message);
    }
}


public class SmsNotificationAdapter : Notification
{
    private Sms smsService;

    public SmsNotificationAdapter(string phone, string sender)
    {
        this.smsService = new Sms(phone, sender);
    }

    public void Send(string title, string message)
    {
        smsService.SendSms(title, message);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Notification emailNotification = new EmailNotification("adminKHPI@gmail.com");
        emailNotification.Send("Bug Report", "New Bug Founded");

        Notification slackNotification = new SlackNotificationAdapter("Student1", "dfsdf7jhfid83dfSFGF ", "6304");
        slackNotification.Send("Exams schedule", "Winter exams schedule <link>");

        Notification smsNotification = new SmsNotificationAdapter("+380 XX-XXX-XX-XX", "Directorate");
        smsNotification.Send("New university admission rules ", "university admission rules updated on our official cite, check them now <link>");
    }
}

