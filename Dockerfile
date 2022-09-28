# See: https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-6.0#the-dockerfile
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG TARGETPLATFORM
ARG BUILDPLATFORM
RUN echo "I am running on $BUILDPLATFORM, building for $TARGETPLATFORM"

WORKDIR /source
COPY *.sln .
COPY PRMasterServer/*.csproj ./PRMasterServer/
RUN dotnet restore

COPY . .
WORKDIR /source/PRMasterServer
RUN dotnet publish -c release -o /app --no-restore

WORKDIR /app
RUN echo 'bf2' > modwhitelist.txt
RUN wget -q https://cdn.jsdelivr.net/npm/geolite2-country@1.0.2/GeoLite2-Country.mmdb.gz
RUN gzip -d GeoLite2-Country.mmdb.gz

FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /app
COPY --from=build /app ./
VOLUME /data
CMD ["dotnet", "PRMasterServer.dll", "+db", "/data/LoginDatabase.db3"]
