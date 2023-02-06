using System;

namespace RecordPoint.Connectors.SDK.Test.Common
{
    public class TestException : Exception
    {
        private const string EXCEPTION_MESSAGE = "This is a test exception";

        public TestException() : base(EXCEPTION_MESSAGE)
        {

        }
    }
}
