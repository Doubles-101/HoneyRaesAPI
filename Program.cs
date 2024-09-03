using HoneyRaesAPI.Models;
using System.Collections.Generic;

List<Customer> customers = new List<Customer>
{
    new Customer { Id = 1, Name = "John Doe", Address = "123 Main St" },
    new Customer { Id = 2, Name = "Jane Smith", Address = "456 Elm St" },
    new Customer { Id = 3, Name = "Bob Johnson", Address = "789 Maple Ave" }
};

List<Employee> employees = new List<Employee>
{
    new Employee { Id = 1, Name = "Alice Brown", Specialty = "Electrical" },
    new Employee { Id = 2, Name = "Charlie Davis", Specialty = "Plumbing" }
};

List<ServiceTicket> serviceTickets = new List<ServiceTicket>
{
    new ServiceTicket { Id = 1, CustomerId = 1, EmployeeId = 1, Description = "Fix wiring", Emergency = false },
    new ServiceTicket { Id = 2, CustomerId = 2, EmployeeId = 2, Description = "Fix leaky faucet", Emergency = true, DateCompleted = DateOnly.FromDateTime(DateTime.Now) },
    new ServiceTicket { Id = 3, CustomerId = 3, EmployeeId = 1, Description = "Install new light", Emergency = false },
    new ServiceTicket { Id = 4, CustomerId = 1, Description = "Emergency water leak", Emergency = true }, // Unassigned
    new ServiceTicket { Id = 5, CustomerId = 2, EmployeeId = 2, Description = "Repair heater", Emergency = false } // Incomplete
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/servicetickets", () =>
{
    return serviceTickets.Select(t => new ServiceTicketDTO
    {
        Id = t.Id,
        CustomerId = t.CustomerId,
        EmployeeId = t.EmployeeId,
        Description = t.Description,
        Emergency = t.Emergency,
        DateCompleted = t.DateCompleted
    });
});

app.MapGet("/servicetickets/{id}", (int id) =>
{
    ServiceTicket serviceTicket = serviceTickets.FirstOrDefault(st => st.Id == id);
  
    return new ServiceTicketDTO
    {
        Id = serviceTicket.Id,
        CustomerId = serviceTicket.CustomerId,
        EmployeeId = serviceTicket.EmployeeId,
        Description = serviceTicket.Description,
        Emergency = serviceTicket.Emergency,
        DateCompleted = serviceTicket.DateCompleted
    };
});

app.MapGet("/hello", () =>
{
    return "hello";
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
