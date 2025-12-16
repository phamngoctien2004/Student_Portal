FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# tại sao cần phải copy sang như này để làm gì ở dưới có COPY . . rồi mà? phải chăng là để giảm thời gian cho việc build lại container ?
COPY ["Api/Api.csproj", "Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
# restore là lệnh để làm gì
RUN dotnet restore "./Api/Api.csproj"
COPY . .

# Tại sao chỉ cần build và publish ở thư mục Api và giải thích câu lệnh build và publish này
WORKDIR "/src/Api"
RUN dotnet build "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Sau khi publish xong thì được những file gì
FROM base AS final
# Thư mục /app ở đây có khác thư mục /app ở dòng số 4 không
WORKDIR /app

# Câu lệnh này là gì ?
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

# Đây là export cổng 8080 truy cập ở ngoài vào ?
EXPOSE 8080

# cú pháp ntn ?
ENTRYPOINT ["dotnet", "Api.dll"]