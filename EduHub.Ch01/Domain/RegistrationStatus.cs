namespace EduHub.Ch01.Domain;

/// <summary>Trạng thái của phiếu đăng ký.</summary>
public enum RegistrationStatus
{
    /// <summary>Đang chờ xác nhận.</summary>
    Pending,
    /// <summary>Đã được xác nhận.</summary>
    Confirmed,
    /// <summary>Đã bị huỷ.</summary>
    Cancelled
}