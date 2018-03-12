# CoinMarketCal API C#

C# Wrapper for CoinMarketCal API (https://coinmarketcal.com/) 

[![NuGet](https://img.shields.io/nuget/v/CoinMarketCalApi.svg)](https://www.nuget.org/packages/CoinMarketCalApi/)
 

## Available for:
- .NET Standard 2.0
- .NET 4.5

## Example:
```csharp
//there are two ways to handle authorization right now, in future AccessToken will be permanent
//1. Set ClientId and ClientSecret - it will try to obtain token automatically and will authorize again when token expires
//2. Set existing access token - it will not update automatically, if ClientId and ClientSecret not set
CoinMarketCalClient.ClientId = "your client Id";
CoinMarketCalClient.ClientSecret = "your client Id";
//OR
CoinMarketCalClient.AccessToken = "obtained access token"

// Get the instane
var client = new CoinMarketCalClient(); // or using ICoinMarketCalClient
// Get coins list
var coins = await client.Coins();
// Get categories list
var categories = await client.Categories();
// Get default events
var events = await client.Events();
// Get events for Bitcoin and Ripple
var result = await client.Events(new EventsRequest() { Coins = new List<string>() { "bitcoin", "ripple"} });
  
```
