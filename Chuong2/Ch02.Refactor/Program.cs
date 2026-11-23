using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Patterns.Observers;
using EduHub.Ch02.Patterns.Strategies;
using EduHub.Ch02.Repositories;
using EduHub.Ch02.Services;

// Composition root: nơi duy nhất dùng new cho class cụ thể
IAppLogger logger = new ConsoleLogger();
INotificationServiceFactory factory = new NotificationServiceFactory();
INotificationService notifier = factory.Create(NotificationChannel.Email);

IClubRepository clubRepo = new LoggingClubRepository(new InMemoryClubRepository(), logger);
IMemberRepository memberRepo = new InMemoryMemberRepository();
IEventRepository eventRepo = new InMemoryEventRepository();

IClubService clubService = new ClubService(clubRepo, memberRepo, notifier, logger);
IEventService eventService = new EventService(clubRepo, eventRepo);
IReportService reportService = new ReportService(clubRepo, memberRepo, eventRepo);

eventService.Subscribe(new LogNotifier(logger));
eventService.Subscribe(new EmailNotifier(memberRepo, notifier));

Console.WriteLine("== 1. Tạo CLB và thành viên ==");
var club = clubService.CreateClub("Tech Club", "Câu lạc bộ công nghệ", "president@gmail.com");
clubService.AddMember(club.Id, "Nguyễn Văn A", "a@student.dthu.edu.vn");

Console.WriteLine("\n== 2. Observer: tạo sự kiện ==");
eventService.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 20));

Console.WriteLine("\n== 3. Strategy: báo cáo ==");
Console.WriteLine(reportService.Generate(new ConsoleReportStrategy()));
Console.WriteLine(reportService.Generate(new CsvReportStrategy()));

Console.WriteLine("\n== 4. Factory: thử từng kênh ==");
foreach (var channel in Enum.GetValues<NotificationChannel>())
    factory.Create(channel).Send("a@student.dthu.edu.vn", $"Thử {channel}");

Console.WriteLine("\n== 5. Kiểm tra dữ liệu sai ==");
try { clubService.CreateClub("", "mô tả", "p@gmail.com"); }
catch (ArgumentException ex) { Console.WriteLine($"Lỗi: {ex.Message}"); }