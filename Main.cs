using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Functional Delivery Calculator");
        Console.WriteLine();

        Console.Write("Enter base delivery price: ");
        string? priceInput = Console.ReadLine();

        if (!decimal.TryParse(priceInput, out decimal basePrice) || basePrice <= 0)
        {
            Console.WriteLine("Invalid price. Please enter a positive number.");
            return;
        }

        Console.Write("Enter number of items: ");
        string? itemsInput = Console.ReadLine();

        if (!int.TryParse(itemsInput, out int items) || items <= 0)
        {
            Console.WriteLine("Invalid number of items. Please enter a positive integer.");
            return;
        }

        Console.Write("Express delivery (true/false): ");
        string? expressInput = Console.ReadLine();

        if (!bool.TryParse(expressInput, out bool express))
        {
            Console.WriteLine("Invalid express value. Please enter true or false.");
            return;
        }

        Console.Write("Delivery type (Pickup/Courier/DoorToDoor): ");
        string? typeInput = Console.ReadLine();

        if (!Enum.TryParse(typeInput, true, out DeliveryType deliveryType))
        {
            Console.WriteLine("Invalid delivery type.");
            return;
        }

        Console.Write("Delivery zone (City/OutsideCity/Remote): ");
        string? zoneInput = Console.ReadLine();

        if (!Enum.TryParse(zoneInput, true, out DeliveryZone deliveryZone))
        {
            Console.WriteLine("Invalid delivery zone.");
            return;
        }

        decimal finalPrice = CalculateDeliveryPrice(
            basePrice,
            items,
            deliveryType,
            deliveryZone,
            express
        );

        Console.WriteLine();
        Console.WriteLine($"Final delivery price: {finalPrice:F2}");
    }

    static decimal ApplyRule(
        decimal price,
        Func<decimal, decimal> rule) =>
        rule(price);
    static decimal CalculateDeliveryPrice(
        decimal basePrice,
        int items,
        DeliveryType deliveryType,
        DeliveryZone deliveryZone,
        bool express)
    {
        decimal price = basePrice;

        Func<decimal, decimal> itemRule =
            currentPrice => ApplyItemsRule(currentPrice, items);

        price = ApplyRule(price, itemRule);        
        
        Func<decimal, decimal> deliveryTypeRule =
            currentPrice => ApplyDeliveryTypeRule(currentPrice, deliveryType);

        price = ApplyRule(price, deliveryTypeRule);
        
        Func<decimal, decimal> deliveryZoneRule =
            currentPrice => ApplyDeliveryZoneRule(currentPrice, deliveryZone);

        price = ApplyRule(price, deliveryZoneRule);
        
        Func<decimal, decimal> expressRule =
            currentPrice => ApplyExpressRule(currentPrice, express);

        price = ApplyRule(price, expressRule);

        return Math.Round(price, 2, MidpointRounding.AwayFromZero);
    }

    static decimal ApplyItemsRule(decimal price, int items)
    {
        if (items <= 3)
        {
            return price;
        }
        else if (items <= 7)
        {
            return price * 1.10m;
        }
        else
        {
            return price * 1.20m;
        }
    }

    static decimal ApplyDeliveryTypeRule(
        decimal price,
        DeliveryType deliveryType)
    {
        switch (deliveryType)
        {
            case DeliveryType.Pickup:
                return price * 0.80m;

            case DeliveryType.Courier:
                return price;

            case DeliveryType.DoorToDoor:
                return price * 1.15m;

            default:
                return price;
        }
    }

    static decimal ApplyDeliveryZoneRule(
        decimal price,
        DeliveryZone deliveryZone)
    {
        switch (deliveryZone)
        {
            case DeliveryZone.City:
                return price;

            case DeliveryZone.OutsideCity:
                return price * 1.25m;

            case DeliveryZone.Remote:
                return price;

            default:
                return price;
        }
    }

    static decimal ApplyExpressRule(
        decimal price,
        bool express)
    {
        if (express)
        {
            return price * 1.30m;
        }

        return price;
    }
}