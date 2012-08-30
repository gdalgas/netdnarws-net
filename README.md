# NetDNA REST Web Services NET Client

This is a Visual Studio 2012 project using .NET 4.5 

Make sure an obtain a proper account alias, consumer key, and consumer secret prior to using this library

## Usage

var api = new NetDNARWS.Api("account alais", "consumer key","consumer secret");

var accountResult = api.Get("/account.json")

## Methods
It has support for GET requests only at the moment

Every request can take an optional debug parameter which will write all objects returned to the console

var accountResult = api.Get("/account.json", true)