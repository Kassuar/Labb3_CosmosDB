using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CustomerDbTrigger;

public class CustomerTrigger
{
    private readonly ILogger<CustomerTrigger> _logger;

    public CustomerTrigger(
        ILogger<CustomerTrigger> logger)
    {
        _logger = logger;
    }

    [Function("CustomerTrigger")]
    public async Task Run(

        [CosmosDBTrigger(
        databaseName: "CrmDb",
        containerName: "Customer",
        Connection = "CosmosConnection",
        LeaseContainerName = "leases",
        CreateLeaseContainerIfNotExists = true)]

        IReadOnlyList<Customer> input)
    {
        foreach (var customer in input)
        {
            _logger.LogInformation(
                $"Customer: {customer.Name}");

            _logger.LogInformation(
                $"Seller: {customer.Seller.Name}");

            await SendMail(customer);
        }
    }

    private async Task SendMail(
        Customer customer)
    {
        var smtp =
            new SmtpClient(
                "sandbox.smtp.mailtrap.io",
                2525);

        smtp.Credentials =
            new NetworkCredential("e86c2a4be60107", "e92444ddb1d575");

        smtp.EnableSsl = true;

        var mail = new MailMessage();

        mail.From = new MailAddress("crm@test.se");

        mail.To.Add(customer.Seller.Email);

        mail.Subject ="New Customer Added";

        mail.Body =
            $"Hello {customer.Seller.Name}\n\n" +
            $"You have been the seller for.\n\n" +
            $"Customer: {customer.Name}\n" +
            $"Title: {customer.Title}\n" +
            $"Telephone: {customer.Telephone}\n" +
            $"Email: {customer.Email}\n" +
            $"Adress: {customer.Address}\n";

        await smtp.SendMailAsync(mail);

        _logger.LogInformation($"Mail has been sent to {customer.Seller.Email}");
    }
}

public class Customer
{
    public string? id { get; set; }

    public string Name { get; set; }
        = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Telephone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public Seller Seller { get; set; } = new Seller();
}

public class Seller
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Telephone { get; set; } = string.Empty;
}
