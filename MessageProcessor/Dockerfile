FROM microsoft/dotnet-framework-build:4.7.1 as build-env

WORKDIR /app
COPY . /app
RUN nuget.exe restore MessageProcessor.csproj -SolutionDirectory ../
RUN MSBuild.exe MessageProcessor.csproj /t:build /p:Configuration=Release /p:OutputPath=./out /v:m

FROM microsoft/dotnet-framework:4.7.1
WORKDIR /app
COPY --from=build-env app/out .

ENTRYPOINT ["MessageProcessor.exe"]