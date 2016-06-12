Configuration UserConfigAdd
{
  Node localhost
  {
    User UserAdd
    {
        Ensure = "Present"  # To ensure the user account does not exist, set Ensure to "Absent"
        UserName = "dev01"
        Password = 201606gogoMS # This needs to be a credential object
        DependsOn = “[Group]GroupExample" # Configures GroupExample first
    }
    {
        Ensure = "Present"  # To ensure the user account does not exist, set Ensure to "Absent"
        UserName = "ops01"
        Password = 201606gogoMS # This needs to be a credential object
        DependsOn = “[Group]GroupExample" # Configures GroupExample first
    }
  }
}
UserConfigAdd -OutputPath .
Start-DscConfiguration .\UserConfigAdd -Wait -Verbose