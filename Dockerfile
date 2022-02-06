FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GraphQL_Test2.csproj", "./"]
RUN dotnet restore "GraphQL_Test2.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "GraphQL_Test2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GraphQL_Test2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQL_Test2.dll"]
