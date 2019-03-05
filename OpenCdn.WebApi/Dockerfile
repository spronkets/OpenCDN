FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY "OpenCdn.WebApi/OpenCdn.WebApi.csproj" "./OpenCdn.WebApi/"
RUN dotnet restore "OpenCdn.WebApi/OpenCdn.WebApi.csproj" --no-cache # --source "<Custom Nuget Host>"
COPY . ./
WORKDIR "/src/OpenCdn.WebApi"
RUN dotnet build "OpenCdn.WebApi.csproj" -c Release -o /app --framework netcoreapp2.2 --runtime linux-x64 # --source "<Custom Nuget Host>"

FROM build AS publish
RUN dotnet publish "OpenCdn.WebApi.csproj" -c Release -o /app --framework netcoreapp2.2 --runtime linux-x64 # --source "<Custom Nuget Host>"

FROM base AS final
WORKDIR /app
COPY --from=publish /app ./
HEALTHCHECK --interval=30s --timeout=30s --start-period=5s --retries=3 CMD curl --fail "http://*:8080/api/ping" || exit 1
ENV ASPNETCORE_URLS "http://*:8080"
ENTRYPOINT "dotnet" "OpenCdn.WebApi.dll"
