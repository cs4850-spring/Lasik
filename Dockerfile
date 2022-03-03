  FROM mcr.microsoft.com/dotnet/aspnet:6.0
  COPY Lasik/bin/Release/net6.0/ App/
  WORKDIR /App
  ENV ASPNETCORE_URLS=http://+:8081
  ENTRYPOINT ["dotnet", "Lasik.dll"]
