using System;

public interface IDeliveryStrategy
{
    double DeliveryCost(double distance, double weight, double tax);
}

public class PickupDelivery : IDeliveryStrategy
{
    public double DeliveryCost(double distance, double weight, double tax)
    {
        return 0; 
    }
}

public class ExternalDelivery : IDeliveryStrategy
{
    public double DeliveryCost(double distance, double weight, double tax)
    {
        tax = tax / 100;
        return distance * 3 + weight * 2 + tax;
    }
}

public class InternalDelivery : IDeliveryStrategy
{
    public double DeliveryCost(double distance, double weight, double tax)
    {
        tax = tax / 100;
        return distance * 2 + weight * 0.5 + tax;
    }
}

public class DeliveryContext
{
    private IDeliveryStrategy _deliveryStrategy;

    public void SetDelivery(IDeliveryStrategy deliveryStrategy)
    {
        _deliveryStrategy = deliveryStrategy;
    }

    public double CalculateCost(double distance, double weight, double tax)
    {
        if (_deliveryStrategy == null)
        {
            throw new InvalidOperationException("Set delivary type!");
        }
        return _deliveryStrategy.DeliveryCost(distance, weight, tax);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        DeliveryContext context = new DeliveryContext();

        context.SetDelivery(new PickupDelivery());
        Console.WriteLine("Pickup ->  " + context.CalculateCost(10, 5,5));

        context.SetDelivery(new ExternalDelivery());
        Console.WriteLine("External Delivery -> " + context.CalculateCost(10, 5, 5));

        context.SetDelivery(new InternalDelivery());
        Console.WriteLine("Internal Delivery - > " + context.CalculateCost(10, 5, 5));
    }
}
