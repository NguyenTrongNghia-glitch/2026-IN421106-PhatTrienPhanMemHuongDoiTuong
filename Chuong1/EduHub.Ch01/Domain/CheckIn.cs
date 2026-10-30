namespace EduHub.Ch01.Domain;

/// <summary>Bản ghi điểm danh tại sự kiện.</summary>
public sealed class CheckIn
{
    /// <summary>Mã định danh của bản ghi điểm danh.</summary>
    public Guid Id { get; private set; }
    /// <summary>Mã phiếu đăng ký (khoá ngoại).</summary>
    public Guid RegistrationId { get; private set; }
    /// <summary>Thời điểm điểm danh.</summary>
    public DateTime CheckedInAt { get; private set; }
    /// <summary>Hình thức điểm danh.</summary>
    public CheckInMethod Method { get; private set; }

    private CheckIn() { }

    /// <summary>Tạo bản ghi điểm danh mới.</summary>
    public static CheckIn Create(Guid registrationId, CheckInMethod method)
    {
        if (registrationId == Guid.Empty)
            throw new ArgumentException("RegistrationId must not be empty.", nameof(registrationId));
        if (!Enum.IsDefined(method))
            throw new ArgumentException("Check-in method is invalid.", nameof(method));

        return new CheckIn
        {
            Id = Guid.NewGuid(),
            RegistrationId = registrationId,
            CheckedInAt = DateTime.UtcNow,
            Method = method
        };
    }

    /// <summary>Trả về chuỗi mô tả bản ghi điểm danh.</summary>
    public string Summary() => $"CheckIn {Id} | Registration {RegistrationId} | {Method} | {CheckedInAt:HH:mm:ss}";
}