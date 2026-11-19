\# Phân tích vi phạm SOLID trong `ClubManager.cs`



\## 1. Vi phạm SRP — `CreateClub` làm quá nhiều việc

\- \*\*Vị trí:\*\* `ClubManager.cs`, dòng 35–63

\- \*\*Nguyên lý:\*\* Single Responsibility Principle (SRP) — `CreateClub` vừa validate, vừa lưu, vừa ghi log, vừa gửi thông báo.

\- \*\*Hậu quả:\*\* Khó bảo trì và khó viết unit test độc lập; khi đổi luật tên CLB hoặc cách gửi thông báo dễ ảnh hưởng các phần khác.

\- \*\*Cách sửa:\*\* Đưa luật tên vào `Club.Create`, lưu qua `IClubRepository`, ghi log qua `IAppLogger`, gửi thông báo qua `INotificationService`.

\- \*\*Ví dụ EduHub:\*\* Khi đổi quy tắc độ dài tên CLB hoặc cách gửi thông báo tạo CLB mới, nếu để chung trong `CreateClub` sẽ phải đụng vào cả phần lưu danh sách CLB và ghi log.



\## 2. Vi phạm OCP — Chuỗi `if/else` theo email khi gửi thông báo

\- \*\*Vị trí:\*\* `ClubManager.cs`, dòng 50–62

\- \*\*Nguyên lý:\*\* Open/Closed Principle (OCP) — Rẽ nhánh cứng theo `@gmail.com`, `@yahoo.com` khiến thêm kênh mới phải sửa method đang chạy.

\- \*\*Hậu quả:\*\* Dễ phát sinh lỗi ở các nhánh cũ mỗi khi bổ sung kênh thông báo mới.

\- \*\*Cách sửa:\*\* Mỗi kênh một class cài đặt `INotificationService` kết hợp Factory (`INotificationServiceFactory`).

\- \*\*Ví dụ EduHub:\*\* Nếu thêm kênh Slack hoặc SMS cho chủ nhiệm CLB khi tạo CLB thì phải sửa trực tiếp đoạn `if/else` trong `CreateClub`.



\## 3. Vi phạm DIP — Gọi thẳng `File.AppendAllText` và `Console.WriteLine`

\- \*\*Vị trí:\*\* `ClubManager.cs`, dòng 32, 46, 52, 56, 60, 78, 82–83, 94, 97, 108, 111

\- \*\*Nguyên lý:\*\* Dependency Inversion Principle (DIP) — Phụ thuộc trực tiếp vào `File.AppendAllText` và `Console.WriteLine` thay vì qua abstraction.

\- \*\*Hậu quả:\*\* Không test được nếu không tạo file thật (`club-log.txt`) hoặc xuất ra console, khó đổi nơi lưu log.

\- \*\*Cách sửa:\*\* Tách qua `IAppLogger` (và `INotificationService` / `IClubRepository`) nhận qua constructor.

\- \*\*Ví dụ EduHub:\*\* Khi chạy unit test cho chức năng thêm thành viên hoặc tạo sự kiện của EduHub, nếu không qua `IAppLogger` thì sẽ ghi thật ra file `club-log.txt` và in thẳng ra console.



\## 4. Vi phạm SRP \& OCP — `CreateEvent` tự vòng lặp gửi mail

\- \*\*Vị trí:\*\* `ClubManager.cs`, dòng 88–99

\- \*\*Nguyên lý:\*\* Single Responsibility Principle (SRP) \& Open/Closed Principle (OCP) — Tạo sự kiện nhưng tự lặp qua thành viên để gửi mail ngay trong hàm.

\- \*\*Hậu quả:\*\* Gắn chặt nghiệp vụ tạo sự kiện với cách gửi thông báo; thêm hành động mới khi có sự kiện phải sửa `CreateEvent`.

\- \*\*Cách sửa:\*\* Áp dụng Observer Pattern (`IEventObserver`, `EmailNotifier`, `LogNotifier`) để `EventService` gọi `OnEventCreated`.

\- \*\*Ví dụ EduHub:\*\* Khi tạo một buổi sinh hoạt CLB mới trong EduHub mà muốn vừa gửi email vừa ghi log thông báo sự kiện, nếu để vòng lặp gửi mail trong `CreateEvent` sẽ phải sửa lại hàm tạo sự kiện.



\## 5. Vi phạm SRP \& OCP — `PrintClubReport` vừa tính vừa in/ghi file

\- \*\*Vị trí:\*\* `ClubManager.cs`, dòng 103–113

\- \*\*Nguyên lý:\*\* Single Responsibility Principle (SRP) \& Open/Closed Principle (OCP) — Vừa tính số lượng thành viên, vừa in ra console và ghi log file.

\- \*\*Hậu quả:\*\* Muốn đổi định dạng báo cáo phải sửa trực tiếp logic báo cáo và I/O.

\- \*\*Cách sửa:\*\* Áp dụng Strategy Pattern (`IReportStrategy` với `ConsoleReportStrategy` và `CsvReportStrategy`) trong `ReportService`.

\- \*\*Ví dụ EduHub:\*\* Nếu ban chủ nhiệm CLB trên EduHub muốn xuất báo cáo danh sách thành viên sang file CSV thay vì chỉ in ra console thì phải sửa thẳng vào `PrintClubReport`.

