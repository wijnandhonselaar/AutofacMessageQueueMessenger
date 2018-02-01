FROM microsoft/dotnet-framework-build:4.7.1 as build-env

WORKDIR /app
COPY . .

RUN ["dotnet", "build"]

FROM microsoft/dotnet-framework:4.7.1
WORKDIR /app
#COPY /app ./

ENTRYPOINT ["MessageProcessor.exe"]