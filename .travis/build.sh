#!/usr/bin/bash

dotnet restore
dotnet build src/NetGears.Core
dotnet build src/NetGears.Game
dotnet build src/NetGears.Login
dotnet build src/NetGears.ORM
