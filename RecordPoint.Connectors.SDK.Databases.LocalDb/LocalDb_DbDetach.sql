alter database {DatabaseName} set single_user with rollback immediate;
EXEC sp_detach_db '{DatabaseName}'
