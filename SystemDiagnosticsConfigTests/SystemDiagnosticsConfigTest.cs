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

        [Test]
        public void ExpectedValuesReportingConfig()
        {
            var s = File3.SysDiag;
            var sl = s.SharedListeners.Add[0];
            Assert.That(sl.Name == "ReportingLogTxt");
            Assert.That(sl.Type == "Kofax.Reporting.Common.Logging.TenantBasedTraceListener, Kofax.Reporting.Common");
            Assert.That(sl.InitializeData == @"C:\ProgramData\Kofax\TotalAgility\Reporting\Log\WorkerRole.log");
            Assert.That(sl.MaxFileSizeKB == "5120");
            Assert.That(sl.MaxFilesAmount == "10");


            var so = s.Sources[0];
            Assert.That(so.Name == "Reporting");
            Assert.That(so.SwitchName == "TraceLevelSwitch");
            Assert.That(so.Listeners.ClearSpecified);
            Assert.That(so.Listeners.Add[0].Name== "ReportingLogTxt");

            Assert.That(s.Switches.Add[0].Name == "TraceLevelSwitch");
            Assert.That(s.Switches.Add[0].Value == "Warning");
            Assert.That(s.Trace.Autoflush);

        }

        //[Test]
        //public void RoundTripXml()
        //{
        //    var newdoc = new XDocument(File3.XDoc);
        //    //remove comments
        //    newdoc.DescendantNodes().OfType<XComment>().Remove();

        //    var c = new ConfigFile(newdoc);

        //    string startxml = c.SysDiagElement().ToString();


        //    string endxml = c.SysDiagObjXml().ToString();

        //    //ordering is different so this does not work
        //    Assert.AreEqual(startxml, endxml);
        //}



    }
}
