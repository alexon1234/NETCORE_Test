FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
 
RUN dotnet restore
 
COPY . .

RUN dotnet test "./test/test.csproj" -c Release
RUN dotnet publish "./webapi/webapi.csproj" -c Release -o /publish/ --no-restore

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "webapi.dll"]