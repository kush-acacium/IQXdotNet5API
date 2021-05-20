#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
RUN apt-get update
RUN apt-get install unixodbc-dev unixodbc-bin unixodbc -y
ENV ODBCHOME=/etc 
ENV ODBCINST=/etc/odbcinst.ini
ENV LD_LIBRARY_PATH /opt/sqlanywhere12/lib64

WORKDIR /src
COPY . .
RUN dotnet sln remove docker-compose.dcproj
RUN dotnet restore
RUN dotnet build

FROM build AS test
RUN dotnet test

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish
RUN dotnet dev-certs https -ep /app/https/app.pfx -p dev-app

FROM base AS final
COPY --from=publish /app/publish .
COPY --from=publish /app/https /https
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/app.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=dev-app
ENTRYPOINT ["dotnet", "IQXdotNet5API.dll"]
