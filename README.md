# **CarBazaar**

## **Table of Contents**
1. [About the Project](#about-the-project)
2. [Features](#features)
3. [Technologies Used](#technologies-used)
4. [Prerequisites](#prerequisites)
5. [Installation](#installation)
6. [Running the Application](#running-the-application)
7. [Usage](#usage)
8. [API Endpoints](#api-endpoints)
9. [Contributing](#contributing)
10. [License](#license)

---

## **About the Project**

CarBazaar is a modern web application that allows users to **list, search, and manage cars**. Users can browse car listings, chat with sellers in real-time, and manage their favorite cars.

This project is built with **ASP.NET Core** for the backend API and **React.js** for the frontend, creating a full-stack web application.

---

## **Features**

- **User Authentication**: Register and log in securely with JWT.
- **Car Listings**: 
   - Add, Edit, Delete car listings.
   - Search and filter listings by car type, brand, and price range.
- **Favourites Management**: Save favourite cars for later.
- **Real-Time Chat**: Chat with car sellers via SignalR integration.
- **Notifications**: Receive notifications about new messages.
- **Responsive Design**: Fully responsive UI for all devices.
- **Admin Features**: Admin can manage all listings and users.

---

## **Technologies Used**

### Backend
- **ASP.NET Core** (Web API)
- **Entity Framework Core** (ORM)
- **SignalR** (Real-Time Communication)

### Frontend
- **React.js** (Front-end Library)
- **Material-UI** (UI Components and Styling)
- **Axios** (HTTP Client for API calls)
- **Redux** (State Management)
- **Formik + Yup** (Form Handling and Validation)

### Database
- **SQL Server** 

---

## **Prerequisites**

Before running this project, ensure you have the following installed:

1. **Node.js** and **npm**:
2. **.NET SDK**: 
3. **SQL Server** or LocalDB for database setup.
4. **Redis Database** (**Docker** or other container application)

---

## **Installation**

Follow these steps to set up the project locally:

### **1. Clone the Repository**
```bash
git clone https://github.com/bojidars1/CarBazaar.git
cd CarBazaar
```

### **2. Backend Setup**
#### 1. Navigate to the `CarBazaar.Server` directory:
```bash
cd CarBazaar.Server
```
#### 2. Install dependencies:
```bash
dotnet restore
```
#### 3. Apply database migrations:
```bash
dotnet ef database update or Update-Database
```
#### 4. Run the backend server:
```bash
dotnet run
```
#### 5. The backend API will be available at:
```bash
https://localhost:7100
```
### **3. Frontend Setup**
#### 1. Navigate to the `CarBazaar.Client` directory:
```bash
cd CarBazaar.Client
```
#### 2. Install dependencies:
```bash
npm install
```
#### 3. Start the frontend server:
```bash
npm start
```
#### 4. The frontend application will run at:
```bash
https://localhost:5173
```
## **API Endpoints**
#### User Authentication

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `POST` | `/Account/register` | Register a new user |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `POST` | `/Account/login` | Log in a user |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `POST` | `/Account/logout` | Logout the current user |

#### Admin

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `GET` | `/Admin/get-users` | Gets all application users |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `GET` | `/Admin/get-listings` | Gets all application car listings |

#### Car Listing

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `GET` | `/CarListing` | Get all car listings |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `GET` | `/CarListing/{id}` | Get a single car listing by ID |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `GET` | `/CarListing/delete/{id}` | Get a delete car listing dto by ID |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `GET` | `/CarListing/search` | Get a filtered car listings |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `POST` | `/CarListing` | Add a new car listing |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `PUT` | `/CarListing` | Update a car listing |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `DELETE` | `/CarListing` | Delete a car listing |

#### Chat

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `POST` | `/Chat/send` | Send message to user |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `GET` | `/Chat/messages/{carListingId}/{participantId}` | Get messages with user about a certain car listing |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `GET` | `/Chat/summaries` | Gets user's chat summaries |


#### Favourite Car Listing

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `GET` | `/FavouriteCarListing/get-favourites` | Get favourites |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `POST` | `/FavouriteCarListing/{carId}` | Favourite a car listing by id |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `DELETE` | `/FavouriteCarListing/{carId}` | Remove car listing from favourites |

#### Notification

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `GET` | `/Notification/get-notiffications` | Get notiffications |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `POST` | `/Notification/mark-as-read` | Marks a notification as read |

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `DELETE` | `/FavouriteCarListing/delete` | Deletes a notification |

#### User Car Listing

| Method | Endpoint     | Description                |
| :-------- | :------- | :------------------------- |
| `GET` | `/UserCarListing/get-listings` | Get car listings of a user |
## Contributing

Contributions are always welcome!

#### 1. Fork the repository
#### 2. Create a new feature branch
```bash
git checkout -b feature/YourFeatureName
```
#### 3. Commit your changes
```bash
git commit -m "Add a descriptive commit message"
```
#### 4. Push your branch
```bash
git push origin feature/YourFeatureName
```
#### 5. Open a pull request

## License

This project is licensed under the **CC0 1.0 Universal License**.

This means you can use, modify, distribute, and share this project without any restrictions. For more details, visit the [Creative Commons CC0 License](https://creativecommons.org/publicdomain/zero/1.0/).
