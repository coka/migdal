FROM microsoft/dotnet:2.0.0-sdk-2.0.2-stretch
COPY . .
RUN dotnet restore
ENTRYPOINT ["dotnet", "test", "./tests/Migdal.Tests.csproj"]
