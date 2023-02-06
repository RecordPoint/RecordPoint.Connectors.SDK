IF NOT EXISTS (
        SELECT *
        FROM sys.databases
        WHERE name = '{DatabaseName}'
        )
BEGIN
    CREATE DATABASE
        [{DatabaseName}]
    ON PRIMARY (
        NAME={DatabaseName}_data,
        FILENAME = '{DatabasePath}'
    )
    LOG ON (
        NAME={DatabaseName}_log,
        FILENAME = '{DatabaseLogFilePath}'
    )
    FOR ATTACH;
END
