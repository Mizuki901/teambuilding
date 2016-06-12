Configuration WebSiteConfigInstall
{
  Node localhost
  {
    WindowsFeature IIS
    {
      Ensure               = "Present"
      Name                 = "Web-Server"
    }
 
  }
}
 
WebSiteConfigInstall -OutputPath .
Start-DscConfiguration .\WebSiteConfigInstall -Wait -Verbose