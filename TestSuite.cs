using System;
using System.Collections.Generic;
using System.Linq;

namespace TDDUnit {
  class TestSuite {
    public TestSuite(Type type = null) {
      if (type != null) {
        var methodInfos = from methodInfo in type.GetMethods()
                          where methodInfo.Name.StartsWith("Test")
                          select methodInfo;
        foreach (var methodInfo in methodInfos)
          Add((TestCase)type.GetConstructor(new Type[] { typeof(string) }).Invoke(new string[] { methodInfo.Name }));
      }
    }

    public void Add(TestCase test) {
      m_tests.Add(test);
    }

    public void Run(TestResult result) {
      foreach (TestCase test in m_tests) test.Run(result);
    }

    public IEnumerable<string> FailedTests(TestResult result) {
      int oldErrorCount = result.ErrorCount;

      foreach (TestCase test in m_tests) {
        test.Run(result);
        if (result.ErrorCount == oldErrorCount + 1) {
          yield return test.Name;
        }
      }
    }

    private List<TestCase> m_tests = new List<TestCase>();
  }
}
