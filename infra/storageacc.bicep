param location string = resourceGroup().location
param appName string = 'devops22-firstName'
param environmentName string = 'environment-name'

var newAppName = replace(appName, '-', '')

var storeageAccFullName = concat('st${newAppName}${toLower(environmentName)}')

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

