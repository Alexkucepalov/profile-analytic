FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY *.csproj ./
RUN dotnet restore

COPY . .

RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 5000 5001
ENTRYPOINT ["dotnet", "Horizons.dll"]

# docker run -p 5000:5000 -p 5001:5001 -e ConnectionStrings__Horizons="Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=password" -e ASPNETCORE_URLS="http://+:5000/" profileanalytic-backend:latest -d
