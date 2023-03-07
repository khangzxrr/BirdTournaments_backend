using System.Numerics;
using BirdTournaments.Core.BirdAggregate;
using BirdTournaments.Core.BirdOwnerAggregate;
using BirdTournaments.Core.ContributorAggregate;
using BirdTournaments.Core.ParticipantAggregate;
using BirdTournaments.Core.ProjectAggregate;
using BirdTournaments.Core.UserAggregate;
using BirdTournaments.Infrastructure.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BirdTournaments.Web;

public static class SeedData
{
  public static readonly Contributor Contributor1 = new ("Ardalis");
  public static readonly Contributor Contributor2 = new ("Snowfrog");
  public static readonly Project TestProject1 = new Project("Test Project", PriorityStatus.Backlog);
  public static readonly ToDoItem ToDoItem1 = new ToDoItem
  {
    Title = "Get Sample Working",
    Description = "Try to get the sample to build."
  };
  public static readonly ToDoItem ToDoItem2 = new ToDoItem
  {
    Title = "Review Solution",
    Description = "Review the different projects in the solution and how they relate to one another."
  };
  public static readonly ToDoItem ToDoItem3 = new ToDoItem
  {
    Title = "Run and Review Tests",
    Description = "Make sure all the tests run and review what they are doing."
  };

  public static readonly Rank SilverRank = new Rank
  {
      MinElo = 0,
      Name = "silver"
  };
  public static readonly Rank GoldRank = new Rank
  {
    MinElo = 1701,
    Name = "gold"
  };
  public static readonly Rank PlatinumRank = new Rank
  {
    MinElo = 1901,
    Name = "platinum"
  };
  public static readonly Rank DiamondRank = new Rank
  {
    MinElo = 2101,
    Name = "diamond"
  };

  public static readonly BirdType BirdType1 = new BirdType("Vàng anh");
  public static readonly BirdType BirdType2 = new BirdType("Sáo");
  public static readonly BirdType BirdType3 = new BirdType("Chào Mào");

  public static readonly User user1 = new User("khangzxrr", "khangzxrr@gmail.com", "4297f44b13955235245b2497399d7a93", "salt", UserVerify.actived, UserRole.player);
  public static readonly BirdOwner birdOwner1 = new BirdOwner("mr khang vo", "149 xa lo ha noi p. thao dien, quan 2", "0919092211", "vipprozx!");
  public static readonly Bird bird1 = new Bird("Vàng anh pro vip", "https://picsum.photos/200/300", 500);

  public static readonly User user2 = new User("khangzxrr2", "khangzxrr2@gmail.com", "4297f44b13955235245b2497399d7a93", "salt", UserVerify.actived, UserRole.player);
  public static readonly BirdOwner birdOwner2 = new BirdOwner("mr vo vo", "299 xa lo ha noi p. thao dien, quan 2", "0988111790", "vipprozx!");
  public static readonly Bird bird2 = new Bird("Sáo sậu sổ lồng", "https://picsum.photos/200/300", 1701);

  public static readonly User user3 = new User("admin", "khangzxrr3@gmail.com", "4297f44b13955235245b2497399d7a93", "salt", UserVerify.actived, UserRole.player);
  public static readonly Moderator moderator1 = new Moderator("match_checker");


  public static readonly Region region1 = new Region("Quan 1");
  public static readonly Region region2 = new Region("Quan 2");

  public static readonly Place place1 = new Place("Nhà thi đấu quận 1");
  public static readonly Place place2 = new Place("Nhà thi đấu quận 2");

  public static readonly Competition competition1 = new Competition(DateTime.Now);
  public static readonly Participant participant1 = new Participant();
  public static void Initialize(IServiceProvider serviceProvider)
  {
    using (var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
    {
      // Look for any TODO items.
      //if (dbContext.ToDoItems.Any())
      //{
      //  return;   // DB has been seeded
      //}

      //PopulateTestData(dbContext);


    }
  }
  public static void PopulateTestData(AppDbContext dbContext)
  {
    foreach (var item in dbContext.Projects)
    {
      dbContext.Remove(item);
    }
    foreach (var item in dbContext.ToDoItems)
    {
      dbContext.Remove(item);
    }
    foreach (var item in dbContext.Contributors)
    {
      dbContext.Remove(item);
    }
    foreach(var rank in dbContext.Ranks)
    {
      dbContext.Remove(rank);
    }
    foreach (var user in dbContext.Users)
    {
      dbContext.Remove(user);
    }
    foreach(var birdType in dbContext.BirdTypes)
    {
      dbContext.Remove(birdType);
    }
    foreach(var region in dbContext.Regions)
    {
      dbContext.Remove(region);
    }
    foreach(var paticipants in dbContext.Participants)
    {
      dbContext.Remove(paticipants);
    }
    foreach(var competition in dbContext.Competitions)
    {
      dbContext.Remove(competition);
    }

    dbContext.SaveChanges();

    dbContext.Contributors.Add(Contributor1);
    dbContext.Contributors.Add(Contributor2);

    dbContext.SaveChanges();

    ToDoItem1.AddContributor(Contributor1.Id);
    ToDoItem2.AddContributor(Contributor2.Id);
    ToDoItem3.AddContributor(Contributor1.Id);

    TestProject1.AddItem(ToDoItem1);
    TestProject1.AddItem(ToDoItem2);
    TestProject1.AddItem(ToDoItem3);
    dbContext.Projects.Add(TestProject1);

    dbContext.Ranks.Add(SilverRank);
    dbContext.Ranks.Add(GoldRank);
    dbContext.Ranks.Add(PlatinumRank);
    dbContext.Ranks.Add(DiamondRank);

    dbContext.BirdTypes.Add(BirdType1);
    dbContext.BirdTypes.Add(BirdType2);
    dbContext.BirdTypes.Add(BirdType3);

    dbContext.Regions.Add(region1);
    dbContext.Regions.Add(region2);

    dbContext.SaveChanges();

    place1.SetRegion(region1);
    place2.SetRegion(region2);

    dbContext.Places.Add(place1);
    dbContext.Places.Add(place2);

    dbContext.Users.Add(user1);
    dbContext.Users.Add(user2);
    dbContext.Users.Add(user3);

    dbContext.SaveChanges();

    moderator1.SetUser(user3);
    dbContext.Moderators.Add(moderator1);

    birdOwner1.SetUser(user1);
    birdOwner2.SetUser(user2);  

    dbContext.BirdOwners.Add(birdOwner1);
    dbContext.BirdOwners.Add(birdOwner2);
    dbContext.SaveChanges();

    bird1.SetRank(SilverRank);
    bird1.SetBirdType(BirdType1);

    bird2.SetRank(GoldRank);
    bird2.SetBirdType(BirdType2);

    birdOwner1.AddBird(bird1);
    birdOwner2.AddBird(bird2);
    
    dbContext.SaveChanges();

    competition1.SetStatus(CompetitionStatus.WaitingForOpponent);
    competition1.SetBirdType(BirdType1);
    competition1.SetModerator(moderator1);
    competition1.SetPlace(place1);

    dbContext.Competitions.Add(competition1);
    dbContext.SaveChanges();

    participant1.SetBird(bird1);
    participant1.SetBirdOwner(birdOwner1);
    participant1.SetParticipantStatus(ParticipantStatus.Joined);

    competition1.AddParticipant(participant1);

    dbContext.SaveChanges();

  }
}
