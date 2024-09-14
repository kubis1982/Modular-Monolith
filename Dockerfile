#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 443 

FROM mcr.microsoft.com/dotnet/sdk:8.0.100 AS publish
WORKDIR /src
COPY . .
WORKDIR "/src/src/Bootstraper"
RUN dotnet publish "Bootstraper.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModularMonolith.Bootstraper.dll"]