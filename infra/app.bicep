param environmentName string = 'environment-name'
param location string = resourceGroup().location
param appName string = 'devops22-firstName'


@secure()
param dbPassword string
param dbAdmin string = 'admin-name'
param sqlDbName string = 'db-name'
param sqlServerName string = 'sql-server-name'

var appFullName = 'app-${appName}-${toLower(environmentName)}'
var planName = 'plan-devops22'

var skuName = 'F1'
var skuCapacity = 1

resource appPlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  location: location
  name: planName
  kind: 'app'
  properties: {
    elasticScaleEnabled: false
    reserved: false
  }
  sku: {
    name: skuName
    capacity: skuCapacity
  }
}

resource app 'Microsoft.Web/sites@2022-03-01' = {
  kind: 'api'
  location: location
  name: appFullName
  properties: {
    enabled: true
    serverFarmId: appPlan.id
    reserved: false
    hostNamesDisabled: false
    httpsOnly: true
    siteConfig: {
      
      minTlsVersion: '1.2'
      http20Enabled: true
      ftpsState: 'Disabled'
      remoteDebuggingEnabled: false

      connectionStrings: [
        {
          name: 'OrdersConnectionString'
          connectionString: 'Data Source=tcp:${sqlServerName}.database.windows.net,1433;Initial Catalog=${sqlDbName};User Id=${dbAdmin};Password=${dbPassword}'
          type: 'SQLAzure'
        }
      ]
    }
  }
}

