FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
 
RUN dotnet restore
 
COPY . .

RUN dotnet test "./test/test.csproj" -c Release /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

RUN dotnet tool install --global dotnet-sonarscanner
RUN dotnet sonarscanner begin /k:"NETCORE_Test" /d:sonar.organization="alexon1234-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="e80ad883f5fd9d0f4a9cd19831ca84db2dbb545e" /d:sonar.cs.opencover.reportsPaths="test/coverage.opencover.xml"
RUN dotnet build
RUN dotnet sonarscanner end /d:sonar.login="e80ad883f5fd9d0f4a9cd19831ca84db2dbb545e"
RUN dotnet publish "./webapi/webapi.csproj" -c Release -o /publish/ --no-restore

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "webapi.dll"]
