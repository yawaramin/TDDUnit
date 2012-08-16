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

  class WasRunObj : Case {
    public WasRunObj(string name) : base(name) {
    }

    public bool WasRun {
      get {
        return m_wasRun;
      }
    }

    public string Log {
      get {
        return m_log;
      }
    }

    public override void SetUp() {
      m_wasRun = false;
      m_log = "SetUp ";
    }

    public void TestMethod() {
      m_wasRun = true;
      m_log += "TestMethod ";
    }

    public void TestBrokenMethod() {
      m_log += "TestBrokenMethod ";
      throw new TestRunException("This test is meant to fail");
    }

    public override void TearDown() {
      m_log += "TearDown ";
    }

    private bool m_wasRun;
    private string m_log;
  }
}
