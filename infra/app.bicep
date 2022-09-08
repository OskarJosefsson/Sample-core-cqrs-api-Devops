param environmentName string = 'environment-name'
param location string = resourceGroup().location
param appName string = 'devops22-firstName'


@secure()
param dbPassword string
param dbAdmin string = 'oskarsSql'
param sqlDbName string = 'db-name'
param sqlServerName string = 'sql-server-name'

var appFullName = 'app-${appName}-${toLower(environmentName)}'
var planName = 'plan-devops22'

var skuName = 'F1'
var skuCapacity = 1
var appIds = [0,1]

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

resource app 'Microsoft.Web/sites@2022-03-01' = [for id in appIds: {
  kind: 'api'

  location: location
  name: bool(id == 0) ? '${appFullName}' : '${appFullName}-${id}'
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
      appSettings: [
        {
          name: 'EmailFrom'
          value: 'iac@nackademin.se'
        }
      ]
      connectionStrings: [
        {
          name: 'OrdersConnectionString'
          connectionString: 'Data Source=tcp:${sqlServerName}.database.windows.net,1433;Initial Catalog=${sqlDbName};User Id=${dbAdmin};Password=${dbPassword}'
          type: 'SQLAzure'
        }
      ]
    }
  }
}]
