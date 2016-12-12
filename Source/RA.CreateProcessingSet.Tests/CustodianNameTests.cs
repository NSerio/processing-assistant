using RA.CreateProcessingSet.Helpers;
using RA.CreateProcessingSet.Models;
using NUnit.Framework;

namespace RA.CreateProcessingSet.Tests
{
    [TestFixture]
    public class CustodianNameTests
    {
        [Test]
        public void TestNameSeparationByUnderscore(){
            string fullInput = "Javier_Gonzalez";
            var result = NameSeparator.Separate(fullInput, DestinationEnum.Custodian);
            Assert.AreEqual("Javier", result.FirstName);
            Assert.AreEqual("Gonzalez", result.LastName);
            Assert.AreEqual("Gonzalez, Javier", result.FullName);
        }

        [Test]
        public void TestNameSeparationNoSeparator(){
            string fullInput = "Javier";
            var result = NameSeparator.Separate(fullInput, DestinationEnum.Custodian);
            Assert.AreEqual("Javier", result.FirstName);
            Assert.IsNull(result.LastName);
            Assert.AreEqual("Javier", result.FullName);
        }

        [Test]
        public void TestNameSeparationByComma(){
            string fullInput = "Gonzalez Velandia, Javier";
            var result = NameSeparator.Separate(fullInput, DestinationEnum.Custodian);
            Assert.AreEqual("Javier", result.FirstName);
            Assert.AreEqual("Gonzalez Velandia", result.LastName);
            Assert.AreEqual("Gonzalez Velandia, Javier", result.FullName);
        }
        
        [Test]
        public void TestNameSeparationByCommaMultipleSpaces(){
            string fullInput = "Gonzalez Velandia ,  Javier ";
            var result = NameSeparator.Separate(fullInput, DestinationEnum.Custodian);
            Assert.AreEqual("Javier", result.FirstName);
            Assert.AreEqual("Gonzalez Velandia", result.LastName);
            Assert.AreEqual("Gonzalez Velandia, Javier", result.FullName);
        }

        [Test]
        public void TestNameSeparationBySpace(){
            string fullInput = "Javier Gonzalez";
            var result = NameSeparator.Separate(fullInput, DestinationEnum.Custodian);
            Assert.AreEqual(result.FirstName, "Javier");
            Assert.AreEqual(result.LastName, "Gonzalez");
            Assert.AreEqual(result.FullName, "Gonzalez, Javier");
        }

        [Test]
        public void TestNameSeparationSingleNameProject(){
            string fullInput = "Firestone";
            var result = NameSeparator.Separate(fullInput, DestinationEnum.Project);
            Assert.AreEqual(result.FullName, "Firestone");
            Assert.AreEqual(result.FirstName, "Firestone");
            Assert.IsNull(result.LastName);
        }

        [Test]
        public void TestNameSeparationByCommaMultipleNames(){
            string fullInput = "Jones,  Mary Beth";
            var result = NameSeparator.Separate(fullInput, DestinationEnum.Custodian);
            Assert.AreEqual("Mary Beth", result.FirstName);
            Assert.AreEqual("Jones", result.LastName);
            Assert.AreEqual("Jones, Mary Beth", result.FullName);
        }

        [Test]
        public void TestNameSeparationByCommaMultipleNames2(){
            string fullInput = "Jones,  Mary Beth Gomez";
            var result = NameSeparator.Separate(fullInput, DestinationEnum.Custodian);
            Assert.AreEqual("Mary Beth Gomez", result.FirstName);
            Assert.AreEqual("Jones", result.LastName);
            Assert.AreEqual("Jones, Mary Beth Gomez", result.FullName);
        }

    }
}
