FROM microsoft/dotnet-framework-build:4.7.1 as build-env

WORKDIR /app

COPY . /app

RUN ["dotnet","restore"]
RUN ["dotnet", "build"]
#RUN msbuild.exe /t:Restore /p:OutputPath=out/packages
#RUN msbuild.exe /t:Build /p:Configuration=Release /p:OutputPath=out
#RUN ["nuget", "restore -SolutionDirectory ../"]

FROM microsoft/dotnet-framework:4.7.1
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["MessageProcessor.exe"]