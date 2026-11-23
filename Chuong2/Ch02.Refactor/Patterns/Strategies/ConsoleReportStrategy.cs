namespace EduHub.Ch02.Patterns.Strategies;

public class ConsoleReportStrategy : IReportStrategy
{
    public string Name => "Console";

    public string Render(IReadOnlyList<ClubStat> stats) =>
        string.Join(Environment.NewLine,
            stats.Select(s => $"Club: {s.ClubName} | Members: {s.Members} | Events: {s.Events}"));
}