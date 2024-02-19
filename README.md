# Virtual Vending Machine

## Introduction

This project implements a virtual vending machine using **Clean Architecture** and **CQRS** principles. It allows users to browse a list of products, deposit balance, and purchase items. Users can then view their purchased items and receive change if applicable.

The project includes basic business rules such as preventing purchases when the quantity is insufficient or when the user hasn't added enough funds.

## Features

- Browse available products
- Deposit funds to the virtual balance
- Purchase items from the vending machine
- View a receipt showing purchased items
- Receive change after transactions

## Dependencies

The project utilizes the following dependencies:

- **AutoMapper**: For mapping between DTOs and domain entities.
- **FluentValidation**: For validating commands and queries.
- **MediatR**: For implementing CQRS pattern.

## Installation

To run the project, follow these steps:

1. Clone the repository to your local machine.
2. Open the solution in your preferred IDE.
3. Build the solution to restore dependencies.
4. Run the application.

**Note:** Installing Docker Desktop is necessary to run the application. Ensure Docker Desktop is installed and running on your machine before proceeding.

## Usage

Once the application is running, users can interact with the virtual vending machine through the provided API endpoints. Below are some example endpoints:

- `GET /api/product/get`: Retrieve a list of available products.
- `POST /api/balance/deposit`: Add funds to the virtual wallet.
- `POST /api/transaction/purchase`: Purchase an item from the vending machine.

For detailed API documentation, refer to the [OpenAPI YAML specs](https://app.swaggerhub.com/apis-docs/BARRYGUVENKAYA/vending-machine_api/1.0#/).

## Production Readiness

While the project serves as a functional prototype, it's not yet ready for production due to the following reasons:

- Lack of authentication: Authentication is essential for securing access to the API endpoints.
- Missing pagination: Pagination is necessary to efficiently handle large datasets.
- Use of EF Core in-memory database: In-memory databases are suitable for prototyping but not for production use.
- Absence of EF entity configurations and relations: Proper database configurations and relationships are required for scalability and maintainability.
- No health checks: Health checks are essential for monitoring application health and availability.
- Unimplemented options configuration and caching: These features are important for performance optimization and configuration flexibility.

## Nice-to-Have Features for Future

In future iterations, consider adding the following features to enhance the project:

- Localisation and currency support for products.
- Timezone support for date/time operations.
- Authorisation logic using CQRS authorisers.
- Auto-generation of NSwag client code for easier adoption.
