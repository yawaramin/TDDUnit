using System;
using System.Collections.Generic;
using System.Linq;

namespace TDDUnit {
  class Suite {
    public Suite(Type type = null) {
      if (type != null) {
        var methodInfos = from methodInfo in type.GetMethods()
                          where methodInfo.Name.StartsWith("Test")
                          select methodInfo;
        foreach (var methodInfo in methodInfos)
          Add((Case)type.GetConstructor(new Type[] { typeof(string) }).Invoke(new string[] { methodInfo.Name }));
      }
    }

    public void Add(Case test) {
      m_tests.Add(test);
    }

    public void Run(Result result) {
      foreach (Case test in m_tests) test.Run(result);
    }

    public IEnumerable<string> FailedTests(Result result) {
      int oldErrorCount;

      foreach (Case test in m_tests) {
        oldErrorCount = result.ErrorCount;
        test.Run(result);
        if (result.ErrorCount == oldErrorCount + 1) {
          yield return test.Name;
        }
      }
    }

    private List<Case> m_tests = new List<Case>();
  }
}
