#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
FROM kushel/base-sqlanywhere-dotnet:latest As base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS build
FROM kushel/base-sqlanywhere-dotnet:latest AS build
RUN apt-get update
RUN apt-get install unixodbc-dev unixodbc-bin unixodbc -y
ENV ODBCHOME=/etc 
ENV ODBCINST=/etc/odbcinst.ini
ENV LD_LIBRARY_PATH /opt/sqlanywhere12/lib64

WORKDIR /src
COPY ["IQXdotNet5API/IQXdotNet5API.csproj", "IQXdotNet5API/"]
RUN dotnet restore "IQXdotNet5API/IQXdotNet5API.csproj"
COPY . .
WORKDIR "/src/IQXdotNet5API"
RUN dotnet build "IQXdotNet5API.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR "/src/IQXdotNet5API/Tests"
RUN dotnet test --logger:trx

FROM build AS publish
RUN dotnet publish "IQXdotNet5API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IQXdotNet5API.dll"]
