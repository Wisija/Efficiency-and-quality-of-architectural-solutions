using System;
using System.Collections.Generic;

public interface IStorage
{
    void Connect();
    void Disconnect();
    void UploadFile(string fileName);
    void DownloadFile(string fileName);
}

public class LocalDiskStorage : IStorage
{
    public void Connect()
    {
        Console.WriteLine("Connected to local disk storage");
    }

    public void Disconnect()
    {
        Console.WriteLine("Disconnected from local disk storage");
    }

    public void UploadFile(string fileName)
    {
        Console.WriteLine($"Uploaded {fileName} to local disk");
    }

    public void DownloadFile(string fileName)
    {
        Console.WriteLine($"Downloaded {fileName} from local disk");
    }
}

public class AmazonS3Storage : IStorage
{
    public void Connect()
    {
        Console.WriteLine("Connected to Amazon S3 storage");
    }

    public void Disconnect()
    {
        Console.WriteLine("Disconnected from Amazon S3 storage");
    }

    public void UploadFile(string fileName)
    {
        Console.WriteLine($"Uploaded {fileName} to Amazon S3");
    }

    public void DownloadFile(string fileName)
    {
        Console.WriteLine($"Downloaded {fileName} from Amazon S3");
    }
}

public class FileManager
{
    private static FileManager instance;
    private const int MAX_USERS = 2;
    private string[] users = new string[MAX_USERS];
    private IStorage[] storages = new IStorage[MAX_USERS];
    private int userCount = 0;

    private FileManager() { }

    public static FileManager GetInstance()
    {
        if (instance == null)
        {
            instance = new FileManager();
        }
        return instance;
    }

    public void SetUserStorage(string user, string storageType)
    {
        if (userCount >= MAX_USERS)
        {
            Console.WriteLine("Maximum user limit reached!");
            return;
        }
        for (int i = 0; i < userCount; ++i)
        {
            if (users[i] == user)
            {
                storages[i] = storageType == "local" ? new LocalDiskStorage() : new AmazonS3Storage();
                return;
            }
        }
        users[userCount] = user;
        storages[userCount] = storageType == "local" ? new LocalDiskStorage() : new AmazonS3Storage();
        ++userCount;
    }

    public void ConnectUserStorage(string user)
    {
        for (int i = 0; i < userCount; ++i)
        {
            if (users[i] == user)
            {
                storages[i].Connect();
                return;
            }
        }
        Console.WriteLine("User not found!");
    }

    public void DisconnectUserStorage(string user)
    {
        for (int i = 0; i < userCount; ++i)
        {
            if (users[i] == user)
            {
                storages[i].Disconnect();
                return;
            }
        }
        Console.WriteLine("User not found!");
    }

    public void UploadFile(string user, string fileName)
    {
        for (int i = 0; i < userCount; ++i)
        {
            if (users[i] == user)
            {
                storages[i].UploadFile(fileName);
                return;
            }
        }
        Console.WriteLine("User not found!");
    }

    public void DownloadFile(string user, string fileName)
    {
        for (int i = 0; i < userCount; ++i)
        {
            if (users[i] == user)
            {
                storages[i].DownloadFile(fileName);
                return;
            }
        }
        Console.WriteLine("User not found!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        FileManager manager = FileManager.GetInstance();

        manager.SetUserStorage("user1", "local");
        manager.ConnectUserStorage("user1");
        manager.UploadFile("user1", "file1.txt");
        manager.DisconnectUserStorage("user1");

        manager.SetUserStorage("user2", "s3");
        manager.ConnectUserStorage("user2");
        manager.DownloadFile("user2", "file2.txt");
        manager.DisconnectUserStorage("user2");
    }
}