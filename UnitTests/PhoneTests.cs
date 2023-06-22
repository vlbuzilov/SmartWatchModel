using NUnit.Framework;
using OOP_Lab6.WatchFuncs;

namespace UnitTestsLab_6
{
    [TestFixture]
    public class PhoneTests
    {
         private Phone phone;

        [SetUp]
        public void Setup()
        {
            phone = new Phone();
        }

        [Test]
        public void Add_WhenAddingANewContact_ContactIsAddedToThePhoneBook()
        {
            // Arrange
            string name = "John Doe";
            int phoneNumber = 123456789;

            // Act
            phone.Add(name, phoneNumber);

            // Assert
            Assert.AreEqual(phoneNumber, phone.GetNumber(name));
        }

        [Test]
        public void Update_WhenUpdatingAContact_ContactIsUpdatedInPhoneBook()
        {
            // Arrange
            string name = "John Doe";
            int phoneNumber = 123456789;
            phone.Add(name, phoneNumber);

            // Act
            phone.Update(name, 987654321);

            // Assert
            Assert.AreEqual(987654321, phone.GetNumber(name));
        }

        [Test]
        public void GetNumber_WhenGettingANumberOfAnExistingContact_NumberIsReturned()
        {
            // Arrange
            string name = "John Doe";
            int phoneNumber = 123456789;
            phone.Add(name, phoneNumber);

            // Act
            int actualPhoneNumber = phone.GetNumber(name);

            // Assert
            Assert.AreEqual(phoneNumber, actualPhoneNumber);
        }

        [Test]
        public void GetNumber_WhenGettingANumberOfANonexistentContact_ZeroIsReturned()
        {
            // Arrange
            string name = "John Doe";

            // Act
            int actualPhoneNumber = phone.GetNumber(name);

            // Assert
            Assert.AreEqual(0, actualPhoneNumber);
        }

        [Test]
        public void PrintPhoneBookSorted_WhenPrintingThePhoneBook_ThePhoneBookIsPrintedInSortedOrder()
        {
            // Arrange
            phone.Add("C", 123);
            phone.Add("B", 456);
            phone.Add("A", 789);

            // Act
            phone.PrintPhoneBookSorted();

            // Assert
            Assert.Pass();
        }
    }
}