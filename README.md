# CoinMarketCal API C#

C# Wrapper for CoinMarketCal API (https://coinmarketcal.com/) 

[![NuGet](https://img.shields.io/nuget/v/CoinMarketCalApi.svg)](https://www.nuget.org/packages/CoinMarketCalApi/)
 
### Attention!
CoinMarketCal API currently in beta



## Available for:
- .NET Standard 2.0
- .NET 4.5

## Example:
```csharp
// Get the instane
var client = new CoinMarketCalClient(); // or using ICoinMarketCalClient
// Get coins list
var coins = await client.Coins();
// Get categories list
var categories = await client.Categories();
// Get default events
var events = await client.Events();
// Get events for Bitcoin and Ripple
var result = await client.Events(new EventsRequest() { Coins = new List<string>() { "Bitcoin (BTC)", "Ripple (XRP)"} });
  
```