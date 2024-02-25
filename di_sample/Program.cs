namespace di_sample;
public static class Program
{
    public static void Main(string[] args)
    {
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            OrderNumber = "123",
            OrderDate = DateTime.Now,
            CustomerName = "John Doe",
            CustomerEmail = "john@example.com"
        };

        IEmailService emailService = new EmailService();
        IOrderService orderService = new OrderService(emailService);

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

    public OrderService(IEmailService emailService)
    {
        this.emailService = emailService;
    }

    public void PlaceOrder(Order order)
    {
        // Place order
        emailService.SendEmail(order.CustomerEmail!, "Order Placed", "Your order has been placed");
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
