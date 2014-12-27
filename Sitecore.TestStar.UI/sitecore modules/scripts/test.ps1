
# & 'C:\inetpub\wwwroot\testsite-sc-7.1\Website\bin\Sitecore.TestStar.TestLauncher.exe' '-w' 'Sitecore.TestStar.WebTests' 'Sitecore.TestStar.WebTests.Tests.IPTest' '{095865B2-2DAB-47B8-BF31-37264690E15D},{B5AAE915-A53F-4B17-A914-3AC7A5C4E059}' '' '{D3C3B8FB-3FE0-4362-AF30-A4DF685D4CE9},{DF898DEF-92A5-4F6E-AD5D-866E38CEF332}'
# & 'C:\inetpub\wwwroot\testsite-sc-7.1\Website\bin\Sitecore.TestStar.TestLauncher.exe' "-u" "Sitecore.SharedSource.DataImporter.Tests" "Provider Tests" ""

$URI = 'http://teststar.local/sitecore%20modules/Web/teststar/service/testservice.asmx'
$a = New-WebServiceProxy -Uri $URI -Namespace System -Class string
[json]$response = $a.RunUnitTests("Sitecore.SharedSource.DataImporter.Tests", "Extension Tests")
Write-Output $response
#$ErrorMessage = "";

#[json]$response = $a.CreateUnitTestScript("Sitecore.SharedSource.DataImporter.Tests","Extension Tests")
#this is only on the genscript call. There's no success or message on the regular ones
#$ErrorMessage = $response.Message 
#if ($response.Success) {
#  exit(0)
#} else {
#  # Write-Output $ErrorMessage
#  exit(1)
#} 

