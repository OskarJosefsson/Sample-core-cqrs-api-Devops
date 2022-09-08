param location string = resourceGroup().location
param costing string = 'bill-payer'

@secure()
param dbPassword string
param dbAdmin string = 'admin-name'
param sqlDbName string = 'database-name'
param sqlServerName string = 'sql-server-name'

