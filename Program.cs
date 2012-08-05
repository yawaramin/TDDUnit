using System;

namespace TDDUnit {
  class TestTestCase : TestCase {
    public TestTestCase(string name) : base(name) {
    }

    public void TestRunning() {
      WasRunObj test = new WasRunObj("TestMethod");
      if (test.WasRun) throw new TestRunException("Expected test to not run");
      test.Run();
      if (!test.WasRun) throw new TestRunException("Expected test to run");
    }
  }

  class Program {
    static void Main() {
      new TestTestCase("TestRunning").Run();
    }
  }
}
