using System;
using System.Linq;
using System.Reflection;

namespace TDDUnit {
  class Runner {
    public Runner(Type callingType) {
      Suite suite = new Suite();
      m_result = new Result();
      Type[] forbiddenTypes = new Type[] {
        callingType
      , typeof (TDDUnit.WasRunObj)
      , typeof (TDDUnit.WasRunSetUpFailed)
      };

      foreach (Type t in Assembly.GetEntryAssembly().GetTypes()) {
        if (t.IsSubclassOf(typeof (TDDUnit.Case)) && !forbiddenTypes.Contains(t))
          suite.Add(t);
      }

      foreach (string test in suite.FailedTests(m_result)) {
        Console.WriteLine("Failed: " + test);
      }
      Console.WriteLine(m_result.Summary);
    }

    public string Summary {
      get {
        return m_result.Summary;
      }
    }

    private Result m_result;
  }
}
