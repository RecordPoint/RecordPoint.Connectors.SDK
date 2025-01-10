using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Test.SUT
{
    /// <summary>
    /// Context that extends the standard context with additional features for on premise connectors
    /// </summary>
    public class TestSystemContext : ISystemContext
    {
        public const string RECORDPOINT_COMPANY_NAME = "RecordPoint";
        public const string CONNECTOR_NAME = "Test Connector";
        public const string CONNECTOR_SHORT_NAME = "TstConn";
        public const string DATA_ROOT_PATH = "c:\temp";

        public string GetCompanyName() => RECORDPOINT_COMPANY_NAME;

        public string GetConnectorName() => CONNECTOR_NAME;

        public string GetDataRootPath() => DATA_ROOT_PATH;

        public string GetShortName() => CONNECTOR_SHORT_NAME;
    }

}
