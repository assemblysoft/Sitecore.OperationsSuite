# Sitecore Operations Suite

## Overview
Providing automatic on-demand way for customers to get relevant status of the vendor's services, underlying systems and cloud services health.

Key packages:
- Monitoring. Get automatic status of all technical components and combine it to reflect actual status of your business service 24/7
  - Basic Monitors (endpoint, static text)
  - Azure (any Micorsoft Azure entity like Storage, Cloud Service, App Service etc.)
- Maintenances. Keep your scheduled, on-going and urgent maintenances immediately communicated to your customers

## Local Environment Setup
Recommended version: Sitecore 8.2 Update-2.

To set-up local environment:
- Download sources (git clone)
- Go to ".\automation" folder and replace $sitecoreRoot value to the one pointing of your Sitecore instance locally (specify folder that is parent of "Website" and "Data")
- Build the project (NuGet should download all dependencies if Sitecore repo is in the path)
- Run ".\automation\ToLocal-DeployStaticFiles.ps1" (transfer serialized items and a few additional files)
- Go to Sitecore Desktop, restore serialized items to content tree via "Developer" tab in Content Editor
- Run ".\automation\ToLocal-Deploy.ps1" (regular binaries/MVC/etc transfer)
- Go to Sitecore Desktop, rebuild all indexes

That's it!
If you need to sync back some of the items changes to project folder (so Git can see them), use "ToRepo-SyncItems.ps1" script.

### F.A.Q.
For items syncing, Unicorn and TDS are not used to aovid unnecessary complexities on this stage of the project. Will be considered in future.
