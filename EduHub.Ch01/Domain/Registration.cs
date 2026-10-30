namespace EduHub.Ch01.Domain;

/// <summary>Phiếu đăng ký tham dự sự kiện của một thành viên.</summary>
public sealed class Registration
{
    /// <summary>Mã định danh của phiếu đăng ký.</summary>
    public Guid Id { get; private set; }
    /// <summary>Mã thành viên (khoá ngoại).</summary>
    public Guid MemberId { get; private set; }
    /// <summary>Mã sự kiện (khoá ngoại).</summary>
    public Guid EventId { get; private set; }
    /// <summary>Thời điểm đăng ký.</summary>
    public DateTime RegisteredAt { get; private set; }
    /// <summary>Trạng thái hiện tại của phiếu.</summary>
    public RegistrationStatus Status { get; private set; }

    private Registration() { }

    /// <summary>Tạo phiếu đăng ký mới ở trạng thái Pending.</summary>
    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
            throw new ArgumentException("MemberId must not be empty.", nameof(memberId));
        if (eventId == Guid.Empty)
            throw new ArgumentException("EventId must not be empty.", nameof(eventId));

        return new Registration
        {
            Id = Guid.NewGuid(),
            MemberId = memberId,
            EventId = eventId,
            RegisteredAt = DateTime.UtcNow,
            Status = RegistrationStatus.Pending
        };
    }

    /// <summary>Xác nhận phiếu; chỉ hợp lệ khi đang Pending.</summary>
    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
            throw new InvalidOperationException($"Cannot confirm a registration in status {Status}.");
        Status = RegistrationStatus.Confirmed;
    }

    /// <summary>Huỷ phiếu; không được huỷ lặp lại.</summary>
    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("Registration is already cancelled.");
        Status = RegistrationStatus.Cancelled;
    }
}