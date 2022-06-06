#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FMAPI.csproj", "."]
RUN dotnet restore "./FMAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "FMAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FMAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FMAPI.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet "FMAPI.dll"
ENV FMDATABASE=Server=ec2-52-3-200-138.compute-1.amazonaws.com;Port=5432;Database=d503fi3sdgn8s7;Username=xcqzjwldzbjawo;Password=4f995f25c04955345b5c27df5ae2c3471eb7261dd19ecc3da195ec6c1b2647e6
CMD docker -d -v temp-keys:/root/.aspnet/DataProtection-Keys fm-apiv1
