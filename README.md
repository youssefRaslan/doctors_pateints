# 🏥 Doctors & Patients Management System

## 📋 Overview
This project is a **Doctors & Patients Management System** built using **ASP.NET Core Web API** and **SQL Server**.  
It allows users to manage doctors and patients' data efficiently, including adding, updating, deleting, and viewing records.  
The project also supports **image upload** using **Cloudinary**.

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
│ ├── DoctorsController.cs
│ ├── PatientsController.cs
│
├── DTOs/
│ ├── AddDoctorDTO.cs
│ ├── UpdateDoctorDTO.cs
│ ├── GetDoctorDTO.cs
│
├── Models/
│ ├── Doctor.cs
│ ├── Patient.cs
│
├── Services/
│ ├── CloudinaryService.cs
│
├── Data/
│ ├── AppDbContext.cs
│
├── appsettings.json
├── Program.cs
└── README.md

---

## 🚀 Features
- Add, edit, and delete doctors & patients  
- Upload doctor images using Cloudinary  
- Retrieve all doctors or a specific doctor by email or ID  
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
}

"CloudinarySettings": {
  "CloudName": "YOUR_CLOUD_NAME",
  "ApiKey": "YOUR_API_KEY",
  "ApiSecret": "YOUR_API_SECRET"
}
CLOUDINARY_URL=cloudinary://<API_KEY>:<API_SECRET>@<CLOUD_NAME>
dotnet run
