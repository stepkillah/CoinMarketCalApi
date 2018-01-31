# CoinMarketCal API C#

C# Wrapper for CoinMarketCal API (https://coinmarketcal.com/) 

## Available for:
- .NET Standard 2.0
- .NET 4.5

## Example:
```csharp
// Get the instane
var client = new CoinMarketCalClient(); // or using DI ICoinMarketCalClient
// Get coins list
var coins = await client.Coins();
// Get categories list
var categories = await client.Categories();
// Get default events
var events = await client.Events();
// Get events for Bitcoin and Ripple
var result = await client.Events(new EventsReqeust() { Coins = new List<string>() { "Bitcoin (BTC)", "Ripple (XRP)"} });
  
```
