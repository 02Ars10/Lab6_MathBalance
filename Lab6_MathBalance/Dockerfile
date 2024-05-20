#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Lab6_MathBalance/Lab6_MathBalance.csproj", "Lab6_MathBalance/"]
RUN dotnet restore "Lab6_MathBalance/Lab6_MathBalance.csproj"
COPY . .
WORKDIR "/src/Lab6_MathBalance"
RUN dotnet build "Lab6_MathBalance.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab6_MathBalance.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab6_MathBalance.dll"]