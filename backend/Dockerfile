FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/IceCreamRecipes.API/IceCreamRecipes.API.csproj", "src/IceCreamRecipes.API/"]
RUN dotnet restore "src/IceCreamRecipes.API/IceCreamRecipes.API.csproj"
COPY . .
WORKDIR "/src/src/IceCreamRecipes.API"
RUN dotnet build "IceCreamRecipes.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IceCreamRecipes.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IceCreamRecipes.API.dll"]