# Vulnerable Jwt Based Authentication Application
## Running the Code
1. Install ASP.NET Core 3.1 SDK
1. Open a terminal and cd into the project's working directory
1. Obtain access to an SMTP server (I used gmail for example)
    1. run ```dotnet user-secrets set "Email:Username" "<smtp username here>"```
    1. run ```dotnet user-secrets set "Email:Password" "<smtp password here"```
    1. run ```dotnet user-secrets set "Email:Host" "<your smtp host here, ex: smtp.gmail.com"```
    1. run ```dotnet user-secrets set "Email:Port" "587"```
    1. run ```dotnet user-secrets set "Email:From" "<your email address here, ex: myusername@gmail.com"```
1. run ```dotnet run```
1. Open a browser and visit https://127.0.0.1:50001
1. Register a user account (not yet implemented in the UI)
1. Login with a username and password