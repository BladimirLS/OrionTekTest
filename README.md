# Client Management Application

This project is a full-stack web application for managing clients. The backend is built with .NET Core and the frontend is developed using React and Bootstrap.

## Features

- View a list of clients
- Create new clients
- Update existing clients
- Delete clients

## Backend

The backend is developed with .NET Core, following Clean Architecture principles. It provides RESTful APIs for managing clients.

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- SQL Server (if not using in-memory database)

### Installation

1. Navigate to the backend folder:
    ```bash
    cd Backend
    ```

2. Restore dependencies:
    ```bash
    dotnet restore
    ```

3. Run the application:
    ```bash
    dotnet run
    ```

The API will be available at `http://localhost:5142`.

### Endpoints

- `GET /api/clients` - Get all clients
- `GET /api/clients/{id}` - Get a specific client by ID
- `POST /api/clients` - Create a new client
- `PUT /api/clients/{id}` - Update an existing client
- `DELETE /api/clients/{id}` - Delete a client

## Frontend

The frontend is built with React and styled using Bootstrap.

### Prerequisites

- [Node.js and npm](https://nodejs.org/en/)

### Installation

1. Navigate to the frontend folder:
    ```bash
    cd Frontend
    ```

2. Install dependencies:
    ```bash
    npm install
    ```

3. Start the development server:
    ```bash
    npm start
    ```

The application will be available at `http://localhost:3000`.

### Features

- **Client List**: View a table of all clients with options to edit or delete.
- **Create Client**: A form to add a new client.
- **Update Client**: A form to edit the details of an existing client.

## Deployment

- Ensure both backend and frontend are running on their respective ports.
- Update the frontend `axios` base URL to match the backend API's address if necessary.

## Additional Notes

- This project uses FontAwesome for icons and Bootstrap for responsive design.
- The backend uses Entity Framework Core with an in-memory database for testing purposes.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.

