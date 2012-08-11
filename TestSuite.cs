using System.Collections.Generic;

namespace TDDUnit {
  class TestSuite {
    public void Add(TestCase test) {
      m_tests.Add(test);
    }

    public void Run(TestResult result) {
      foreach (TestCase test in m_tests) test.Run(result);
    }

    private List<TestCase> m_tests = new List<TestCase>();
  }
}
