using System;

namespace TDDUnit {
  class TestCase {
    public TestCase(string name) {
      m_name = name;
    }

    public virtual void SetUp() {
    }

    public TestResult Run() {
      TestResult result = new TestResult();

      result.TestStarted();

      try {
        SetUp();
        GetType().GetMethod(m_name).Invoke(this, null);
      } catch (Exception) {
        result.TestFailed();
      }

      TearDown();
      return result;
    }

    public virtual void TearDown() {
    }

    private string m_name;
  }
}
