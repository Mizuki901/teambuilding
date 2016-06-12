Configuration SetupedIISConfig
{
    Param
    (
	    [Parameter(Mandatory = $true)]
	    [string[]] $Nodes
    )
    Import-DscResource -Module xWebAdministration

    Node $Nodes
    {
        WindowsFeature Installed-IIS
        {
            Name = "Web-Server"
            Ensure = "Present"
            IncludeAllSubFeature = $true
        }
        Windowsfeature Installed-AspNet45
        {
            Name = "Web-Asp-Net45"
            Ensure = "Present"
        }
        xWebsite TestWebSite
        {
            Name = "TestWebSite"
            PhysicalPath = "C:inetpubwwwroot"
            State = "Started"
            BindingInfo = MSFT_xWebBindingInformation
                        {
                            Protocol = "HTTP"
                            Port = 8080
                        }
            Ensure = "Present"
            DependsOn = "[WindowsFeature]Installed-IIS"
        }
    }
}

SetupedIISConfig -OutputPath . -Nodes ("localhost")
Start-DscConfiguration -Path .SetupedIISConfig -Wait -Verbose