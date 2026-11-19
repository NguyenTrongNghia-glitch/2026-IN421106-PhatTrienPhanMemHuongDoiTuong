\# Phân tích vi phạm SOLID trong `ClubManager.cs`



\## 1. Vi phạm SRP (Single Responsibility Principle) — Lớp `ClubManager` ôm đồm quá nhiều trách nhiệm (God Class)

\- \*\*Vị trí:\*\* `Chuong2\\Ch02.Refactor\\ClubManager.cs`, dòng 28–118 (cụ thể phương thức `CreateClub` tại dòng 35–63, `AddMemberToClub` tại dòng 66–84, `CreateEvent` tại dòng 88–99, `PrintClubReport` tại dòng 103–113).

\- \*\*Nguyên lý:\*\* Single Responsibility Principle (SRP) — Một lớp hoặc phương thức chỉ nên có một lý do duy nhất để thay đổi. Tuy nhiên, `ClubManager` vừa quản lý nghiệp vụ câu lạc bộ, quản lý thành viên, tạo sự kiện, in báo cáo, vừa trực tiếp thao tác dữ liệu, ghi log ra file và gửi thông báo.

\- \*\*Hậu quả:\*\* Code phình to, các trách nhiệm bị ràng buộc chặt chẽ; khi thay đổi quy tắc nghiệp vụ của sự kiện hoặc định dạng báo cáo có thể làm hỏng chức năng quản lý câu lạc bộ và rất khó viết unit test cô lập từng phần.

\- \*\*Cách sửa:\*\* Tách nhỏ `ClubManager` thành các service chuyên biệt (`ClubService`, `EventService`, `ReportService`), chuyển luật kiểm tra hợp lệ vào domain entity (`Club.Create`), tách lưu trữ qua `IClubRepository`, ghi log qua `IAppLogger` và gửi thông báo qua `INotificationService`.



\## 2. Vi phạm OCP (Open/Closed Principle) — Rẽ nhánh cứng `if/else` theo loại email khi gửi thông báo

\- \*\*Vị trí:\*\* `Chuong2\\Ch02.Refactor\\ClubManager.cs`, dòng 50–62 (và gửi thông báo cố định qua Console trong `AddMemberToClub` tại dòng 81–83).

\- \*\*Nguyên lý:\*\* Open/Closed Principle (OCP) — Phần mềm nên mở để mở rộng nhưng đóng để sửa đổi. Đoạn code kiểm tra chuỗi `@gmail.com`, `@yahoo.com` bằng cấu trúc `if / else if / else` ngay trong `CreateClub` khiến mỗi lần thêm kênh mới (như SMS, Slack, Outlook) đều phải sửa trực tiếp mã nguồn của hàm này.

\- \*\*Hậu quả:\*\* Dễ phát sinh lỗi hồi quy (regression bug) ở các luồng gửi thông báo đang chạy ổn định mỗi khi bổ sung kênh thông báo mới.

\- \*\*Cách sửa:\*\* Áp dụng Factory Pattern (`INotificationServiceFactory.Create(NotificationChannel)`) kết hợp interface `INotificationService` để khởi tạo đúng kênh thông báo mà không cần sửa đổi logic nghiệp vụ trong `ClubService`.



\## 3. Vi phạm DIP (Dependency Inversion Principle) — Phụ thuộc trực tiếp vào `File.AppendAllText` và `Console.WriteLine`

\- \*\*Vị trí:\*\* `Chuong2\\Ch02.Refactor\\ClubManager.cs`, dòng 32, 46, 78, 94, 111 (`File.AppendAllText(\_logFilePath, ...)`) và dòng 52, 56, 60, 82–83, 97, 108 (`Console.WriteLine(...)`).

\- \*\*Nguyên lý:\*\* Dependency Inversion Principle (DIP) — Module cấp cao (logic nghiệp vụ) không nên phụ thuộc trực tiếp vào module cấp thấp (chi tiết I/O như ghi file cục bộ hay xuất ra màn hình console), mà cả hai nên phụ thuộc vào abstraction (interface).

\- \*\*Hậu quả:\*\* Không thể thay đổi cơ chế lưu log (ví dụ chuyển từ ghi file text sang ghi database hoặc cloud log) mà không sửa code nghiệp vụ; khi chạy unit test sẽ tạo ra file rác `club-log.txt` trên ổ đĩa và không kiểm chứng được hành vi ghi log/gửi tin bằng mock object.

\- \*\*Cách sửa:\*\* Định nghĩa các interface `IAppLogger` và `INotificationService`, truyền chúng qua constructor (Dependency Injection) vào các service, đồng thời dùng Decorator Pattern (`LoggingClubRepository` bọc `IClubRepository`) để tách việc ghi log truy cập dữ liệu.



\## 4. Vi phạm OCP \& SRP — Trộn lẫn logic xuất báo cáo ra Console vào lớp nghiệp vụ

\- \*\*Vị trí:\*\* `Chuong2\\Ch02.Refactor\\ClubManager.cs`, dòng 103–113 (phương thức `PrintClubReport`).

\- \*\*Nguyên lý:\*\* Open/Closed Principle (OCP) và Single Responsibility Principle (SRP) — Phương thức `PrintClubReport` vừa tính toán số lượng thành viên, vừa định dạng và xuất thẳng ra `Console.WriteLine`, đồng thời ghi log file. Nếu đề bài hoặc người dùng yêu cầu xuất báo cáo ra file CSV hoặc JSON thì phải sửa trực tiếp phương thức này.

\- \*\*Hậu quả:\*\* Không thể tái sử dụng dữ liệu báo cáo cho nhiều định dạng đầu ra khác nhau và vi phạm nguyên tắc tách biệt giữa tầng nghiệp vụ với tầng trình bày/xuất dữ liệu.

\- \*\*Cách sửa:\*\* Tách thành `ReportService` áp dụng Strategy Pattern thông qua interface `IReportStrategy` với các chiến lược cụ thể như `ConsoleReportStrategy` và `CsvReportStrategy`.



\## 5. Vi phạm OCP \& SRP — Gửi thông báo trực tiếp cho từng thành viên ngay trong hàm tạo sự kiện

\- \*\*Vị trí:\*\* `Chuong2\\Ch02.Refactor\\ClubManager.cs`, dòng 88–99 (đặc biệt vòng lặp `foreach` gửi email tại dòng 96–97 trong `CreateEvent`).

\- \*\*Nguyên lý:\*\* Single Responsibility Principle (SRP) và Open/Closed Principle (OCP) — Phương thức `CreateEvent` phải biết chi tiết danh sách thành viên và tự thực hiện hành động thông báo qua `Console.WriteLine`. Khi cần thêm hành động phụ khi tạo sự kiện (như ghi log sự kiện, gửi email, đẩy thông báo ứng dụng) lại phải sửa hàm `CreateEvent`.

\- \*\*Hậu quả:\*\* Gia tăng sự phụ thuộc chéo (tight coupling) giữa quản lý sự kiện với danh sách thành viên và các kênh thông báo, làm phức tạp hóa quy trình tạo sự kiện.

\- \*\*Cách sửa:\*\* Áp dụng Observer Pattern bằng cách tạo interface `IEventObserver` cùng các lớp theo dõi (`EmailNotifier`, `LogNotifier`), để `EventService` chỉ cần kích hoạt `OnEventCreated` cho các observer đã đăng ký.

