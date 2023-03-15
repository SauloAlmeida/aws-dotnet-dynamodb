FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "AWSDotnetDynamoDB.csproj" --disable-parallel
RUN dotnet publish "AWSDotnetDynamoDB.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_URLS=http://+:5000 

ENV AWS_ACCESS_KEY=
ENV AWS_SECRET_KEY=

ENTRYPOINT ["dotnet", "AWSDotnetDynamoDB.dll"]