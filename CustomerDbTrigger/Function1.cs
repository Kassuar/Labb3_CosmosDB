using System;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CustomerDbTrigger;

public class CustomerTrigger
{
    private readonly ILogger<CustomerTrigger>
        _logger;

    public CustomerTrigger(
        ILogger<CustomerTrigger>
        logger
    )
    {
        _logger = logger;
    }

    [Function(
        "CustomerTrigger"
    )]

    public void Run(

        [CosmosDBTrigger(
            databaseName:
            "CrmDb",

            containerName:
            "Customer",

            Connection =
            "CosmosConnection",

            LeaseContainerName =
            "leases",

            CreateLeaseContainerIfNotExists =
            true
        )]

        IReadOnlyList<Customer>
        input
    )
    {
        foreach (
            var customer
            in input
        )
        {
            _logger
            .LogInformation(
                $"Customer: {customer.Name}"
            );

            _logger
            .LogInformation(
                $"Seller: {customer.Seller.Name}"
            );
        }
    }
}

public class Customer
{
    public string? id { get; set; }

    public string Name { get; set; }
        = string.Empty;

    public string Title { get; set; }
        = string.Empty;

    public string Telephone { get; set; }
        = string.Empty;

    public string Email { get; set; }
        = string.Empty;

    public string Address { get; set; }
        = string.Empty;

    public Seller Seller { get; set; }
        = new();
}

public class Seller
{
    public string Name { get; set; }
        = string.Empty;

    public string Email { get; set; }
        = string.Empty;

    public string Telephone { get; set; }
}