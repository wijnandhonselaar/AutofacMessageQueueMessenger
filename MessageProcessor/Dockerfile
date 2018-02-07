FROM microsoft/dotnet-framework-build:4.7.1 as build-env

WORKDIR /app
COPY . /app

RUN ["dotnet", "restore"]
#Change output path from build to /app/out
RUN ["dotnet", "build"]

FROM microsoft/dotnet-framework:4.7.1
WORKDIR /app
#Change from patch to build-env /app/out
COPY --from=build-env /app .

ENTRYPOINT ["MessageProcessor.exe"]

