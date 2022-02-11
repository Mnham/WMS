FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WMS.EmployeeService/WMS.EmployeeService.csproj", "src/WMS.EmployeeService/"]
RUN dotnet restore "src/WMS.EmployeeService/WMS.EmployeeService.csproj"
COPY . .
WORKDIR "/src/src/WMS.EmployeeService"
RUN dotnet build "WMS.EmployeeService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WMS.EmployeeService.csproj" -c Release -o /app/publish
COPY "entrypoint.sh" "/app/publish/."

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 80

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN chmod +x entrypoint.sh
CMD /bin/bash entrypoint.sh