namespace EduHub.Ch01.Domain;

/// <summary>Sự kiện do câu lạc bộ tổ chức.</summary>
public sealed class Event
{
    /// <summary>Mã định danh của sự kiện.</summary>
    public Guid Id { get; private set; }
    /// <summary>Mã câu lạc bộ tổ chức (khoá ngoại).</summary>
    public Guid ClubId { get; private set; }
    /// <summary>Tiêu đề sự kiện.</summary>
    public string Title { get; private set; } = string.Empty;
    /// <summary>Ngày diễn ra.</summary>
    public DateTime Date { get; private set; }
    /// <summary>Sức chứa tối đa.</summary>
    public int Capacity { get; private set; }
    /// <summary>Sự kiện có tổ chức trực tuyến hay không.</summary>
    public bool IsOnline { get; private set; }

    private Event() { }

    /// <summary>Tạo sự kiện mới sau khi kiểm tra dữ liệu đầu vào.</summary>
    public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        if (clubId == Guid.Empty)
            throw new ArgumentException("ClubId must not be empty.", nameof(clubId));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title must not be empty.", nameof(title));
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than 0.", nameof(capacity));

        return new Event
        {
            Id = Guid.NewGuid(),
            ClubId = clubId,
            Title = title.Trim(),
            Date = date,
            Capacity = capacity,
            IsOnline = isOnline
        };
    }

    /// <summary>Cho biết sự kiện còn chỗ hay không.</summary>
    public bool HasCapacity(int registeredCount) => registeredCount < Capacity;
}