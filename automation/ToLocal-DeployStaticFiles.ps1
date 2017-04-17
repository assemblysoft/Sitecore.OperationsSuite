$sitecoreRoot = "C:\Sitecore\Instances\status.v3"
$localDevTarget = "C:\Files\Coding\Projects\Sitecore.OperationsSuite"

Copy-Item -Path "$localDevTarget\Sitecore.OperationsSuite.Web\SitecoreMeta\*" -Destination "$sitecoreRoot\" -recurse