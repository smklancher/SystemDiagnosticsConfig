# Get the text of the file all at once (Raw ReadCount 0, to allow multiline replace)
(Get-Content SystemDiagnosticsConfig.cs -Raw -ReadCount 0) | 
# Remove namespace (possible option in XSDExtractor or XmlSchemaClassGenerator)
Foreach-Object {$_ -replace ", Namespace=`"http://System.Diagnostics.SystemDiagnosticsSection`"", ""} | 
# Set base class for all generated classes
Foreach-Object {$_ -replace " class (?<classname>\w+)", " class `${classname} : XmlSerializationElement"} |
# Correct root element name (possible wrong config in XSDExtractor?)
Foreach-Object {$_ -replace "XmlRootAttribute\(`"SystemDiagnosticsConfig`"\)", "XmlRootAttribute(`"system.diagnostics`")"} | 
# Ignore traceoutputoptions enum since it does not parse coma separated values correctly.  New method on partial class will handle it.
Foreach-Object {$_ -replace "\[System\.ComponentModel\.DefaultValueAttribute\(SystemDiagnosticsConfig\.ListenerElementCTTraceOutputOptions\.None\)\]\s*", ""} | 
Foreach-Object {$_ -replace "\[System\.Xml\.Serialization\.XmlAttributeAttribute\(`"traceOutputOptions`", Form=System\.Xml\.Schema\.XmlSchemaForm\.Unqualified\)\]", "[System.Xml.Serialization.XmlIgnoreAttribute()]"} | 
Set-Content SystemDiagnosticsConfig.cs
#write-host "Press any key to continue..."
#[void][System.Console]::ReadKey($true)