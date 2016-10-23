using System.Collections.Generic;

namespace Auto
{
    public interface IFindWordManager
    {
        void AddTest(ITest test);
        void AddTest(ITest test, int depth);
        List<Idea> Find(string word);
        void PrintAll();
    }
}