# .NET Core Templates

## Overview

You can save yourself time creating and configuring projects by using one of these templates.

## Install Template

```bash
mkdir -p ~/templates
cd ~/templates
git clone git@github.com:appshapes-org/dotnet-templates.git
cd ./dotnet-templates
dotnet new -i DotNetTemplate.Service
```

Repeat final step for other templates.

## Use Template

```bash
cd ~/myprojects/solutionfolder
dotnet new dotnettemplate-service --name YourProjectName.Service
```

The new project is created and ready to use!
