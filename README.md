# Movie Search Application

This project is a full-stack Movie Search Application built using **ASP.NET Core** for the backend and **ReactJS** for the frontend. It utilizes the [OMDb API](http://www.omdbapi.com/) to provide movie search functionality and detailed information about movies.

---

## Features

### 1. Movie Search by Title

* Users can search for movies by entering a title.
* Displays a list of search results including movie titles and posters.

### 2. Recent Searches

* Saves the last 5 search queries for quick access.
* Users can click on a recent query to re-initiate the search.

### 3. Display Search Results

* Lists movies that match the search query.
* Each movie displays its title and poster.

### 4. Extended Movie Details

* Clicking on a movie from the search results shows extended information such as:
  * Title
  * Plot
  * IMDb Rating
  * Poster

### 5. Backend API

* **Search API** : Fetches movies based on title from the OMDb API.
* **Details API** : Fetches detailed information about a specific movie by IMDb ID.

### 6. Unit Testing

* Backend includes unit tests for API endpoints to ensure proper functionality.

---

## Architecture

### Backend

* **Framework** : ASP.NET Core 6
* **Structure** : Follows a layered architecture:
* **Controllers** : Handles HTTP requests and routes them to services.
* **Services** : Contains the business logic for interacting with the OMDb API.
* **Models** : Defines data structures used in the application.
* **Dependency Injection (DI)** : Utilized for injecting services and HttpClient.
* **Configuration** :
* OMDb API base URL and API key are stored in `appsettings.json`.
* HttpClient is configured with DI to use the base URL.

### Frontend

* **Framework** : ReactJS
* **Structure** :
* **Components** : Reusable and modular React components for UI.
  * `MovieSearch`: Search bar and recent search history.
  * `MovieList`: Displays search results.
  * `MovieDetails`: Shows extended movie details.
* **Axios** : Used for HTTP requests to the backend.
* **State Management** : Uses React hooks (`useState`) for managing state.
* **Styling** : Basic CSS for a responsive and user-friendly UI.

---

## Dependencies

### Backend

* **ASP.NET Core 6** : Web application framework.
* **Newtonsoft.Json** : JSON serialization/deserialization.
* **xUnit** : Unit testing framework.
* **Moq** : Mocking library for testing.

### Frontend

* **ReactJS** : Frontend framework.
* **Axios** : For making HTTP requests.
* **Create React App** : Boilerplate for setting up the React project.

---

## Installation and Setup

### Prerequisites

* .NET 6 SDK
* Node.js and npm

### Backend Setup

1. Clone the repository.
2. Navigate to the backend directory:
   ```bash
   cd backend
   ```
3. Add your OMDb API key to `appsettings.json`:
   ```json
   {
     "OmdbApi": {
       "BaseUrl": "http://www.omdbapi.com/",
       "ApiKey": "your-omdb-api-key"
     }
   }
   ```
4. Run the application:
   ```bash
   dotnet run
   ```
5. The backend API will be available at `http://localhost:5000`.

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Start the application:
   ```bash
   npm start
   ```
4. The frontend will be available at `http://localhost:3000`.

---

## API Endpoints

### Search Movies

 **Endpoint** : `/api/movies/search`

* **Method** : GET
* **Query Parameters** :
* `title` (string): Movie title to search for.
* **Response** : Returns a list of movies matching the title.

### Get Movie Details

 **Endpoint** : `/api/movies/details`

* **Method** : GET
* **Query Parameters** :
* `imdbId` (string): IMDb ID of the movie.
* **Response** : Returns detailed information about the movie.

---

## Testing

### Backend Testing

* **Framework** : xUnit
* Run tests using the following command:
  ```bash
  dotnet test
  ```
* Tests ensure:
  * Search API returns valid results for a given title.
  * Details API fetches the correct movie information for a given IMDb ID.

### Frontend Testing

* Tests can be added using Jest or React Testing Library.

---

## Future Enhancements

* Add pagination to search results.
* Implement user authentication.
* Save recent searches to the backend database for persistence.
* Add advanced search filters (e.g., genre, year).

---

## Resources

* [OMDb API Documentation](https://www.omdbapi.com/)
* [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
* [ReactJS Documentation](https://reactjs.org/docs/getting-started.html)
* [Axios Documentation](https://axios-http.com/)
* [xUnit Documentation](https://xunit.net/)
