using System;

namespace TDDUnit {
  class TestTestCase : TestCase {
    public TestTestCase(string name) : base(name) {
    }

    public void TestSetUp() {
      WasRunObj test = new WasRunObj("TestMethod");
      test.Run();
      Assert.That(test.WasSetUp, "Expected test to have been set up");
    }

    public void TestRunning() {
      WasRunObj test = new WasRunObj("TestMethod");
      Assert.That(!test.WasRun, "Expected test to not run");
      test.Run();
      Assert.That(test.WasRun, "Expected test to run");
    }
  }

  class Program {
    static void Main() {
      new TestTestCase("TestRunning").Run();
    }
  }
}
