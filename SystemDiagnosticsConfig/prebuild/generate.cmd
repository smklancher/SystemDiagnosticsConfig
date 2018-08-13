REM @echo off
del /F System.Diagnostics.SystemDiagnosticsSection.xsd
REM XSDExtractor.exe /C System.Diagnostics.SystemDiagnosticsSection /R SystemDiagnosticsConfig /A C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.dll /T http://www.w3.org/2001/XMLSchema /L TRUE /O .

REM manually extracted section of DotNetConfig.xsd
REM ..\..\packages\XmlSchemaClassGenerator.Console\tools\net45\XmlSchemaClassGenerator.Console.exe -o . -n "http://www.w3.org/2001/XMLSchema=SystemDiagnosticsConfig" SystemDiagnostics.xsd

XSDExtractor.exe /C System.Diagnostics.SystemDiagnosticsSection /R SystemDiagnosticsConfig /A C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.dll /L TRUE /O .
..\..\packages\XmlSchemaClassGenerator.Console\tools\net45\XmlSchemaClassGenerator.Console.exe -o . -n "http://System.Diagnostics.SystemDiagnosticsSection=SystemDiagnosticsConfig" System.Diagnostics.SystemDiagnosticsSection.xsd

SET ThisScriptsDirectory=%~dp0
SET PowerShellScriptPath=%ThisScriptsDirectory%postprocessing.ps1
PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& '%PowerShellScriptPath%'";
move /Y SystemDiagnosticsConfig.cs ..\SystemDiagnosticsConfig.Generated.cs
pause