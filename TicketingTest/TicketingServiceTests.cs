using NUnit.Framework;
using Moq;
using Ticketing;
using Ticketing.Data;
using Ticketing.Models;
using Ticketing.Services;
using Ticketing.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using Ticketing.Models.DTO;

namespace TicketingTest;

[TestFixture, NUnit.Framework.Category("Unit")]
public class TicketingServiceTests
{

    private TicketingDBContext _context;

    private static readonly Guid User1Id = Guid.Parse("3c9a2c7e-1c49-4c3b-bf1d-f8e2c3e57a45");
    private static readonly Guid User2Id = Guid.Parse("b27a8b0c-593f-4b24-b7b1-8a1e3a9747a3");
    private static readonly Guid User3Id = Guid.Parse("e9df2f53-7a6a-4a4e-9318-90b57c201c7f");

    private static readonly Guid Ticket1Id = Guid.Parse("0a26e2cf-2b38-43b7-8f93-6d5a2a22e5a4");
    private static readonly Guid Ticket2Id = Guid.Parse("d5e3f4a7-3e7a-4d0f-9c76-bba0e78d9e3f");
    private static readonly Guid Ticket3Id = Guid.Parse("74a63b1b-f9e2-44ed-bf07-3e5b8d2a2d76");
    private static readonly Guid Ticket4Id = Guid.Parse("bcbf0b13-1ee4-498f-a73f-4a3c2b8c479d");
    


    [SetUp]
    public void Setup()
    {

        var user1 = new User
        {
            Id = User1Id,
            Name = "Bruce Wayne",
            Email = "batman@asdf.com"
        };
        var user2 = new User
        {
            Id = User2Id,
            Name = "Peter Parker",
            Email = "spiderman@asdf.com"
        };
        var user3 = new User
        {
            Id = User3Id,
            Name = "Clark Kent"
        };

        var options = new DbContextOptionsBuilder<TicketingDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _context = new TicketingDBContext(options);

        _context.Users.AddRange(user1, user2, user3);
        _context.Tickets.AddRange(
            new Ticket
            {
                Id = Ticket1Id,
                Title = "Critical Bug",
                TicketNo = 1,
                Description = "Login page displays blank screen",
                TicketStatus = Status.Open,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow,
                AssignedUser = user1,
                AssignedUserId = user1.Id
            },
            new Ticket
            {
                Id = Ticket2Id,
                Title = "UI Glitch on Dashboard",
                TicketNo = 2,
                Description = "Misaligned image on page load",
                TicketStatus = Status.InProgress,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow,
                AssignedUser = user2,
                AssignedUserId = user2.Id
            },
            new Ticket
            {
                Id = Ticket3Id,
                Title = "Submit button disabled",
                TicketNo = 3,
                Description = "Form button does not enable even when all fields are filled out",
                TicketStatus = Status.Closed,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow,
                AssignedUser = user2,
                AssignedUserId = user2.Id
            },
            new Ticket
            {
                Id = Ticket4Id,
                Title = "Wrong Image Displays",
                TicketNo = 4,
                Description = "Wrong image displays on homepage",
                TicketStatus = Status.Closed,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow,
                AssignedUser = null,
                AssignedUserId = null
            }
        );

        _context.SaveChanges();

    }
    [Test]
    public async Task GetAllTickets()
    {
        //Arrange
        var service = new TicketService(_context);
        //Act
        var tickets = await service.GetTickets(new TicketQuery());
        var count = _context.Tickets.Count();
        //Assert
        Assert.That(tickets, Is.Not.Null);
        Assert.That(tickets.Count, Is.EqualTo(4));
        Assert.That(tickets.Any(x => x.Id == Ticket1Id),
            Is.True, "Expected ticket ID not found in result");

    }
    [Test]
    [TestCase("OPEN")]
    [TestCase("open")]
    public async Task FilterByStatus(string status)
    {
        //Arrange
        var service = new TicketService(_context);
        //Act
        var query = new TicketQuery
        {
           Status  = status
        };
        var tickets = await service.GetTickets(query);
        var count = _context.Tickets.Count();
        //Assert
        Assert.That(tickets, Is.Not.Null);
        Assert.That(tickets.Count, Is.EqualTo(1));
        Assert.That(tickets.Any(x => x.Id == Ticket1Id),
            Is.True, "Expected ticket ID not found in result");

    }
    [Test]
    public async Task FilterByUser()
    {
        //Arrange
        var service = new TicketService(_context);
        //Act
        var query = new TicketQuery
        {
            UserId = User2Id
        };
        var tickets = await service.GetTickets(query);
        var count = _context.Tickets.Count();
        //Assert
        Assert.That(tickets, Is.Not.Null);
        Assert.That(tickets.Count, Is.EqualTo(2));
        Assert.That(tickets.Any(x => x.Id == Ticket2Id),
            Is.True, "Expected ticket ID not found in result");

    }

    [Test]
    [TestCase("0a26e2cf-2b38-43b7-8f93-6d5a2a22e5a4")]
    [TestCase("d5e3f4a7-3e7a-4d0f-9c76-bba0e78d9e3f")]
    [TestCase("74a63b1b-f9e2-44ed-bf07-3e5b8d2a2d76")]
    public async Task GetTicketById(string guidString)
    {
        //Arrange
        var service = new TicketService(_context);
        //Act
        var ticket = await service.GetTicketById(Guid.Parse(guidString));
        //Assert
        Assert.That(ticket, Is.Not.Null);
    }

    [Test]
    public async Task GetTicketByNumber()
    {
        //Arrange
        var service = new TicketService(_context);
        int ticketNo = 3;
        //Act
        var ticket = await service.GetTicketByNo(ticketNo);
        //Assert
        Assert.That(ticket, Is.Not.Null);
        Assert.That(ticket.TicketNo, Is.EqualTo(3));
        Assert.That(ticket.Title, Is.EqualTo("Submit button disabled"));
    }

    [Test]
    public async Task TicketNotFound()
    {
        //arrange
        var service = new TicketService(_context);
        //Act
        var ticket = await service.GetTicketById(User1Id);
        //Assert
        Assert.That(ticket, Is.Null);
    }


    [Test]
    public async Task TicketNotFoundByNumber()
    {
        //arrange
        var service = new TicketService(_context);
        //Act
        var ticket = await service.GetTicketByNo(7);
        //Assert
        Assert.That(ticket, Is.Null);
    }
    [Test]
    public async Task UpdateTicketTitle()
    {
        //Arrange
        var before = DateTime.UtcNow;
        var service = new TicketService(_context);
        var request = new UpdateTicketDTO
        {
            Id = Ticket4Id,
            Title = "New Ticket Title"
        };
        //Act
        var ticketResponse = await service.UpdateTicket(request);
        var after = DateTime.UtcNow;
        //Assert
        var updatedTicket = await _context.Tickets.FindAsync(Ticket4Id);
        Assert.That(ticketResponse, Is.Not.Null);
        Assert.That(updatedTicket.Title, Is.EqualTo("New Ticket Title"));
        Assert.That(updatedTicket.Description, Is.EqualTo("Wrong image displays on homepage"));
        Assert.That(updatedTicket.UpdatedAt, Is.InRange(before, after));

    }
    [Test]
    public async Task UpdateTicketStaatus()
    {
        //Arrange
        var before = DateTime.UtcNow;
        var service = new TicketService(_context);
        var request = new UpdateTicketDTO
        {
            Id = Ticket4Id,
            TicketStatus = Status.InProgress
        };
        //Act
        var ticketResponse = await service.UpdateTicket(request);
        var after = DateTime.UtcNow;
        //Assert
        var updatedTicket = await _context.Tickets.FindAsync(Ticket4Id);
        Assert.That(ticketResponse, Is.Not.Null);
        Assert.That(updatedTicket.TicketStatus, Is.EqualTo(Status.InProgress));
        Assert.That(updatedTicket.Description, Is.EqualTo("Wrong image displays on homepage"));
        Assert.That(updatedTicket.UpdatedAt, Is.InRange(before, after));

    }
    [Test]
    public async Task UpdateTicketUser()
    {
        //Arrange
        var service = new TicketService(_context);
        var request = new UpdateTicketDTO
        {
            Id = Ticket3Id,
            AssignedUserId = User3Id
        };
        //Act
        var ticketResponse = await service.UpdateTicket(request);
        var updatedTicket = await _context.Tickets.FindAsync(Ticket3Id);
        //Assert

        Assert.That(ticketResponse, Is.Not.Null);
        Assert.That(updatedTicket.AssignedUser, Is.Not.Null);
        Assert.That(updatedTicket.AssignedUser.Name, Is.EqualTo("Clark Kent"));
        Assert.That(updatedTicket.Description, Is.EqualTo("Form button does not enable even when all fields are filled out"));

    }
    
    [Test]
    public async Task UpdateTicketUserNotFound()
    {
        //For this use case I decided to treat a user not found as if the user had not been 
        // included in the update JSON.
        //Also tests changing description
        //Arrange
        var service = new TicketService(_context);
        var request = new UpdateTicketDTO
        {
            Id = Ticket3Id,
            Description = "New Ticket Description",
            AssignedUserId = Ticket3Id
        };
        //Act
        var ticketResponse = await service.UpdateTicket(request);
        var updatedTicket = await _context.Tickets.FindAsync(Ticket3Id);
        //Assert

        Assert.That(ticketResponse, Is.Not.Null);
        
        Assert.That(updatedTicket.AssignedUser, Is.Not.Null);
        Assert.That(updatedTicket.AssignedUser.Name, Is.EqualTo("Peter Parker"));
        Assert.That(updatedTicket.Description, Is.EqualTo("New Ticket Description"));       

    }
    [Test]
    public async Task UpdateTicketTicketNotFound()
    {
        //For this use case I decided to treat a user not found as if the user had not been 
        // included in the update JSON.
        //Also tests changing description
        //Arrange
        var service = new TicketService(_context);
        var request = new UpdateTicketDTO
        {
            Id = User1Id,
            Description = "New Ticket Description",
            AssignedUserId = Ticket3Id
        };
        //Act
        var ticketResponse = await service.UpdateTicket(request);
        //Assert

        Assert.That(ticketResponse, Is.Null);

    }
    [Test]
        public async Task CreateTicket()
    {
        //For this use case I decided to treat a user not found as if the user had not been 
        // included in the update JSON.
        //Also tests changing description
        //Arrange
        var before = DateTime.UtcNow;
        var service = new TicketService(_context);
        var request = new TicketRequestDTO
        {
            Title = "New Ticket",
            Description = "New Ticket Description",
            AssignedUserId = Ticket3Id
        };
        //Act
        var ticketResponse = await service.CreateTicket(request);
        var tickets = await _context.Tickets.AsQueryable().ToListAsync();
        var ticket = tickets.FirstOrDefault(x => x.Title == "New Ticket");
        var after = DateTime.UtcNow;
        //Assert

        Assert.That(ticketResponse, Is.Not.Null);
        Assert.That(tickets.Count, Is.EqualTo(5));
        Assert.That(ticket, Is.Not.Null);
        Assert.That(ticket.Description, Is.EqualTo("New Ticket Description"));
        Assert.That(ticket.CreatedAt, Is.InRange(before, after));
        Assert.That(ticket.UpdatedAt, Is.InRange(before, after));


    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

}
