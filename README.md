# GameStore ASP.NET Project

## Description
This is an ASP.NET Core project using SQLite and various NuGet packages, written in C#.  
It allows full CRUD (Create, Read, Update, Delete) operations on games.  
Each game has its own unique ID, name, genre ID (linked to genre), price, and release date stored in the database.

This project was built following Julio Casal's **ASP.NET Core Full Course For Beginners** (Feb 22nd, 2024).  
[Watch the tutorial here](https://www.youtube.com/watch?v=AhAxLiGC7Pc&t=587s&ab_channel=JulioCasal)

---

<details>
<summary><strong>Features</strong></summary>
  
- Full CRUD operations on games  
- Each game entity includes:  
  - Unique ID  
  - Name  
  - Genre ID and associated Genre  
  - Price  
  - Release Date  
- Genre management  
- SQLite database backend  
- RESTful API design  

<!-- Add your features description here -->

</details>

---

<details>
<summary><strong>Concepts Learned</strong></summary>

- REST API  
- Data Transfer Objects (DTOs)  
- CRUD Endpoints  
- Extension Methods  
- Routing  
- Debugging and Handling Errors  
- Entity Framework Core  
- Data Model  
- Core Configuration System  
- Database Seeding  
- Dependency Injection and Service Lifetimes  
- Mapping Entities to DTOs  
- Querying, Updating, and Deleting Entities  
- Asynchronous Programming Model  
- API Integration with Frontend  

</details>



## Installation

1. **Clone the repository:**

```bash
git clone https://github.com/TomChimorin/aspnet-gamestore.git
```

## Usage

Database migrations and data seeding are applied automatically when the application starts, thanks to the `await app.MigrateDbAsync();` call in `Program.cs`.

To run the application locally, use the following command in the project directory:

```bash
dotnet run
```
