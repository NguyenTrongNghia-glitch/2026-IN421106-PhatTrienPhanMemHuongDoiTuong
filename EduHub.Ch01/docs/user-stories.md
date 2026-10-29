# User Stories - EduHub

## Actor: Sinh viên

### US-01: Đăng ký tham gia CLB
**As a** sinh viên, **I want** đăng ký tham gia một câu lạc bộ, **so that** tôi muốn trở thành thành viên và nhận thông tin sớm nhất về hoạt động của CLB.

**Acceptance Criteria:**
- Given tôi đã đăng nhập, When tôi chọn một CLB và bấm đăng ký, Then hệ thống ghi nhận yêu cầu ở trạng thái chờ duyệt.
- Given tôi đã là thành viên của CLB, When tôi đăng ký lại, Then hệ thống báo lỗi trùng đăng ký.

### US-02: Đăng ký tham dự sự kiện
**As a** sinh viên, **I want** đăng ký tham dự một sự kiện của CLB, **so that** tôi giữ được chỗ và được ban tổ chức xác nhận.

**Acceptance Criteria:**
- Given sự kiện còn chỗ, When tôi đăng ký, Then hệ thống tạo phiếu đăng ký ở trạng thái Pending.
- Given sự kiện đã đủ sức chứa, When tôi đăng ký, Then hệ thống từ chối và thông báo hết chỗ.
- Given tôi đã đăng ký sự kiện này, When tôi đăng ký lần nữa, Then hệ thống báo lỗi trùng đăng ký.

### US-03: Check-in tại sự kiện
**As a** sinh viên, **I want** check-in khi đến sự kiện, **so that** việc tham dự của tôi được ghi nhận chính thức.

**Acceptance Criteria:**
- Given phiếu đăng ký của tôi ở trạng thái Confirmed, When tôi check-in, Then hệ thống ghi nhận thời điểm điểm danh.
- Given phiếu đăng ký đã bị huỷ hoặc chưa được xác nhận, When tôi check-in, Then hệ thống từ chối check-in.

## Actor: Ban CLB

### US-04: Tạo sự kiện
**As a** thành viên ban CLB, **I want** tạo một sự kiện mới cho CLB, **so that** sinh viên có thể xem và đăng ký tham dự.

**Acceptance Criteria:**
- Given tôi nhập đủ tiêu đề, ngày tổ chức và sức chứa lớn hơn 0, When tôi lưu, Then sự kiện được tạo và gắn với CLB của tôi.
- Given tôi để trống tiêu đề hoặc nhập sức chứa nhỏ hơn hoặc bằng 0, When tôi lưu, Then hệ thống báo lỗi và không tạo sự kiện.

### US-05: Xem danh sách thành viên và xuất báo cáo tham dự
**As a** thành viên ban CLB, **I want** xem danh sách thành viên và báo cáo tham dự của sự kiện, **so that** tôi nắm được số lượng đăng ký và số người đã check-in.

**Acceptance Criteria:**
- Given CLB có thành viên, When tôi mở danh sách, Then hệ thống hiển thị tên, mã sinh viên và ngày tham gia của từng người.
- Given một sự kiện đã diễn ra, When tôi xuất báo cáo, Then báo cáo hiển thị số đăng ký, số check-in và danh sách tên người tham dự.

## Actor: Admin

### US-06: Phê duyệt thành viên và quản lý CLB
**As an** admin, **I want** phê duyệt yêu cầu tham gia và quản lý các CLB, **so that** chỉ những sinh viên hợp lệ mới trở thành thành viên chính thức.

**Acceptance Criteria:**
- Given có yêu cầu tham gia đang chờ duyệt, When tôi phê duyệt, Then sinh viên trở thành thành viên của CLB.
- Given có yêu cầu không hợp lệ, When tôi từ chối, Then yêu cầu bị huỷ và sinh viên được thông báo.
- Given tôi tạo CLB mới, When tên CLB bị trống, Then hệ thống báo lỗi và không lưu.