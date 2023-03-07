using Ardalis.Specification;

namespace BirdTournaments.Core.UserAggregate.Specifications;
internal class UserByUsernamePassword : Specification<User>, ISingleResultSpecification
{
  public UserByUsernamePassword(string username, string password)
  {
    Query
      .Where(u => u.UserName == username && u.Hash == password)
      .Include(u => u.BirdOwner);
    
      //.Where(u => u.Hash == password);
  }
}
