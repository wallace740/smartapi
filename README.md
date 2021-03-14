# smartapi
Smart Apartment Api Assignment - .NET 5.0 Core

This API project is a showcase project. It is used to List, Insert and Update products using Auth0 for authentication.

- DALCore : Data layer of the application. FireBase is used as Realtime Database server. 
- BusinessCore : This layer is used to serve the necessary business implementations for the API requests. 
- Wallace740-SmartApi : This layer executes the requests from users. Auth0 is configured with scopes (read:products , write:products)
- UnitTest : Unit test project is added to check token services, Business and Repositories can be added later.
                     
Mediator pattern is used to demonstrate a commonly used best-practice example with MediatR package.

