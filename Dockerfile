FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Install OpenJDK-8
RUN apt-get update && \
    apt-get install -y openjdk-8-jdk && \
    apt-get install -y ant && \
    apt-get clean;

# Fix certificate issues
RUN apt-get update && \
    apt-get install ca-certificates-java && \
    apt-get clean && \
    update-ca-certificates -f;

# Setup JAVA_HOME -- useful for docker commandline
ENV JAVA_HOME /usr/lib/jvm/java-8-openjdk-amd64/
RUN export JAVA_HOME


COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
 
RUN dotnet restore
 
COPY . .

RUN dotnet test "./test/test.csproj" -c Release /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

RUN dotnet tool install --global dotnet-sonarscanner
ENV PATH="${PATH}:/root/.dotnet/tools"
    
RUN dotnet sonarscanner begin /key:"alexon1234_TODO_APP" /d:sonar.organization="alexon1234-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="a22b844295f76a8e2a09918d27786776e0506378" /d:sonar.cs.opencover.reportsPaths="test/coverage.opencover.xml"
RUN dotnet build
RUN dotnet sonarscanner end /d:sonar.login="a22b844295f76a8e2a09918d27786776e0506378" 
RUN dotnet publish "./webapi/webapi.csproj" -c Release -o /publish/ --no-restore

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "webapi.dll"]
