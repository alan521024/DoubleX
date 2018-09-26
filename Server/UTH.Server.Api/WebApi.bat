@echo off  
::start cmd /k "cd/d E:\Project\UTH\Server\UTH.Server.Api &&dotnet run -batchmode &&taskkill /f /t /im cmd.exe"
set b=%cd%
start cmd /k "dotnet run &&taskkill /f /t /im cmd.exe"