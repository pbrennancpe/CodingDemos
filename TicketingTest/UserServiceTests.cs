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
public class UserServiceTests
{

    private TicketingDBContext _context;

    private static readonly Guid User1Id = Guid.Parse("3c9a2c7e-1c49-4c3b-bf1d-f8e2c3e57a45");
    private static readonly Guid User2Id = Guid.Parse("b27a8b0c-593f-4b24-b7b1-8a1e3a9747a3");
    private static readonly Guid User3Id = Guid.Parse("e9df2f53-7a6a-4a4e-9318-90b57c201c7f");

    


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

        _context.SaveChanges();

    }
    [Test]
    public async Task GetAllUsers()
    {
        //Arrange
        var service = new UserService(_context);
        //Act
        var users = await service.GetUsers();
        var count = _context.Tickets.Count();
        //Assert
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Count, Is.EqualTo(3));
        Assert.That(users.Any(x => x.Id == User1Id),
            Is.True, "Expected ticket ID not found in result");
        Assert.That(users.Any(x => x.Name == "Bruce Wayne"), Is.True, "Expected Name not found in result");
        Assert.That(users.Any(x => x.Email == "spiderman@asdf.com"), Is.True, "Expected Email not found in result");

    }
     

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

}
