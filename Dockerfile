FROM microsoft/aspnetcore:2.0
COPY DigitRecognizer/bin/Debug/netcoreapp2.0/publish /root/
WORKDIR /root
ENTRYPOINT ["dotnet", "DigitRecognizer.dll"]