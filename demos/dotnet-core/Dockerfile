FROM mcr.microsoft.com/dotnet/core/runtime:2.2

WORKDIR c:/api
COPY ./bin/Release/netcoreapp2.2/win-x64/* ./
COPY ./keys/* c:/keys/
RUN dotnet dotnet-core-api.dll
