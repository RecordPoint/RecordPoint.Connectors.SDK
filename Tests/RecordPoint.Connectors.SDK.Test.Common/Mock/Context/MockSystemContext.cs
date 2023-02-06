using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Test.Mock.Context
{
    public class MockSystemContext : ISystemContext
    {

        public const string TEST_COMPANY_NAME = "Test";
        public const string TEST_CONNECTOR_NAME = "Test";
        public const string TEST_SHORT_NAME = "Test";

        private readonly IOptions<MockSystemOptions> _systemOptions;

        public MockSystemContext(IOptions<MockSystemOptions> systemOptions)
        {
            _systemOptions = systemOptions;
        }

        public string GetCompanyName()
        {
            return TEST_COMPANY_NAME;
        }

        public string GetConnectorName()
        {
            return TEST_CONNECTOR_NAME;
        }

        public string GetDataRootPath()
        {
            throw new System.NotImplementedException();
        }

        public string GetShortName()
        {
            return TEST_SHORT_NAME;
        }

        public string GetSystemName()
        {
            return _systemOptions.Value.SUTName;
       }

    }
}
