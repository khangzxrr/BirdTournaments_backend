using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace BirdTournaments.Core.ParticipantAggregate;
public class ParticipantStatus: SmartEnum<ParticipantStatus>
{
  public static readonly ParticipantStatus Joined = new(nameof(Joined), 0);
  public static readonly ParticipantStatus Submited = new(nameof(Submited), 1);
  public static readonly ParticipantStatus Win = new(nameof(Win), 2);
  public static readonly ParticipantStatus Lose = new(nameof(Lose), 2);

  protected ParticipantStatus(string name, int value) : base(name, value) { }

}
