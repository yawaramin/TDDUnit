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

    private List<TestCase> m_tests = new List<TestCase>();
  }
}
