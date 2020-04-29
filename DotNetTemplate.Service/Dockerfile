FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
ADD DotNetTemplate.Service/start.sh /
COPY wait-for-it.sh /
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["DotNetTemplate.Service/DotNetTemplate.Service.csproj", "DotNetTemplate.Service/"]
COPY NuGet.Config ./
RUN dotnet restore "DotNetTemplate.Service/DotNetTemplate.Service.csproj" --configfile ./NuGet.Config
COPY . .
WORKDIR "/src/DotNetTemplate.Service"
RUN dotnet build "DotNetTemplate.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DotNetTemplate.Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT /bin/bash /wait-for-it.sh postgres:5432 -- /bin/bash /start.sh
