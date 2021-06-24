# TestDeamonApp

This is a console app demonstration how to aquire an access token from a Microsoft AzureAD B2C Tenant, and how to used it when calling a protected NVE API, 
in this case the Regoba API.

## Prerequisites

You will need to register a Regobs user account. All you have to do, is to login on Regobs and set your nickname. 
Login on the test environment here: https://test.regobs.no/

## Run

Edit the appsettings.json file and hit run. It is preconfigured to access the test tenant, and the test environment of the Regobs API. 
It will call the GetObserver endpoint and if successful, you will get your Regobs user info as a result.