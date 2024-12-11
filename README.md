# 🎨 Art Exhibition Management System 🖼️

Welcome to the **Art Exhibition Management System** repository! This system provides an elegant solution for managing art exhibitions, including functionalities for artists, artworks, and exhibitions, offering both administrators and users a seamless experience.

---

## 🌟 Features

### 🎭 For Artists

- **Profile Management**: Create and maintain a professional profile with bio, website, and social media links.
- **Artwork Management**: Upload and manage artworks, including titles, descriptions, genres, and images.
- **Exhibition Participation**: Easily link artworks to exhibitions.

### 👥 For Visitors

- **Exhibition Browser**: Explore ongoing and upcoming exhibitions.
- **Artwork Viewer**: View high-quality images and details of artworks.
- **Artist Profiles**: Learn more about participating artists.

### 🔧 For Administrators

- **User Management**: Manage artists and visitors.
- **Exhibition Management**: Create and organize exhibitions, assigning artworks and artists.
- **Analytics Dashboard**: View key metrics and insights about exhibitions.

---

## 🛠️ Technologies Used

### ⚙️ Backend

- **ASP.NET Core**: Powerful backend framework for RESTful APIs.
- **Entity Framework Core**: Database management and migrations.
- **SQL Server**: Robust relational database for storing project data.

### 🌐 Frontend

- **Razor Pages**: Dynamic server-rendered views for seamless user experience.

### 📦 Other Tools

- **Identity Framework**: Secure user authentication and authorization.
- **Bootstrap**: Responsive UI design.
- **Cloudinary**: Image hosting and optimization.

---

## 📝 Installation Guide

### ✅ Prerequisites

- [.NET SDK 9.0+](https://dotnet.microsoft.com/)
- [SQL Server](https://www.microsoft.com/sql-server/)

### ⚙️ Setup

1. **Clone the Repository**

   ```bash
     git clone https://github.com/username/ArtExhibition.git
     cd ArtExhibition
   
2. **Configure the Database**
- Open the appsettings.json file.
- Update the ConnectionStrings section with your SQL Server details.

3. **Apply Migrations**
      ```bash
      dotnet ef database update

5. Run the Application
    ```bash
         dotnet run
The application will be available at https://localhost:5001.

---

## 🚀 Usage

### 🎨 Artists

1. Register an account and create your profile.
2. Upload artworks with details and images.
3. Link artworks to exhibitions.

### 🖼️ Visitors

1. Browse exhibitions and artworks.
2. View artist profiles and follow favorite artists.

### 🔧 Administrators

1. Manage users, exhibitions, and artworks.
2. Monitor key metrics through the analytics dashboard.

---

## 🎥 Demo

Check out a live demo of the application on YouTube: [Art Exhibition Management System Demo](https://youtu.be/PwTK3yxbPJ8)

---

## 🔗 API Endpoints

### 🔑 Authentication

- `POST /api/auth/register`: Register a new user.
- `POST /api/auth/login`: Authenticate a user.

### 👩‍🎨 Artists

- `GET /api/artists`: Retrieve all artists.
- `POST /api/artists`: Create a new artist profile.

### 🎨 Artworks

- `GET /api/artworks`: Retrieve all artworks.
- `POST /api/artworks`: Upload a new artwork.

### 🗓️ Exhibitions

- `GET /api/exhibitions`: Retrieve all exhibitions.
- `POST /api/exhibitions`: Create a new exhibition.

---

## 🤝 Contributing

We welcome contributions from the community! To contribute:

1. Fork the repository.
2. Create a new branch for your feature/bugfix.
3. Commit your changes.
4. Push the branch and create a pull request.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

## 📧 Contact

For inquiries, please contact:

- **Project Lead**: Adem Bessam ([email](mailto:adem.bessam@example.com))

---

## 🙏 Acknowledgments

Special thanks to all contributors, testers, and the open-source community for making this project possible! 🌟

