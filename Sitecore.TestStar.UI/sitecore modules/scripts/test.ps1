
# & 'C:\inetpub\wwwroot\testsite-sc-7.1\Website\bin\Sitecore.TestStar.TestLauncher.exe' '-w' 'Sitecore.TestStar.WebTests' 'Sitecore.TestStar.WebTests.Tests.IPTest' '{095865B2-2DAB-47B8-BF31-37264690E15D},{B5AAE915-A53F-4B17-A914-3AC7A5C4E059}' '' '{D3C3B8FB-3FE0-4362-AF30-A4DF685D4CE9},{DF898DEF-92A5-4F6E-AD5D-866E38CEF332}'
# & 'C:\inetpub\wwwroot\testsite-sc-7.1\Website\bin\Sitecore.TestStar.TestLauncher.exe' "-u" "Sitecore.SharedSource.DataImporter.Tests" "Provider Tests" ""

#UNIT TESTING#

$URI = 'http://teststar.local/sitecore%20modules/Web/teststar/service/testservice.asmx'
$proxy = New-WebServiceProxy -Uri $URI -Namespace System -Class string

#[System.Collections.ArrayList]$errList
$errList = New-Object System.Collections.ArrayList

$testCats = New-Object System.Collections.ArrayList
$length = $testCats.Add("Extension Tests");
$length = $testCats.Add("Provider Tests");
$length = $testCats.Add("Utility Tests");
$testTable = @{}
$testTable.Add("Sitecore.SharedSource.DataImporter.Tests", $testCats)

foreach ($t in $testTable.GetEnumerator()) {
  foreach ($c in $t.Value) {
    [json]$response = $proxy.RunUnitTests($t.Key, $c)
    $errs = $response | where {$_.Failed -eq $True}
    #Write-Output 'Error Length: ' $errs.Length 
    foreach($e in $errs){
      $a = $errList.Add($e.Message)
    }
  }
}
    
if($errList.Count -gt 0){
  Write-Output "There were " $errList.Count " error(s)"
  exit(1)  
} else {
  exit(0)
} 