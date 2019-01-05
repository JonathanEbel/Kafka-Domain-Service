FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Identity.Service/Identity.Service.csproj", "Identity.Service/"]
RUN dotnet restore "Identity.Service/Identity.Service.csproj"
COPY . .
WORKDIR "/src/Identity.Service"
RUN dotnet build "Identity.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Identity.Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Identity.Service.dll"]