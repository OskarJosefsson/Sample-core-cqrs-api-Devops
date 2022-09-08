param location string = resourceGroup().location
param costing string = 'bill-payer'

@secure()
param dbPassword string
param dbAdmin string = 'admin-name'
param sqlDbName string = 'database-name'
param sqlServerName string = 'sql-server-name'

resource sqlServer 'Microsoft.Sql/servers@2021-08-01-preview' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: dbAdmin
    administratorLoginPassword: dbPassword
  }

  resource fwRuleAzureComs 'firewallRules@2022-02-01-preview' = {
    name: 'AllowAllWindowsAzureIps'
    properties: {
      startIpAddress: '0.0.0.0'
      endIpAddress: '0.0.0.0'
    }
  }

  resource fwRuleHome 'firewallRules@2022-02-01-preview' = {
    name: 'MySpace'
    properties: {
      startIpAddress: '158.174.49.248'
      endIpAddress: '158.174.49.248'
    }
  }

  resource db 'databases@2021-08-01-preview' = {
    name: sqlDbName
    location: location
    sku: {
      name: 'Basic'
      tier: 'Basic'
    }
  }
}
