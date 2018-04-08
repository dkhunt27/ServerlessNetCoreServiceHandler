# Serverless Net Core Service Handler

[![serverless](https://dl.dropboxusercontent.com/s/d6opqwym91k0roz/serverless_badge_v3.svg)](http://www.serverless.com)

This is a proof of concept for building services using .Net Core 2.0 and the Serverless Framework with Dependency Injection through AutoFac.  This is a foundation for a service.  The goal is to use shared libraries (Nuget packages) as our services we inject to our handlers.  Each Handlers project would control a specific domain of an API.

## Getting Started

Make sure you have the [Serverless Framework](http://www.serverless.com) installed.
```
npm install serverless -g

or

yarn global add serverless 
```

Install .Net Core 2.0. 
[https://www.microsoft.com/net/download]


## Build

Run the following command in powershell to build the project.
```
build.ps1
```

Use the following command for bash.
```
./build.sh
```

## Test
To run unit tests (from solution)
```
dotnet test \Tests
```

## Deploy


### To deploy from the command line
Use the following command to deploy from the command line tool
```
serverless deploy
```

#### Expected serverless output
```
λ  serverless deploy                                                                  
Serverless: Packaging service...                                                      
Serverless: Uploading CloudFormation file to S3...                                    
Serverless: Uploading artifacts...                                                    
Serverless: Validating template...                                                    
Serverless: Updating Stack...                                                         
Serverless: Checking Stack update progress...                                         
....................                                                                  
Serverless: Stack update finished...                                                  
Service Information                                                                   
service: ServerlessNetCoreServiceHandler                                            
stage: v1                                                                             
region: us-east-2                                                              
stack: ServerlessNetCoreServiceHandler-v1                                    
```
