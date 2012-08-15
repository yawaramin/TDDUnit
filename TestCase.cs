using System;

namespace TDDUnit {
  class TestCase {
    public TestCase(string name) {
      m_name = name;
    }

    public virtual void SetUp() {
    }

    public void Run(TestResult result) {
      result.TestStarted();

      try {
        SetUp();
      } catch (Exception) {
        return;
      }
      
      try {
        GetType().GetMethod(m_name).Invoke(this, null);
      } catch (Exception) {
        result.TestFailed();
      } finally {
        TearDown();
      }
    }

    public virtual void TearDown() {
    }

    private string m_name;
  }
}
