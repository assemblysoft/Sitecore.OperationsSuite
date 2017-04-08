$sitecoreRoot = "C:\Sitecore\Instances\status.v3"
$localDevTarget = "C:\Files\Coding\Projects\Sitecore.OperationsSuite"

Copy-Item -Path "$sitecoreRoot\Data\serialization\" -Destination "$localDevTarget\Sitecore.OperationsSuite.Web\SitecoreMeta\Data" -recurse -Force