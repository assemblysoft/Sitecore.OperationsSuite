$sitecoreRoot = "C:\Sitecore\Instances\status.v3"
$localDevTarget = "C:\Files\Coding\Projects\Sitecore.OperationsSuite"

Remove-Item "$localDevTarget\Sitecore.OperationsSuite.Web\SitecoreMeta\Data\*" -recurse
Copy-Item -Path "$sitecoreRoot\Data\serialization\" -Destination "$localDevTarget\Sitecore.OperationsSuite.Web\SitecoreMeta\Data" -recurse -Force