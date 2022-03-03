using Xunit;
namespace MyProject.Tests
{
    public class EmployeeTest
    {
        [Fact]
        public void Test1()
        {
            //arrange
            InMemoryEmployee employee1 = new InMemoryEmployee("Paweł", "Gaweł");
            employee1.AddRemuneration(1960.50);
            employee1.AddRemuneration(2560.00);
            employee1.AddRemuneration(2248.67);
            //act
            var result = employee1.GetStatistics();
            //assert
            Assert.Equal(2256.39, result.Average);
            Assert.Equal(2560.00, result.Hight);
            Assert.Equal(1960.50, result.Low);
        }
    }
}
