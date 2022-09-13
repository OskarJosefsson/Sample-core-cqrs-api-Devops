param environmentName string = 'environment-name'
param location string = resourceGroup().location
param appName string = 'devops22-firstName'
param appPlanId string = 'appPlanId'
var functionAppName = 'func-${appName}-${toLower(environmentName)}'
var newAppName = replace(appName, '-', '')


var workspaceName = 'ws-devops22'
var appInsightFullName = 'appi-${appName}-${toLower(environmentName)}'
var storeageAccFullName = concat('st${newAppName}${toLower(environmentName)}')



resource workspace 'Microsoft.OperationalInsights/workspaces@2021-06-01' = {
  name: workspaceName
  location: location
}

resource ai 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightFullName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: workspace.id
  }

    tags: {

     'hidden-link:/subscriptions/${subscription().id}/resourceGroups/${resourceGroup().name}/providers/Microsoft.Web/sites/${functionAppName}': 'Resource'
  }

}






resource storageacc 'Microsoft.Storage/storageAccounts@2021-09-01' = {
  name: storeageAccFullName
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    minimumTlsVersion: 'TLS1_2'
    supportsHttpsTrafficOnly: true
  }
}

resource functionApp 'Microsoft.Web/sites@2020-06-01' = {

  name: functionAppName
  location: location
  kind: 'functionapp'

  
  
  properties: {
  
    httpsOnly: true
    
    serverFarmId: appPlanId
    clientAffinityEnabled: true


    
    siteConfig: {
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: ai.properties.InstrumentationKey
        }
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storageacc.name};EndpointSuffix=${environment().suffixes.storage};AccountKey=${listKeys(storageacc.id, storageacc.apiVersion).keys[0].value}'
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~3'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet'
        }
        {
          name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storageacc.name};EndpointSuffix=${environment().suffixes.storage};AccountKey=${listKeys(storageacc.id, storageacc.apiVersion).keys[0].value}'
        }
        // WEBSITE_CONTENTSHARE will also be auto-generated - https://docs.microsoft.com/en-us/azure/azure-functions/functions-app-settings#website_contentshare
        // WEBSITE_RUN_FROM_PACKAGE will be set to 1 by func azure functionapp publish
      ]
    }
  }


}


output instrumentationKey string = ai.properties.InstrumentationKey
