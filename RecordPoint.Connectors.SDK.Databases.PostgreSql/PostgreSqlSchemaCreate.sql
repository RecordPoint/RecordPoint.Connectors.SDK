IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = '{SchemaName}')
BEGIN
EXEC('CREATE SCHEMA {SchemaName}')
END