using System;

public class Program
{
    public static void Main(string[] args)
    {
        Renderer htmlRenderer = new HTMLRenderer();
        Renderer jsonRenderer = new JSONRenderer();
        Renderer xmlRenderer = new XMLRenderer();

        Page simplePage = new SimplePage(htmlRenderer, "HTML Page", "Welcome to  HTML");
        simplePage.View();
        Product product = new Product("Microphone", "Fifine top 1 mic", "Mic.jpg", 0001);
        Page productPage = new ProductPage(htmlRenderer, product);
        productPage.View();


        simplePage = new SimplePage(jsonRenderer, "JSON Page", "Welcome to  JSON");
        simplePage.View();
        Product product2 = new Product("Soundboard", "Soundboard with different outputs", "board.jpg", 0002);
        productPage = new ProductPage(jsonRenderer, product2);
        productPage.View();


        simplePage = new SimplePage(xmlRenderer, "XML Page", "Welcome to XML");
        simplePage.View();
        productPage = new ProductPage(xmlRenderer, product2);
        productPage.View();
    }
}


public abstract class Page
{
    protected Renderer renderer;

    public Page(Renderer renderer)
    {
        this.renderer = renderer;
    }

    public abstract void View();
}


public interface Renderer
{
    void RenderSimplePage(string title, string content);
    void RenderProductPage(Product product);
}



public class SimplePage : Page
{
    private string title;
    private string content;

    public SimplePage(Renderer renderer, string title, string content) : base(renderer)
    {
        this.title = title;
        this.content = content;
    }

    public override void View()
    {
        renderer.RenderSimplePage(title, content);
    }
}


public class ProductPage : Page
{
    private Product product;

    public ProductPage(Renderer renderer, Product product) : base(renderer)
    {
        this.product = product;
    }

    public override void View()
    {
        renderer.RenderProductPage(product);
    }
}


public class Product
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImgUrl { get; set; }
    public int Id { get; set; }

    public Product(string name, string description, string imgUrl, int id)
    {
        Name = name;
        Description = description;
        ImgUrl = imgUrl;
        Id = id;
    }
}





public class HTMLRenderer : Renderer
{
    public void RenderSimplePage(string title, string content)
    {
        Console.WriteLine($"HTML page with title-> {title}      Content-> {content}");
    }

    public void RenderProductPage(Product product)
    {
        Console.WriteLine($"HTML product {product.Name}     Description -> {product.Description}    Image -> {product.ImgUrl}   Product ID-> {product.Id}\n");
    }
}


public class JSONRenderer : Renderer
{
    public void RenderSimplePage(string title, string content)
    {
        Console.WriteLine($"JSON page with title-> {title}      Content-> {content}");
    }

    public void RenderProductPage(Product product)
    {
        Console.WriteLine($"JSON product {product.Name}     Description -> {product.Description}    Image -> {product.ImgUrl}   Product ID-> {product.Id}\n");
    }
}

public class XMLRenderer : Renderer
{
    public void RenderSimplePage(string title, string content)
    {
        Console.WriteLine($"XML page with title-> {title}      Content-> {content}");
    }

    public void RenderProductPage(Product product)
    {
        Console.WriteLine($"XML product {product.Name}     Description -> {product.Description}    Image -> {product.ImgUrl}   Product ID-> {product.Id}\n");
    }
}
