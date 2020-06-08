# Vulnerable Jwt Based Authentication Application
This is an example vulnerable authentication application created to support Security PS's Cyber Apprenticeship Program.
## Running the Code
1. Install ASP.NET Core 3.1 SDK
1. Open a terminal and cd into the project's working directory
1. Obtain access to an SMTP server (I used gmail for example)
    1. run ```dotnet user-secrets set "Email:Username" "<smtp username here>"```
    1. run ```dotnet user-secrets set "Email:Password" "<smtp password here"```
    1. run ```dotnet user-secrets set "Email:Host" "<your smtp host here, ex: smtp.gmail.com"```
    1. run ```dotnet user-secrets set "Email:Port" "587"```
    1. run ```dotnet user-secrets set "Email:From" "<your email address here, ex: myusername@gmail.com"```
    1. run ```dotnet user-secrets set "JwtKey" "a97d607c-2f75-459e-a070-e94fbdb12038"``` (generate your own GUID)
1. run ```dotnet run```
1. Open a browser and visit https://127.0.0.1:5001
1. Register a user account (with a unique username)
1. Login with a username and password
1. Check your email for an SMS one-time token
1. Enter your SMS one-time token