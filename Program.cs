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
  }

  class Program {
    static void Main() {
      new TestTestCase("TestTemplateMethod").Run();
    }
  }
}
