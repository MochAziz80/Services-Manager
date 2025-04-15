# Service Manager

**Service Manager** is a simple Windows Forms application built with .NET to help you manage Windows services based on `.exe` files.

## Features

- ✅ Install a new service from an `.exe` file
- ✅ Optional auto-start after service installation
- ✅ Customizable service name prefix
- ✅ Delete services associated with a specific `.exe` file

## How It Works

1. **Select an `.exe` file**  
   Use the **Browse** button to choose the executable you want to install as a service.

2. **Set a service name**  
   Enter a name for your service. Optionally, specify a prefix (e.g., `MyCompany_`) that will be prepended to the service name.

3. **Install the service**  
   Click **Install** to create the service. If **Auto Start** is checked, the service will start immediately after installation.

4. **Delete a service**  
   Click **Delete** to remove the service that is associated with the selected `.exe` file.

## Requirements

- Windows OS
- Administrator privileges (required to create/delete services)
- .NET Desktop Runtime

## Build & Run

```bash
git clone https://github.com/MochAziz80/Services-Manager.git
cd Services-Manager
dotnet build
dotnet run
