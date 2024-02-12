using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUnitTests.Common
{
    public class TestApiBase:IDisposable
    {
        public readonly AppDataContext Context;

        public TestApiBase()
        {
            Context= AppDataContextFactory.Create();
        }
        public void Dispose()
        {
            AppDataContextFactory.Destroy(Context);
        }
    }
}
