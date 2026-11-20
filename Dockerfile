FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Lab13.Api/Lab13.Api.csproj", "Lab13.Api/"]
COPY ["Lab13.Application/Lab13.Application.csproj", "Lab13.Application/"]
COPY ["Lab13.Domain/Lab13.Domain.csproj", "Lab13.Domain/"]
COPY ["Lab13.Infrastructure/Lab13.Infrastructure.csproj", "Lab13.Infrastructure/"]
RUN dotnet restore "Lab13.Api/Lab13.Api.csproj"

COPY . .
WORKDIR "/src/Lab13.Api"
RUN dotnet publish "Lab13.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Lab13.Api.dll"]