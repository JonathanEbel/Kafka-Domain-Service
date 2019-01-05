FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Organizations.Service/Organizations.Service.csproj", "Organizations.Service/"]
RUN dotnet restore "Organizations.Service/Organizations.Service.csproj"
COPY . .
WORKDIR "/src/Organizations.Service"
RUN dotnet build "Organizations.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Organizations.Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Organizations.Service.dll"]