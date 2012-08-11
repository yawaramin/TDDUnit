using System;

namespace TDDUnit {
  class TestTestCase : TestCase {
    public TestTestCase(string name) : base(name) {
    }

    public void TestTemplateMethod() {
      WasRunObj test = new WasRunObj("TestMethod");
      test.Run();
      Assert.That(test.Log == "SetUp TestMethod TearDown ");
    }

    public void TestResult() {
      WasRunObj test = new WasRunObj("TestMethod");
      TestResult result = test.Run();
      Assert.That("1 run, 0 failed" == result.Summary);
    }

    public void TestFailedResult() {
      WasRunObj test = new WasRunObj("TestBrokenMethod");
      TestResult result = test.Run();
      Assert.That("1 run, 1 failed" == result.Summary);
    }

    public void TestFailedResultFormatting() {
      TestResult result = new TestResult();
      result.TestStarted();
      result.TestFailed();
      Assert.That("1 run, 1 failed" == result.Summary);
    }
  }

  class Program {
    static void Main() {
      Console.WriteLine(new TestTestCase("TestTemplateMethod").Run().Summary);
      Console.WriteLine(new TestTestCase("TestResult").Run().Summary);
      Console.WriteLine(new TestTestCase("TestFailedResult").Run().Summary);
      Console.WriteLine(new TestTestCase("TestFailedResultFormatting").Run().Summary);
    }
  }
}
