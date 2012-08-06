using System;

namespace TDDUnit {
  class TestTestCase : TestCase {
    public TestTestCase(string name) : base(name) {
    }

    public override void SetUp() {
      base.SetUp();

      m_test = new WasRunObj("TestMethod");
    }

    public void TestSetUp() {
      m_test.Run();
      Assert.That(m_test.WasSetUp, "Expected test to have been set up");
    }

    public void TestRunning() {
      m_test.Run();
      Assert.That(m_test.WasRun, "Expected test to run");
    }

    private WasRunObj m_test;
  }

  class Program {
    static void Main() {
      new TestTestCase("TestRunning").Run();
    }
  }
}
