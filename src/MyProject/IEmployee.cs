using System;
namespace MyProject
{
    public interface IEmployee
    {
        void AddRemuneration(double money);
        void AddRemuneration(double money, bool bonus = false);
        Statistics GetStatistics();
        event EventHandler<string> BonusAdded;
    } 

}