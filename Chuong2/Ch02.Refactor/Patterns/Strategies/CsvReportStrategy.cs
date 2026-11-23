namespace EduHub.Ch02.Patterns.Strategies;

public class CsvReportStrategy : IReportStrategy
{
    public string Name => "CSV";

    public string Render(IReadOnlyList<ClubStat> stats) =>
        "Club,Members,Events" + Environment.NewLine +
        string.Join(Environment.NewLine,
            stats.Select(s => $"\"{s.ClubName}\",{s.Members},{s.Events}"));
}