#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV PORT 80
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS "http://*:${PORT}"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UltimateMovieNight/UltimateMovieNight.csproj", "UltimateMovieNight/"]
RUN dotnet restore "UltimateMovieNight/UltimateMovieNight.csproj"
COPY . .
WORKDIR "/src/UltimateMovieNight"
RUN dotnet build "UltimateMovieNight.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UltimateMovieNight.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UltimateMovieNight.dll"]
