using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SystemDiagnosticsConfig;

namespace SystemDiagnosticsConfig.Test
{
    [TestFixture]
    public class SystemDiagnosticsConfigTest
    {
        ConfigFile File1;
        ConfigFile File2;
        ConfigFile File3;



        [OneTimeSetUp]
        public void Init()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("SystemDiagnosticsConfig.Test.TestConfigs.Test1.config"))
            {
                File1 = new ConfigFile(XDocument.Load(stream));
            }
            using (var stream = assembly.GetManifestResourceStream("SystemDiagnosticsConfig.Test.TestConfigs.Test2.config"))
            {
                File2 = new ConfigFile(XDocument.Load(stream));
            }
            using (var stream = assembly.GetManifestResourceStream("SystemDiagnosticsConfig.Test.TestConfigs.Test3.config"))
            {
                File3 = new ConfigFile(XDocument.Load(stream));
            }

        }


        [Test]
        public void TestFilesAvailable()
        {
            Assert.IsNotNull(File1);
            Assert.IsNotNull(File2);
            Assert.IsNotNull(File3);
        }

        [Test]
        public void DeserializeTestFiles()
        {
            var sd1 = File1.SysDiag;
            //var sd2 = File2.SysDiag;
            var sd3 = File3.SysDiag;


            Assert.IsNotNull(sd1);
            //Assert.IsNotNull(sd2);
            Assert.IsNotNull(sd3);
        }



    }
}
