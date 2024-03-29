﻿using Microsoft.Extensions.DependencyInjection;

namespace di_sample;
public static class Program
{
    public static void Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IEventService, EventService>();
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            OrderNumber = "123",
            OrderDate = DateTime.Now,
            CustomerName = "John Doe",
            CustomerEmail = "john@example.com"
        };

        IOrderService orderService = serviceProvider.GetRequiredService<IOrderService>();

        orderService.PlaceOrder(order);
    }
}

public class Order
{
    public Guid OrderId { get; set; }
    public string? OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
}

public interface IOrderService
{
    void PlaceOrder(Order order);
}

public class OrderService : IOrderService
{
    private readonly IEmailService emailService;
    private readonly IEventService eventService;

    public OrderService(IEmailService emailService, IEventService eventService)
    {
        this.emailService = emailService;
        this.eventService = eventService;
    }

    public void PlaceOrder(Order order)
    {
        // Place order
        emailService.SendEmail(order.CustomerEmail!, "Order Placed", "Your order has been placed");

        // Raise event
        eventService.RaiseEvent("OrderPlaced");
    }
}

public interface IEventService
{
    void RaiseEvent(string eventName);
}

public class EventService : IEventService
{
    public void RaiseEvent(string eventName)
    {
        System.Console.WriteLine($"Event {eventName} raised");
    }
}

public interface IEmailService
{
    void SendEmail(string to, string subject, string body);
}

public class EmailService : IEmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        // Send email
        System.Console.WriteLine($"Email sent to {to} with subject {subject} and body {body}");
    }
}
