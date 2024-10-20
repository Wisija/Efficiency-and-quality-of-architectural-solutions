using System;
using System.Collections.Generic;

public interface Downloader
{
    string Download(string url);
}

public class SimpleDownloader : Downloader
{
    public string Download(string url)
    {
        Console.WriteLine("Downloading -> " + url);
        return "Downloaded content from -> " + url;
    }
}

public class CachedDownloader : Downloader
{
    private SimpleDownloader _simpleDownloader;
    private Dictionary<string, string> _cache;

    public CachedDownloader(SimpleDownloader simpleDownloader)
    {
        _simpleDownloader = simpleDownloader;
        _cache = new Dictionary<string, string>();
    }

    public string Download(string url)
    {
        if (_cache.ContainsKey(url))
        {
            Console.WriteLine("Fetching from cache -> " + url);
            return _cache[url];
        }
        else
        {
            string data = _simpleDownloader.Download(url);
            _cache[url] = data;
            return data;
        }
    }
}

public class MainClass
{
    public static void Main(string[] args)
    {
        Downloader downloader = new CachedDownloader(new SimpleDownloader());

        string data1 = downloader.Download("https://EQAR.com/Lab1");
        Console.WriteLine(data1 + "\n");

        string data2 = downloader.Download("https://EQAR.com/Lab2");
        Console.WriteLine(data2 + "\n");

        string data3 = downloader.Download("https://EQAR.com/Lab1"); 
        Console.WriteLine(data3 + "\n");

        string data4 = downloader.Download("https://EQAR.com/Lab2");
        Console.WriteLine(data4 + "\n");

        string data5 = downloader.Download("https://EQAR.com/Lab3");
        Console.WriteLine(data5 + "\n");
    }
}
