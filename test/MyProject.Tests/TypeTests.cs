using Xunit;
using System;
using MyProject;

namespace MyProject
{
    public class TypeTests
    {
        [Fact]
        public void GetEmployReturnsObjects()
        {
            var emp1 = GetEmployee("Adam", "Adamiak");
            var emp2 = GetEmployee("Bartek", "Kowalski");
            Assert.False(object.ReferenceEquals(emp1, emp2));
        }
        [Fact]
        public void CanSetNameFromRefernce()
        {
            var emp1 = GetEmployee("Adam", "Adamiak");
            this.SetName(emp1, "newName", "newSurname");
            Assert.Equal("newName newSurname", emp1.fullName);
        }
        [Fact]
        public void CanPassByRef()
        {
            InMemoryEmployee emp1;
            GetEmployeeSetName(out emp1, "New", "new");
            Assert.Equal("New new", emp1.fullName);
        }
        [Fact]
        public void CheckMoney()
        {
            Assert.Equal(AddMoney("2500"), true);
        }
        public bool AddMoney(string money)
        {
            int addMoney;
            bool check = int.TryParse(money, out addMoney);
            return check;
        }
        public void GetEmployeeSetName(out InMemoryEmployee emp, string name, string surname)
        {
            emp = new InMemoryEmployee(name, surname);
        }
        private InMemoryEmployee GetEmployee(string name, string surname)
        {
            return new InMemoryEmployee(name, surname);
        }
        private void SetName(InMemoryEmployee employee, string name, string surname)
        {
            employee.fullName = name + " " + surname;
        }
    }
}
