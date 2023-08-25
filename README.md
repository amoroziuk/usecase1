# .NET 6 Country Web API

A .NET 6 Web API application dedicated to managing country information.

## Features

- **Asynchronous Data Fetching:** Efficient data retrieval using asynchronous programming.
- **Modular Design:** Leveraging Dependency Injection for loose coupling and better testing capabilities.
- **Error Handling:** Each endpoint is equipped with error handling to ensure clarity on issues.

## Endpoints

1. **Fetch Countries**
   - **URL:** `GET api/Countries/fetchCountries`
   - **Description:** Fetches countries based on given criteria.
   - **Parameters:** 
     - `RequestModel` (includes fields like name, population, sort order, limit)
  
2. **Fetch Countries by Name**
   - **URL:** `GET api/Countries/byName`
   - **Description:** Filters countries by name.
   - **Parameters:** 
     - `name`: Name of the country.
     - `sort`: Sorting order (default: ascend).
     - `limit`: Maximum number of records (default: 15).
  
3. **Fetch Countries by Population**
   - **URL:** `GET api/Countries/byPopulation`
   - **Description:** Filters countries by population.
   - **Parameters:** 
     - `population`: Population number.
     - `sort`: Sorting order (default: ascend).
     - `limit`: Maximum number of records (default: 15).

## Error Responses

In the event of an error, the API returns a `BadRequest` status code accompanied by the exception message.

## Running Locally

To run the application locally, follow these steps:

1. **Clone the Repository**
   ```
   git clone https://github.com/your-repo/your-project.git
   ```

2. **Navigate to Project Directory**
   ```
   cd your-project
   ```

3. **Restore Packages**
   ```
   dotnet restore
   ```

4. **Build the Project**
   ```
   dotnet build
   ```

5. **Run the Project**
   ```
   dotnet run
   ```

## API Examples

Here are 10 examples of how to use the developed API endpoints:

### Fetch Countries
1. Fetch all countries with default settings
   ```
   GET /api/Countries/fetchCountries
   ```

2. Fetch countries sorted in descending order
   ```
   GET /api/Countries/fetchCountries?sort=descend
   ```

3. Fetch a maximum of 10 countries
   ```
   GET /api/Countries/fetchCountries?limit=10
   ```

### Fetch Countries by Name
4. Fetch countries with the name containing "United"
   ```
   GET /api/Countries/byName?name=United
   ```

5. Fetch countries with the name "Canada" sorted in descending order
   ```
   GET /api/Countries/byName?name=Canada&sort=descend
   ```

6. Fetch countries with the name "India" with a limit of 5 records
   ```
   GET /api/Countries/byName?name=India&limit=5
   ```

### Fetch Countries by Population
7. Fetch countries with a population greater than 1 million
   ```
   GET /api/Countries/byPopulation?population=1000000
   ```

8. Fetch countries with a population greater than 5 million, sorted in ascending order
   ```
   GET /api/Countries/byPopulation?population=5000000&sort=ascend
   ```

9. Fetch countries with a population less than 1 million, with a limit of 20 records
   ```
   GET /api/Countries/byPopulation?population=999999&limit=20
   ```

10. Fetch countries with a population of exactly 300,000
    ```
    GET /api/Countries/byPopulation?population=300000
    ```

---