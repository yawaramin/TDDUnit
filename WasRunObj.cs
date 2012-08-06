using System;

namespace TDDUnit {
  class TestRunException : Exception {
    public TestRunException(string message) {
      m_message = message;
    }

    public override string Message {
      get {
        return m_message;
      }
    }

    private string m_message;
  }

  class WasRunObj : TestCase {
    public WasRunObj(string name) : base(name) {
    }

    public bool WasRun {
      get {
        return m_wasRun;
      }
    }

    public bool WasSetUp {
      get {
        return m_wasSetUp;
      }
    }

    public override void SetUp() {
      m_wasSetUp = true;
    }

    public void TestMethod() {
      m_wasRun = true;
    }

    private bool m_wasRun = false;
    private bool m_wasSetUp = false;
  }
}
