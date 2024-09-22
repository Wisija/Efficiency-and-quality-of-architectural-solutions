using System;

public interface ISocialNetwork
{
    void Authenticate(string login, string password);
    void PublishPost(string message);
}

public class Facebook : ISocialNetwork
{
    private string username;
    private string password;

    public void Authenticate(string username, string password)
    {
        this.username = username;
        this.password = password;
        Console.WriteLine("Logged in Facebook with username: " + this.username);
    }

    public void PublishPost(string message)
    {
        Console.WriteLine("Sent message in Facebook: " + message);
    }
}

public class LinkedIn : ISocialNetwork
{
    private string email;
    private string password;

    public void Authenticate(string email, string password)
    {
        this.email = email;
        this.password = password;
        Console.WriteLine("Logged in LinkedIn with email: " + this.email);
    }

    public void PublishPost(string message)
    {
        Console.WriteLine("Sent message in LinkedIn: " + message);
    }
}

public abstract class SocialNetworkCreator
{
    public abstract ISocialNetwork CreateSocialNetwork();

    public void Publish(string login, string password, string message)
    {
        ISocialNetwork network = CreateSocialNetwork();
        network.Authenticate(login, password);
        network.PublishPost(message);
    }
}

public class FacebookFactory : SocialNetworkCreator
{
    public override ISocialNetwork CreateSocialNetwork()
    {
        return new Facebook();
    }
}

public class LinkedInFactory : SocialNetworkCreator
{
    public override ISocialNetwork CreateSocialNetwork()
    {
        return new LinkedIn();
    }
}

class Program
{
    static void Main(string[] args)
    {
        SocialNetworkCreator facebookFactory = new FacebookFactory();
        facebookFactory.Publish("user1", "password1", "Facebook");

        SocialNetworkCreator linkedInFactory = new LinkedInFactory();
        linkedInFactory.Publish("user_mail@gmail.com", "password2", "Linkedin");

    }
}
