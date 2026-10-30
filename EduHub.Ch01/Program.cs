using EduHub.Ch01.Domain;

const string Separator = "==================================================";

static void PrintStep(int step, string title)
{
    Console.WriteLine();
    Console.WriteLine(Separator);
    Console.WriteLine($"BƯỚC {step}: {title}");
    Console.WriteLine(Separator);
}

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Bước 1: Tạo CLB
PrintStep(1, "Tạo câu lạc bộ");
var club = Club.Create("Tech Club", "Câu lạc bộ công nghệ dành cho sinh viên.");
Console.WriteLine($"Đã tạo CLB: {club.Name} - {club.Description}");

// Bước 2: Thêm 3 thành viên
PrintStep(2, "Thêm thành viên");
var members = new[]
{
    Member.Create("Nguyễn Văn An", "an@example.com", "0024410001"),
    Member.Create("Trần Thị Bình", "binh@example.com", "0024410002"),
    Member.Create("Lê Văn Cường", "cuong@example.com", "0024410003")
};
foreach (var m in members)
{
    club.AddMember(m);
    Console.WriteLine($"Đã thêm: {m.Name} ({m.StudentCode})");
}
Console.WriteLine($"Tổng số thành viên: {club.MemberCount()}");

// Bước 3: Tạo sự kiện
PrintStep(3, "Tạo sự kiện");
var ev = Event.Create(club.Id, "Workshop AI", DateTime.UtcNow.AddDays(7), 50, isOnline: false);
Console.WriteLine($"Sự kiện: {ev.Title} | Sức chứa: {ev.Capacity}");

// Bước 4: Đăng ký và xác nhận
PrintStep(4, "Đăng ký và xác nhận");
var registrations = new List<Registration>();
foreach (var m in members.Take(2))
{
    if (!ev.HasCapacity(registrations.Count))
        break;
    var reg = Registration.Create(m.Id, ev.Id);
    Console.WriteLine($"{m.Name} đăng ký -> {reg.Status}");
    reg.Confirm();
    Console.WriteLine($"{m.Name} sau Confirm() -> {reg.Status}");
    registrations.Add(reg);
}

// Bước 5: Check-in
PrintStep(5, "Điểm danh");
var checkIns = new List<CheckIn>();
var checkIn = CheckIn.Create(registrations[0].Id, CheckInMethod.QrCode);
checkIns.Add(checkIn);
Console.WriteLine(checkIn.Summary());

// Bước 6: Báo cáo
PrintStep(6, "Báo cáo tham dự");
Console.WriteLine($"Số đăng ký : {registrations.Count}");
Console.WriteLine($"Số check-in: {checkIns.Count}");
Console.WriteLine("Danh sách đã check-in:");
foreach (var c in checkIns)
{
    var reg = registrations.First(r => r.Id == c.RegistrationId);
    var member = members.First(m => m.Id == reg.MemberId);
    Console.WriteLine($" - {member.Name}");
}

// Bước 7 (tuỳ chọn): Vi phạm luật nghiệp vụ
PrintStep(7, "Thử thao tác vi phạm luật nghiệp vụ");
try
{
    registrations[1].Cancel();
    registrations[1].Confirm();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Bắt được lỗi đúng như mong đợi: {ex.Message}");
}