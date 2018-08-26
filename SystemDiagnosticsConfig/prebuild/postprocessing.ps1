(Get-Content SystemDiagnosticsConfig.cs) | 
Foreach-Object {$_ -replace ", Namespace=`"http://System.Diagnostics.SystemDiagnosticsSection`"", ""} | 
Foreach-Object {$_ -replace " class (?<classname>\w+)", " class `${classname} : XmlSerializationElement"} |
Foreach-Object {$_ -replace "XmlRootAttribute\(`"SystemDiagnosticsConfig`"\)", "XmlRootAttribute(`"system.diagnostics`")"} | 
Set-Content SystemDiagnosticsConfig.cs
#write-host "Press any key to continue..."
#[void][System.Console]::ReadKey($true)