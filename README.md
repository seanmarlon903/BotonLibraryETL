# BotonLibraryETL

A comprehensive ETL (Extract, Transform, Load) project for managing library book data.

## Project Structure

- **CuarioLibraryETL**: Main ETL application that processes legacy book data and loads it into the API
  - Reads book data from CSV files
  - Transforms and validates the data
  - Uploads records to the Cuario Library API

## Features

- CSV data parsing and validation
- Data transformation (title casing, type conversion)
- HTTP-based data loading to remote API
- Error handling and status reporting

## Getting Started

### Prerequisites
- .NET 8.0 or higher
- C#

### Running the Application

```bash
cd CuarioLibraryETL
dotnet run
```

The application will:
1. Read `legacy_books.csv`
2. Transform the book records
3. Load them into the API at `https://cuariolibrarynowapi-c1n9.onrender.com`

## Data Format

The `legacy_books.csv` file should have the following columns:
- `id`: Book identifier
- `book_title`: Title of the book
- `writer`: Author name
- `book_type`: Genre/Category
- `is_available`: Availability status (yes/no)
- `year_pub`: Publication year

## Author
Sean Marlon Boton

---
*BotonLibraryETL - Your personal library data management solution*
