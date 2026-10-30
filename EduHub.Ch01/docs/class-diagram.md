# Sơ đồ lớp sơ bộ - EduHub

```mermaid
classDiagram
    class Member {
        +Guid Id PK
        +string Name
        +string Email
        +string StudentCode
        +DateTime JoinedAt
        +Create(name, email, studentCode)$ Member
        +IsActive() bool
    }
    class Club {
        +Guid Id PK
        +string Name
        +string Description
        +DateTime CreatedAt
        +List~Member~ Members
        +Create(name, description)$ Club
        +AddMember(member) void
        +MemberCount() int
    }
    class Event {
        +Guid Id PK
        +Guid ClubId FK
        +string Title
        +DateTime Date
        +int Capacity
        +bool IsOnline
        +Create(clubId, title, date, capacity, isOnline)$ Event
        +HasCapacity(registeredCount) bool
    }
    class Registration {
        +Guid Id PK
        +Guid MemberId FK
        +Guid EventId FK
        +DateTime RegisteredAt
        +RegistrationStatus Status
        +Create(memberId, eventId)$ Registration
        +Confirm() void
        +Cancel() void
    }
    class CheckIn {
        +Guid Id PK
        +Guid RegistrationId FK
        +DateTime CheckedInAt
        +CheckInMethod Method
        +Create(registrationId, method)$ CheckIn
        +Summary() string
    }
    class RegistrationStatus {
        <<enumeration>>
        Pending
        Confirmed
        Cancelled
    }
    class CheckInMethod {
        <<enumeration>>
        Manual
        QrCode
    }

    Member "*" -- "*" Club : joins
    Club "1" --> "*" Event : organizes
    Member "1" --> "*" Registration : makes
    Event "1" --> "*" Registration : has
    Registration "1" --> "0..1" CheckIn : has
    Registration ..> RegistrationStatus
    CheckIn ..> CheckInMethod
```