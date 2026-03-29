# 🏥 Doctors & Patients Management System

## 📋 Overview
This project is a **Doctors & Patients Management System** built using **ASP.NET Core Web API** and **SQL Server**.  
It allows users to manage doctors and patients' data efficiently, including adding, updating, deleting, and viewing records.  
The project also supports **image upload** using **Cloudinary**, **JWT Authentication**, and **patient-doctor relationship management**.

---

## ⚙️ Tech Stack
- **Backend:** ASP.NET Core Web API (.NET 9)  
- **Database:** Microsoft SQL Server  
- **ORM:** Entity Framework Core  
- **Image Hosting:** Cloudinary  
- **Language:** C#  
- **IDE:** Visual Studio 2022  

---

## 📁 Project Structure
doctors_patients/
│
├── Controllers/
│ ├── AuthController.cs
│ ├── DoctorsController.cs
│ ├── PatientsController.cs
│ ├── ChatController.cs
│ ├── MeasurementController.cs
│
├── DTOs/
│ ├── AddDoctorDTO.cs
│ ├── UpdateDoctorDTO.cs
│ ├── GetDoctorDTO.cs
│ ├── AddPatientDTO.cs
│ ├── UpdatePatientDTO.cs
│ ├── GetPatientDTO.cs
│
├── Models/
│ ├── Doctor.cs
│ ├── Patient.cs
│ ├── DoctorPatientRequest.cs
│ ├── DoctorPatient.cs
│ ├── Message.cs
│ ├── Measurement.cs
│
├── Services/
│ ├── AuthService.cs
│ ├── DoctorService.cs
│ ├── PatientService.cs
│ ├── ChatService.cs
│ ├── MeasurementService.cs
│ ├── CloudinaryService.cs
│
├── Data/
│ ├── AppDbContext.cs
│
├── appsettings.json
├── Program.cs
└── README.md


---

---

## 🚀 Features
- Add, edit, and delete doctors & patients  
- Upload doctor images using Cloudinary  
- Retrieve all doctors or a specific doctor by email or ID  
- Patient-Doctor relationship requests (accept/reject)  
- Soft-delete relationships  
- JWT Authentication with role-based access (Doctor / Patient)  
- Chat system (with optional file uploads)  
- Track patient measurements (Sugar Level, Blood Pressure)  
- Validation using Data Annotations (`[Required]`, `[EmailAddress]`, etc.)  
- Asynchronous CRUD operations  

---

## 🧰 Setup Instructions

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/youssefRaslan/doctors_patients.git
cd doctors_patients
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=DoctorsDB;Trusted_Connection=True;TrustServerCertificate=True;"
},
"CloudinarySettings": {
  "CloudName": "YOUR_CLOUD_NAME",
  "ApiKey": "YOUR_API_KEY",
  "ApiSecret": "YOUR_API_SECRET"
}
🔐 Authentication
JWT Authentication for all endpoints
Role-based authorization (Doctor / Patient)
Passwords are securely hashed using BCrypt
📊 API Endpoints
Authentication
Method	Endpoint	Description
POST	/api/auth/register-doctor	Register a new doctor
POST	/api/auth/register-patient	Register a new patient
POST	/api/auth/login	Login and get JWT token
Doctor
Method	Endpoint	Description
GET	/api/doctor/getalldoctors	List all doctors
POST	/api/doctor/adddoctor	Add doctor
PATCH	/api/doctor/updatedoctor/{id}	Update doctor
DELETE	/api/doctor/removedoctor/{id}	Delete doctor
POST	/api/doctor/send-request	Send patient request
GET	/api/doctor/patients	List accepted patients
GET	/api/doctor/sent-requests	List sent requests
DELETE	/api/doctor/remove-patient/{patientId}	Soft delete patient
Patient
Method	Endpoint	Description
GET	/api/patient/{id}	Get patient info
PUT	/api/patient/update	Update patient info
DELETE	/api/patient/delete	Delete patient
GET	/api/patient/requests	List incoming doctor requests
POST	/api/patient/accept/{requestId}	Accept doctor request
POST	/api/patient/reject/{requestId}	Reject doctor request
DELETE	/api/patient/remove-doctor/{doctorId}	Remove doctor
Chat
Method	Endpoint	Description
POST	/api/chat/send-message	Send a message (optional file upload)
GET	/api/chat/{doctorId}/{patientId}	Retrieve chat history
Measurements
Method	Endpoint	Description
POST	/api/measurement/add	Add sugar & blood pressure
GET	/api/measurement/{patientId}	Get patient's measurements
💡 Notes
Use DTOs for all API requests/responses
Keep Controllers thin and implement business logic in Services
Validate all emails and phone numbers
Use Cloudinary for storing images & PDFs
Soft delete relationships instead of hard delete
📌 Contribution
Fork the repository
Create a new branch feature/your-feature-name
Commit your changes
Push to your branch
Open a Pull Request
📜 License
