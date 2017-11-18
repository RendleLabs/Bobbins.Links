FROM microsoft/aspnetcore-build:2.0.3 AS build

WORKDIR /code

COPY . .

WORKDIR /code/src/Bobbins.Links

RUN dotnet publish --output /output --configuration Release

FROM microsoft/aspnetcore:2.0.3

COPY --from=build /output /app/

WORKDIR /app

ENTRYPOINT ["dotnet", "Bobbins.Links.dll"]
