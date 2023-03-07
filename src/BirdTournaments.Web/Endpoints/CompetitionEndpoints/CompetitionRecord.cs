namespace BirdTournaments.Web.Endpoints.CompetitionEndpoints;

public record CompetitionRecord(int Id, DateTime date, string place, string birdType, int elo, string status);
