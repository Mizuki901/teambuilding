Configuration UserConfigAdd
{
  Node localhost
  {
    User UserAdd
    {
        Ensure = "Present"  # To ensure the user account does not exist, set Ensure to "Absent"
        UserName = "dev01"
        Password = 201606gogoMS # This needs to be a credential object
    }
    {
        Ensure = "Present"  # To ensure the user account does not exist, set Ensure to "Absent"
        UserName = "ops01"
        Password = 201606gogoMS # This needs to be a credential object
    }
  }
}
UserConfigAdd -OutputPath .
Start-DscConfiguration .\UserConfigAdd -Wait -Verbose