#UNIT TESTING#

$testTable = @{}

$testCats = New-Object System.Collections.ArrayList
$length = $testCats.Add("Extension Tests");
$length = $testCats.Add("Provider Tests");
$length = $testCats.Add("Utility Tests");

$testTable.Add("Sitecore.SharedSource.DataImporter.Tests", $testCats)

$URI = 'http://teststar.local'

$URI += '/sitecore%20modules/Web/teststar/service/testservice.asmx'
$proxy = New-WebServiceProxy -Uri $URI -Namespace System -Class string
$errList = New-Object System.Collections.ArrayList

foreach ($t in $testTable.GetEnumerator()) {
  foreach ($c in $t.Value) {
    [json]$response = $proxy.RunUnitTests($t.Key, $c)
    $errs = $response | where {$_.Failed -eq $True}
    foreach($e in $errs){
      $a = $errList.Add($e.Message)
    }
  }
}
    
if($errList.Count -gt 0){
  $out = "There were " + $errList.Count + " error(s)"
  Write-Output $out
  exit(1)  
} else {
  exit(0)
} 