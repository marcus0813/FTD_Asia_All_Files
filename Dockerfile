#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY FTD_Asia_API/FTD_Asia_API.csproj ./FTD_Asia_API/
RUN dotnet restore "./FTD_Asia_API/FTD_Asia_API.csproj"
COPY . .
WORKDIR "/src/FTD_Asia_API"
RUN dotnet build "FTD_Asia_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FTD_Asia_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FTD_Asia_API.dll"]