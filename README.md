# Todo App

A simple Todo app that is taken to great lengths.

It's basically my playground to try out new technologies and techniques.

## Images

![List](Fatihdgn.Todo.Web/images/list.png)
![Menu](Fatihdgn.Todo.Web/images/menu.png)

## Technologies

- ASP.NET Core Web API
- Entity Framework Core
- Microsoft ASP.NET Core Identity
- Swagger support
- .NET MAUI
- Angular
- Capacitor (for native iOS and Android apps)
- Docker

## Techniques

- Functional programming in C#
- CQRS
- Unit testing

## Third-party Libraries

- OneOf
- FluentValidation
- MediatR
- Moq
- Newtonsoft.Json
- NSwag
- xunit

Feel free to give it a spin. Giving a star to the repository would be appreciated.

## Installation

Here are the installation steps

### Prepare the user secrets

There is one secret that needs to be prepared. Go to the root of the repository. Initialize the user secrets if not already.

```sh
dotnet user-secrets init
```

After that, create the secret below.

```sh
dotnet user-secrets set "JwtBearerAuthenticationIssuerSigningKey" "<generate_a_long_string_here>" --project "Fatihdgn.Todo.API"
```

You can use [this site](https://generate-random.org/string-generator?count=1&length=256&has_lowercase=0&has_lowercase=1&has_uppercase=0&has_symbols=0&has_numbers=0&has_numbers=1&is_pronounceable=0) for generating this secret.


### Run the application

Then run the command below.
```sh
docker compose up
```

## OR

### Change connection string

Look into "Fatihdgn.Todo.API/appsettings.json" file and replace the connection string there.


```json
{
  "ConnectionStrings": {
    "TodoDB": "<your_connection_string>"
  },
  ...
}
```

### Run the API

You can run the API using the command below

```sh
dotnet run --project Fatihdgn.Todo.API -c Release environment=Production
```

After that, note the host address that you can find inside the logs of the command above in case it's changed. It starts with "Now listening on: https://<your_address>"

### Change the Web app environment

Change the files in "Fatihdgn.Todo.Web/src/environments/environment.ts" and "Fatihdgn.Todo.Web/src/environments/environment.prod.ts" to use the host address that you noted above.

```ts
export const environment = {
    production: false,
    apiBaseUrl: "https://<your_address>"
};
```

### Run the Web app

Now go to "Fatihdgn.Todo.Web" directory and run the app.

```sh
cd Fatihdgn.Todo.Web
npm start
```

That's it!