# 📚 API Documentation - Student Management System

## 📋 Mục lục
- [Thông tin chung](#thông-tin-chung)
- [Authentication](#1-authentication)
- [Users](#2-users)
- [Students](#3-students)
- [Teachers](#4-teachers)
- [Courses](#5-courses)
- [Course Sections](#6-course-sections)
- [Enrollments](#7-enrollments)
- [Majors](#8-majors)
- [Upload](#9-upload)

---

## Thông tin chung

### Base URL
```
http://localhost:{port}/api
```

### API Versioning
Các endpoint có version được định dạng:
```
/api/v{version}/{resource}
```
Ví dụ: `/api/v1/students`

### Response Format
Tất cả response đều trả về theo format:

```json
{
  "code": 200,
  "success": true,
  "message": "string",
  "data": {},
  "metaData": {
    "page": 1,
    "pageSize": 20,
    "total": 100,
    "totalPage": 5
  }
}
```

### Pagination Parameters (Query String)
| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `keyword` | string | null | Tìm kiếm theo từ khóa |
| `page` | int | 1 | Số trang hiện tại |
| `pageSize` | int | 20 | Số lượng item trên mỗi trang |
| `sortColumn` | string | "Id" | Cột để sắp xếp |
| `sortDirection` | string | "asc" | Hướng sắp xếp ("asc" hoặc "desc") |

### Enums

#### Gender
| Value | Name |
|-------|------|
| 0 | Female |
| 1 | Male |

#### EnrollmentStatus
| Value | Name |
|-------|------|
| 1 | Inprogress |
| 2 | Submitted |
| 3 | Done |

#### ScheduleSlot
| Value | Name |
|-------|------|
| 1 | SlotFirst |
| 2 | SlotSecond |
| 3 | SlotThird |
| 4 | SlotFourth |

---

## 1. Authentication

### 🔐 POST `/api/auth/login`
Đăng nhập vào hệ thống.

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "string"
}
```

| Field | Type | Required | Validation |
|-------|------|----------|------------|
| email | string | Yes | Email format |
| password | string | Yes | Required |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "message": "Login successfully",
  "data": {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "refreshToken": "",
    "user": {
      "id": 1,
      "email": "user@example.com"
    }
  }
}
```

> ⚠️ **Lưu ý:** `refreshToken` được lưu trong HttpOnly Cookie, không trả về trong response body.

---

## 2. Users

### 📋 GET `/api/v1/users`
Lấy danh sách tất cả users (có phân trang).

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| roleId | int | Lọc theo Role ID |
| keyword | string | Tìm kiếm |
| page | int | Số trang |
| pageSize | int | Số lượng/trang |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "data": [
    {
      "id": 1,
      "email": "user@example.com"
    }
  ],
  "metaData": {
    "page": 1,
    "pageSize": 20,
    "total": 100,
    "totalPage": 5
  }
}
```

---

### 👤 GET `/api/v1/users/{id}`
Lấy thông tin chi tiết một user.

**Path Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | User ID |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "message": "Get user successfully",
  "data": {
    "id": 1,
    "email": "user@example.com"
  }
}
```

---

### 👤 GET `/me`
Lấy thông tin user đang đăng nhập.

**Headers:**
```
Authorization: Bearer {accessToken}
```

**Response:**
```json
{
  "code": 200,
  "success": true,
  "message": "Get user successfully",
  "data": {
    // User information based on role
  }
}
```

---

### ➕ POST `/api/v1/users`
Tạo user mới.

**Request Body:**
```json
{
  "email": "newuser@example.com",
  "password": "password123",
  "roleId": 2
}
```

| Field | Type | Required | Validation |
|-------|------|----------|------------|
| email | string | Yes | Required |
| password | string | Yes | Min 6 characters |
| roleId | int | No | Default: 2 |

**Response:** `201 Created`
```json
{
  "code": 200,
  "success": true,
  "message": "Create user successfully",
  "data": {
    "id": 1,
    "email": "newuser@example.com"
  }
}
```

---

### ✏️ PUT `/api/v1/users`
Cập nhật thông tin user.

**Request Body:**
```json
{
  "id": 1,
  "email": "updated@example.com",
  "password": "newpassword",
  "roleId": 2
}
```

**Response:** `200 OK`
```json
{
  "code": 200,
  "success": true,
  "message": "Update user successfully",
  "data": {
    "id": 1,
    "email": "updated@example.com"
  }
}
```

---

### 🗑️ DELETE `/api/v1/users/{id}`
Xóa user.

**Path Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | User ID |

**Response:** `204 No Content`

---

## 3. Students

### 📋 GET `/api/v1/students`
Lấy danh sách tất cả students (có phân trang).

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| corhortId | int | Lọc theo Cohort ID |
| keyword | string | Tìm kiếm |
| page | int | Số trang |
| pageSize | int | Số lượng/trang |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "data": [
    {
      "id": 1,
      "code": "SV001",
      "name": "Nguyễn Văn A",
      "gender": 1,
      "brith": "2000-01-15",
      "address": "Hà Nội",
      "phone": "0123456789",
      "avatar": "https://example.com/avatar.jpg",
      "cohort": {
        "id": 1,
        "name": "K20"
      }
    }
  ],
  "metaData": {
    "page": 1,
    "pageSize": 20,
    "total": 100,
    "totalPage": 5
  }
}
```

---

### 👤 GET `/api/v1/students/{id}`
Lấy thông tin chi tiết một student.

**Path Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | Student ID |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "message": "Get Student successfully",
  "data": {
    "id": 1,
    "code": "SV001",
    "name": "Nguyễn Văn A",
    "gender": 1,
    "brith": "2000-01-15",
    "address": "Hà Nội",
    "phone": "0123456789",
    "avatar": "https://example.com/avatar.jpg",
    "cohort": {
      "id": 1,
      "name": "K20"
    }
  }
}
```

---

### ➕ POST `/api/v1/students`
Tạo student mới.

**Request Body:**
```json
{
  "name": "Nguyễn Văn A",
  "gender": 1,
  "brith": "2000-01-15",
  "address": "Hà Nội",
  "phone": "0123456789",
  "cohortId": 1
}
```

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| name | string | Yes | Tên sinh viên |
| gender | int | Yes | 0: Female, 1: Male |
| brith | string | Yes | Ngày sinh (YYYY-MM-DD) |
| address | string | Yes | Địa chỉ |
| phone | string | Yes | Số điện thoại |
| cohortId | int | Yes | ID khóa học |

**Response:** `201 Created`
```json
{
  "code": 200,
  "success": true,
  "message": "Create Student successfully",
  "data": {
    "id": 1,
    "code": "SV001",
    "name": "Nguyễn Văn A",
    ...
  }
}
```

---

### ✏️ PUT `/api/v1/students`
Cập nhật thông tin student.

**Request Body:**
```json
{
  "id": 1,
  "name": "Nguyễn Văn B",
  "gender": 1,
  "brith": "2000-01-15",
  "address": "Hà Nội",
  "phone": "0123456789",
  "cohortId": 1
}
```

**Response:** `200 OK`

---

### 🗑️ DELETE `/api/v1/students/{id}`
Xóa student.

**Path Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | Student ID |

**Response:** `204 No Content`

---

### 🖼️ PUT `/api/v1/students/avatar`
Upload avatar cho student.

**Content-Type:** `multipart/form-data`

**Request Body:**
| Field | Type | Description |
|-------|------|-------------|
| formFile | file | File ảnh avatar |

**Response:** `204 No Content`

---

## 4. Teachers

### 📋 GET `/api/v1/teachers`
Lấy danh sách tất cả teachers (có phân trang).

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| facultyId | int | Lọc theo Faculty ID |
| keyword | string | Tìm kiếm |
| page | int | Số trang |
| pageSize | int | Số lượng/trang |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "data": [
    {
      "id": 1,
      "name": "Nguyễn Văn A",
      "gender": 1,
      "brith": "1980-05-20",
      "address": "Hà Nội",
      "phone": "0123456789",
      "avatar": "https://example.com/avatar.jpg",
      "user": {
        "id": 1,
        "email": "teacher@example.com"
      },
      "faculty": {
        "id": 1,
        "name": "Khoa CNTT"
      }
    }
  ],
  "metaData": {
    "page": 1,
    "pageSize": 20,
    "total": 100,
    "totalPage": 5
  }
}
```

---

### 👤 GET `/api/v1/teachers/{id}`
Lấy thông tin chi tiết một teacher.

**Path Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | Teacher ID |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "message": "Get Teacher successfully",
  "data": {
    "id": 1,
    "name": "Nguyễn Văn A",
    "gender": 1,
    "brith": "1980-05-20",
    "address": "Hà Nội",
    "phone": "0123456789",
    "avatar": "https://example.com/avatar.jpg",
    "user": {
      "id": 1,
      "email": "teacher@example.com"
    },
    "faculty": {
      "id": 1,
      "name": "Khoa CNTT"
    }
  }
}
```

---

### ➕ POST `/api/v1/teachers`
Tạo teacher mới.

**Request Body:**
```json
{
  "name": "Nguyễn Văn A",
  "gender": 1,
  "brith": "1980-05-20",
  "address": "Hà Nội",
  "phone": "0123456789",
  "facultyId": 1,
  "emailUser": "teacher@example.com"
}
```

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| name | string | Yes | Tên giáo viên |
| gender | int | Yes | 0: Female, 1: Male |
| brith | string | Yes | Ngày sinh (YYYY-MM-DD) |
| address | string | Yes | Địa chỉ |
| phone | string | Yes | Số điện thoại |
| facultyId | int | Yes | ID khoa |
| emailUser | string | Yes | Email tài khoản |

**Response:** `201 Created`

---

### ✏️ PUT `/api/v1/teachers`
Cập nhật thông tin teacher.

**Request Body:**
```json
{
  "id": 1,
  "name": "Nguyễn Văn B",
  "gender": 1,
  "brith": "1980-05-20",
  "address": "Hà Nội",
  "phone": "0123456789",
  "facultyId": 1,
  "emailUser": "teacher@example.com"
}
```

**Response:** `200 OK`

---

### 🗑️ DELETE `/api/v1/teachers/{id}`
Xóa teacher.

**Path Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | Teacher ID |

**Response:** `204 No Content`

---

### 🖼️ PUT `/api/v1/teachers/avatar`
Upload avatar cho teacher.

**Content-Type:** `multipart/form-data`

**Request Body:**
| Field | Type | Description |
|-------|------|-------------|
| formFile | file | File ảnh avatar |

**Response:** `204 No Content`

---

## 5. Courses

### 📋 GET `/api/v1/courses`
Lấy danh sách tất cả courses (có phân trang). **Public endpoint**.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| facultyId | int | Lọc theo Faculty ID |
| keyword | string | Tìm kiếm |
| page | int | Số trang |
| pageSize | int | Số lượng/trang |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "data": [
    {
      "id": 1,
      "name": "Lập trình Web",
      "price": 1500000,
      "credit": 3
    }
  ],
  "metaData": {
    "page": 1,
    "pageSize": 20,
    "total": 100,
    "totalPage": 5
  }
}
```

---

### 📖 GET `/api/v1/courses/{id}`
Lấy thông tin chi tiết một course. **Public endpoint**.

**Path Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | Course ID |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "message": "Get Course successfully",
  "data": {
    "id": 1,
    "name": "Lập trình Web",
    "price": 1500000,
    "credit": 3
  }
}
```

---

### ➕ POST `/api/v1/courses`
Tạo course mới. **Yêu cầu quyền ADMIN**.

**Headers:**
```
Authorization: Bearer {accessToken}
```

**Request Body:**
```json
{
  "name": "Lập trình Web",
  "credit": 3,
  "facultyId": 1
}
```

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| name | string | Yes | Tên môn học |
| credit | int | Yes | Số tín chỉ |
| facultyId | int | Yes | ID khoa |

**Response:** `201 Created`
```json
{
  "code": 200,
  "success": true,
  "message": "Create Course successfully",
  "data": {
    "id": 1,
    "name": "Lập trình Web",
    "price": 1500000,
    "credit": 3
  }
}
```

---

### ✏️ PUT `/api/v1/courses`
Cập nhật thông tin course. **Yêu cầu quyền ADMIN**.

**Headers:**
```
Authorization: Bearer {accessToken}
```

**Request Body:**
```json
{
  "id": 1,
  "name": "Lập trình Web nâng cao",
  "credit": 4,
  "facultyId": 1
}
```

**Response:** `200 OK`

---

### 🗑️ DELETE `/api/v1/courses/{id}`
Xóa course. **Yêu cầu quyền ADMIN**.

**Headers:**
```
Authorization: Bearer {accessToken}
```

**Path Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | Course ID |

**Response:** `204 No Content`

---

## 6. Course Sections

### 📋 GET `/api/v1/courseSections`
Lấy danh sách tất cả course sections (có phân trang).

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| courseId | int | Lọc theo Course ID |
| teacherId | int | Lọc theo Teacher ID |
| keyword | string | Tìm kiếm |
| page | int | Số trang |
| pageSize | int | Số lượng/trang |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "data": [
    {
      "id": 1,
      "code": "CS001",
      "teacher": {
        "id": 1,
        "name": "Nguyễn Văn A"
      },
      "course": {
        "id": 1,
        "name": "Lập trình Web",
        "credit": 3
      },
      "semester": {
        "id": 1,
        "name": "HK1 2024-2025"
      },
      "dayOfWeek": 2,
      "slot": 1,
      "startDate": "2024-09-01",
      "endDate": "2024-12-31"
    }
  ],
  "metaData": {
    "page": 1,
    "pageSize": 20,
    "total": 100,
    "totalPage": 5
  }
}
```

---

### 📖 GET `/api/v1/courseSections/{id}`
Lấy thông tin chi tiết một course section.

**Path Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | Course Section ID |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "data": {
    "id": 1,
    "code": "CS001",
    "teacher": { ... },
    "course": { ... },
    "semester": { ... },
    "enrollments": [ ... ],
    "dayOfWeek": 2,
    "slot": 1,
    "startDate": "2024-09-01",
    "endDate": "2024-12-31"
  }
}
```

---

### 👥 GET `/api/v1/courseSections/participants/{id}`
Lấy danh sách sinh viên tham gia course section.

**Path Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | Course Section ID |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "data": [
    // List of participants
  ]
}
```

---

### ➕ POST `/api/v1/courseSections`
Mở lớp học phần mới.

**Request Body:**
```json
{
  "teacherId": 1,
  "courseId": 1,
  "startDate": "2024-09-01",
  "semesterId": 1,
  "slot": 1
}
```

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| teacherId | int | Yes | ID giáo viên |
| courseId | int | Yes | ID môn học |
| startDate | string | Yes | Ngày bắt đầu (YYYY-MM-DD) |
| semesterId | int | Yes | ID học kỳ |
| slot | int | Yes | Slot học (1-4) |

**Response:** `201 Created`
```json
{
  "code": 200,
  "success": true,
  "message": "Add Course Section  successfully",
  "data": {
    "id": 1,
    "code": "CS001",
    ...
  }
}
```

---

### ✏️ PUT `/api/v1/courseSections`
Cập nhật thông tin course section.

**Request Body:**
```json
{
  "id": 1,
  "teacherId": 1,
  "courseId": 1,
  "startDate": "2024-09-01",
  "semesterId": 1,
  "slot": 2
}
```

**Response:** `201 Created`

---

## 7. Enrollments

### 📅 GET `/api/v1/enrollments/schedules`
Lấy lịch học của sinh viên theo học kỳ.

**Query Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| semesterId | int | Yes | ID học kỳ |
| studentId | int | Yes | ID sinh viên |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "message": "Get Enroll successfully",
  "data": [
    {
      "teacherName": "Nguyễn Văn A",
      "studentName": "Trần Văn B",
      "courseCode": "WEB101",
      "courseName": "Lập trình Web",
      "credit": 3,
      "dayOfWeek": 2,
      "slot": 1,
      "startDate": "2024-09-01",
      "endDate": "2024-12-31",
      "attendance": null,
      "midterm": null,
      "finalExam": null,
      "totalScore": null,
      "status": 1,
      "isPass": null
    }
  ]
}
```

---

### ➕ POST `/api/v1/enrollments`
Đăng ký học phần cho sinh viên.

**Request Body:**
```json
{
  "studentId": 1,
  "courseSectionId": 1
}
```

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| studentId | int | Yes | ID sinh viên |
| courseSectionId | int | Yes | ID lớp học phần |

**Response:** `201 Created`
```json
{
  "code": 200,
  "success": true,
  "message": "Enroll  successfully",
  "data": {
    "teacherName": "Nguyễn Văn A",
    "studentName": "Trần Văn B",
    ...
  }
}
```

---

### 🗑️ DELETE `/api/v1/enrollments/{id}`
Hủy đăng ký học phần.

**Path Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | Enrollment ID |

**Response:** `204 No Content`

---

### 📝 PUT `/api/v1/enrollments/scores`
Cập nhật điểm cho sinh viên.

**Request Body:**
```json
{
  "id": 1,
  "attendance": 10,
  "midTerm": 7.5,
  "finalExam": 8.0
}
```

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| id | int | Yes | Enrollment ID |
| attendance | double | Yes | Điểm chuyên cần |
| midTerm | double | Yes | Điểm giữa kỳ |
| finalExam | double | Yes | Điểm cuối kỳ |

**Response:** `204 No Content`

---

### 🔄 PUT `/api/v1/enrollments/status`
Cập nhật trạng thái enrollment.

**Request Body:**
```json
{
  "id": 1,
  "status": 2
}
```

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| id | int | Yes | Enrollment ID |
| status | int | Yes | 1: Inprogress, 2: Submitted, 3: Done |

**Response:** `204 No Content`

---

## 8. Majors

### 📋 GET `/api/v1/majors`
Lấy danh sách tất cả majors (ngành học).

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| keyword | string | Tìm kiếm |
| page | int | Số trang |
| pageSize | int | Số lượng/trang |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "data": [
    {
      "id": 1,
      "code": "CNTT",
      "name": "Công nghệ thông tin",
      "courses": [
        {
          "id": 1,
          "name": "Lập trình Web",
          "price": 1500000,
          "credit": 3
        }
      ]
    }
  ],
  "metaData": {
    "page": 1,
    "pageSize": 20,
    "total": 100,
    "totalPage": 5
  }
}
```

---

### 📖 GET `/api/v1/majors/{id}`
Lấy thông tin chi tiết một major.

**Path Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | Major ID |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "message": "Get Major successfully",
  "data": {
    "id": 1,
    "code": "CNTT",
    "name": "Công nghệ thông tin",
    "courses": [
      {
        "id": 1,
        "name": "Lập trình Web",
        "price": 1500000,
        "credit": 3
      }
    ]
  }
}
```

---

## 9. Upload

### 📤 POST `/api/upload`
Upload file (ảnh avatar).

**Content-Type:** `multipart/form-data`

**Request Body:**
| Field | Type | Description |
|-------|------|-------------|
| file | file | File cần upload |

**Response:**
```json
{
  "code": 200,
  "success": true,
  "message": "Upload file successfully",
  "data": "https://storage.example.com/avatar/filename.jpg"
}
```

---

## 🔐 Authentication & Authorization

### Headers
Tất cả các endpoint yêu cầu xác thực cần gửi kèm header:
```
Authorization: Bearer {accessToken}
```

### Roles
| Role | Description |
|------|-------------|
| ADMIN | Quản trị viên - có quyền CRUD tất cả |
| TEACHER | Giáo viên |
| STUDENT | Sinh viên |

### Public Endpoints (không cần xác thực)
- `POST /api/auth/login`
- `GET /api/v1/courses`
- `GET /api/v1/courses/{id}`
- `GET /api/v1/students` (đang AllowAnonymous)
- `GET /api/v1/teachers` (đang AllowAnonymous)
- `GET /api/v1/majors`

---

## ❌ Error Response

Khi có lỗi, API sẽ trả về:

```json
{
  "code": 400,
  "success": false,
  "message": "Error message here",
  "data": null
}
```

### HTTP Status Codes
| Code | Description |
|------|-------------|
| 200 | OK - Thành công |
| 201 | Created - Tạo thành công |
| 204 | No Content - Xóa thành công |
| 400 | Bad Request - Request không hợp lệ |
| 401 | Unauthorized - Chưa xác thực |
| 403 | Forbidden - Không có quyền |
| 404 | Not Found - Không tìm thấy |
| 500 | Internal Server Error - Lỗi server |

---

## 📌 Notes for Frontend

1. **Date Format**: Sử dụng format `YYYY-MM-DD` cho tất cả các field ngày tháng
2. **Pagination**: Luôn kiểm tra `metaData` để xử lý phân trang
3. **Token Refresh**: `refreshToken` được lưu trong HttpOnly Cookie, frontend không cần handle
4. **File Upload**: Sử dụng `multipart/form-data` cho các endpoint upload
5. **Enum Values**: Gửi giá trị số (int) thay vì tên enum

---

*Tài liệu được cập nhật: 11/12/2025*
