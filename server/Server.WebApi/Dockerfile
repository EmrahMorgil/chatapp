#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

# ...

# Dependencies
# Base image olarak .NET SDK kullan
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Projeyi kopyala
COPY . ./

# Proje build iþlemini gerçekleþtir
RUN dotnet publish -c Release -o out

# Çalýþtýrýlabilir dosyalarý belirle
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Uygulamayý çalýþtýr
ENTRYPOINT ["dotnet", "YourApiName.dll"]

