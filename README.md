# Car Fleet

Car fleet is a web app to store and retrive details of a car.
Detail consist of

- Brand name
- Model name
- Colour
- Launch Date
- Release Date
- Fuel Type
- Transmission Type

## System Requirements

- Visual Studio 2019 or above
- .net 6 or above

## Project Setup

- Change the connection string in appsetting.json file.
- In VS open package manager console window. Run 'update-database' command.
- Then run the CarFleet.WebApp project.

## API Reference

#### Get Token

```http
  GET /api/Auth/getToken
```

Returns token to access all other internal API's.

#### Insert Car Brand

```http
  POST /api/CarBrand/insertCarBrand
```

```JSON
  {
    "brandName": "AUDI",
    "logoUrl": "www.audi.com"
  }
```

#### Get Car Brand

```http
  GET /api/CarBrand/getCarBrand
```

#### Insert Car Model

```http
  POST /api/Car/insertCar
```

```JSON
  {
    "brandId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "modelName": "string",
    "colorName": "string",
    "colorCode": "string",
    "launchDate": "2023-04-18T11:54:19.281Z",
    "releaseDate": "2023-04-18T11:54:19.281Z",
    "fuelType": "string",
    "transmissionType": "string"
  }
```

#### Get Cars

```http
  GET /api/Car/getCars
```

| Parameter   | Type     | Description                 |
| :---------- | :------- | :-------------------------- |
| `brandName` | `string` | Brand name of cars to fetch |
