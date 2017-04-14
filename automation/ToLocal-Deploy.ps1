$sitecoreRoot = "C:\Sitecore\Instances\status.v3"
$localDevTarget = "C:\Files\Coding\Projects\Sitecore.OperationsSuite"

$exclude = @('*.cs','*.csproj*','*web.config*')

Copy-Item -Path "$localDevTarget\Sitecore.OperationsSuite.Web\*" -Destination "$sitecoreRoot\Website\" -recurse -Force -Exclude $exclude