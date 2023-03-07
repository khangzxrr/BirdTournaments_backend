using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace BirdTournaments.Core.ParticipantAggregate;
public class CompetitionStatus: SmartEnum<CompetitionStatus>
{
  public static readonly CompetitionStatus WaitingForOpponent = new(nameof(WaitingForOpponent), 0);
  public static readonly CompetitionStatus Happening = new(nameof(Happening), 1);
  public static readonly CompetitionStatus Ended = new(nameof(Ended), 2);
  public static readonly CompetitionStatus WaitingForVerify = new(nameof(WaitingForVerify), 3);

  protected CompetitionStatus(string name, int value): base(name, value) { }
}
