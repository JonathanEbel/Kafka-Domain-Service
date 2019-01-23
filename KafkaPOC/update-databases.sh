#!/bin/bash
echo "***Cleaning solution"
dotnet clean

echo "***Building solution"
dotnet build

echo "***Updating Identity Database"
cd Identity.Infrastructure
dotnet ef database update -s ../Identity.Service --context IdentityContext

echo "***Updating Projections database"
cd ../Projections.Infrastructure
dotnet ef database update -s ../Projections.Service --context ProjectionsContext
