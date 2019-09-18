FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
 
RUN dotnet restore
 
COPY . .

RUN dotnet test "./test/test.csproj" -c Release /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "webapi.dll"]
