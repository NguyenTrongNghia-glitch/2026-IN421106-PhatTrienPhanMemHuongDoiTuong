using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Repositories;

public class InMemoryEventRepository : IEventRepository
{
    private readonly List<Event> _events = new();

    public IReadOnlyList<Event> GetByClub(Guid clubId) =>
        _events.Where(e => e.ClubId == clubId).ToList();

    public void Add(Event ev) => _events.Add(ev);
}