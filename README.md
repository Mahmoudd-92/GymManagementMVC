# 🏋️ Gym Management System

## 🌟 Project Overview

A comprehensive web-based application built with **ASP.NET Core MVC** to efficiently manage all aspects of gym operations, including member subscriptions, trainer scheduling, session booking, and financial reporting. The system promotes efficiency, data integrity, and a clear separation of concerns using industry-standard patterns.

### Core Goals

* **Centralized Management:** Provide a single platform for managing all members, trainers, and membership plans.

* **Scheduling Efficiency:** Effectively manage trainer availability and organize training session schedules.

* **Reporting:** Offer a dedicated **Dashboard** for viewing key analytics and generated reports.

## ✨ Key Features

This system provides a full suite of features necessary for modern gym management, with all business logic strictly enforced in the Services layer.

| Module | Key Functionality | Notes & Constraints |
| ----- | ----- | ----- |
| **Member Management** | Full CRUD (Create, Read, Update, Delete) for members. | Includes detailed tracking of **Health Records** and fast search by **Phone ID**. |
| **Trainer Management** | Full CRUD for trainers, managing specialties and schedules. | Enforcement of unique email/phone; cannot delete trainers with future sessions. |
| **Session Management** | Full CRUD for scheduling, organizing, and assigning sessions to trainers. | Capacity is limited (1-25 participants) and end date must be after start date. |
| **Session Booking** | Allows members to book sessions and track attendance. | Requires an **Active Membership** and checks for available session **Capacity**. |
| **Plan Management** | Management of membership plans (Update, View, Activate/Deactivate). | Deactivation is handled via a **Soft Delete (IsActive)** flag. |
| **Membership Assignment** | Assign training plans to members. | Enforces a maximum of one **Active Membership** per member; end date is auto-calculated. |
| **Authentication** | Secure user login and logout. | Integrated **ASP.NET Core Identity** for role-based access control. |

## 🏗️ Architecture and Design

The system adheres to a robust **Three-Layer Architecture** to ensure the codebase is scalable, maintainable, and loosely coupled.

### 1. Technology Stack

| Category | Technology / Library | Role |
| ----- | ----- | ----- |
| **Backend Framework** | **ASP.NET Core MVC** | Handles the web application structure and request pipeline. |
| **Database** | **SQL Server** | The primary relational data store. |
| **ORM** | **Entity Framework Core** | Used for database interaction via Code First approach. |
| **Design Pattern** | **Repository Pattern + Unit of Work** | Provides an abstraction layer for data access and ensures transaction integrity. |
| **Mapping** | **AutoMapper** | Used to simplify data transfer and mapping between **ViewModels** and **Entities**. |
| **Frontend** | **Bootstrap + Razor Views** | Used for the responsive and dynamic User Interface (UI). |
| **Inversion of Control** | **Dependency Injection (DI)** | Core pattern for managing service lifecycles and decoupling components. |

### 2. Architectural Layers

| Layer | Implementation Details |
| ----- | ----- |
| **Presentation Layer (MVC)** | Handled by **MVC Controllers** and **Razor Views**. Responsible for routing user requests, validating input, and rendering the UI. |
| **Business Logic Layer** | A collection of dedicated **Services** (e.g., `TrainerService`, `BookingService`) where the core logic resides, applying all business rules and orchestrating data flow. |
| **Data Access Layer** | Managed by the **Repository** pattern and **Entity Framework Core**. This layer abstracts database operations, ensuring clean access and manipulation of data entities. |

## 🚀 Getting Started

Follow these steps to set up and run the project locally.

### Prerequisites

* [**.NET 9.0 SDK**](https://dotnet.microsoft.com/download/dotnet/9.0) (or newer)

* **SQL Server LocalDB** or a configured SQL Server instance

* **Visual Studio** (recommended IDE)

### Installation & Setup

1. **Clone the Repository:**
```
git clone https://github.com/Mahmoudd-92/GymManagementMVC

```

2. **Configure Database Connection:**

* Open the main project configuration files: **`appsettings.json`** and **`appsettings.Development.json`**.

* Update the `ConnectionStrings` section with your local SQL Server details.

3. **Run Migrations:**

* Open the terminal in the solution directory and apply the database schema changes and initial data (which includes the user roles):
```
dotnet ef database update --project GymManagmentDAL

```

4. **Run the Application:**

* Run the main MVC project:
```
dotnet run --project GymManagmentMVC

```

The application should launch in your web browser, typically at `http://localhost:7109` or a similar port.

## 🔐 Initial Login Credentials

Use the following seed credentials to access the application and explore the different user roles:

| Role | Email | Password |
| ----- | ----- |
| **Super Admin** | `Mahmoud@gmail.com` | `P@ssw0rd` |
| **Admin** | `Ahmed@gmail.com` | `P@ssw0rd`
