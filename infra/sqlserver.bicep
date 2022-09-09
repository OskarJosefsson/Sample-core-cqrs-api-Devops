param location string = resourceGroup().location


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
    name: 'MyMachine'
    properties: {
      startIpAddress: '158.174.144.216'
      endIpAddress: '158.174.144.216'
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
