namespace EduHub.Ch01.Domain;

/// <summary>Câu lạc bộ sinh viên.</summary>
public sealed class Club
{
    private readonly List<Member> _members = new();

    /// <summary>Mã định danh của câu lạc bộ.</summary>
    public Guid Id { get; private set; }
    /// <summary>Tên câu lạc bộ.</summary>
    public string Name { get; private set; } = string.Empty;
    /// <summary>Mô tả câu lạc bộ.</summary>
    public string Description { get; private set; } = string.Empty;
    /// <summary>Thời điểm thành lập.</summary>
    public DateTime CreatedAt { get; private set; }
    /// <summary>Danh sách thành viên (chỉ đọc).</summary>
    public IReadOnlyList<Member> Members => _members;

    private Club() { }

    /// <summary>Tạo câu lạc bộ mới sau khi kiểm tra dữ liệu đầu vào.</summary>
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name must not be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description must not be empty.", nameof(description));

        return new Club
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description.Trim(),
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>Thêm thành viên vào câu lạc bộ; chặn null và thêm trùng.</summary>
    public void AddMember(Member member)
    {
        ArgumentNullException.ThrowIfNull(member);
        if (_members.Any(m => m.Id == member.Id))
            throw new InvalidOperationException("Member already belongs to this club.");
        _members.Add(member);
    }

    /// <summary>Số thành viên hiện có.</summary>
    public int MemberCount() => _members.Count;
}