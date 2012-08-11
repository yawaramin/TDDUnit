using System;

namespace TDDUnit {
  class TestTestCase : TestCase {
    public TestTestCase(string name) : base(name) {
    }

    public override void SetUp() {
      base.SetUp();

      m_result = new TestResult();
    }

    public void TestTemplateMethod() {
      WasRunObj test = new WasRunObj("TestMethod");
      test.Run(m_result);
      Assert.Equal("SetUp TestMethod TearDown ", test.Log);
    }

    public void TestResult() {
      WasRunObj test = new WasRunObj("TestMethod");
      test.Run(m_result);
      Assert.Equal("1 run, 0 failed", m_result.Summary);
    }

    public void TestFailedResult() {
      WasRunObj test = new WasRunObj("TestBrokenMethod");
      test.Run(m_result);
      Assert.Equal("1 run, 1 failed", m_result.Summary);
    }

    public void TestFailedResultFormatting() {
      m_result.TestStarted();
      m_result.TestFailed();
      Assert.Equal("1 run, 1 failed", m_result.Summary);
    }

    public void TestFailedSetUp() {
      WasRunSetUpFailed test = new WasRunSetUpFailed("TestMethod");
      
      m_result.TestStarted();
      try {
        test.Run(new TestResult());
      } catch (Exception) {
        m_result.TestFailed();
      }
      Assert.Equal("1 run, 0 failed", m_result.Summary);
    }

    public void TestSuite() {
      TestSuite suite = new TestSuite();
      suite.Add(new WasRunObj("TestMethod"));
      suite.Add(new WasRunObj("TestBrokenMethod"));

      suite.Run(m_result);
      Assert.Equal("2 run, 1 failed", m_result.Summary);
    }

    private TestResult m_result;
  }

  class Program {
    static void Main() {
      TestSuite suite = new TestSuite();

      suite.Add(new TestTestCase("TestTemplateMethod"));
      suite.Add(new TestTestCase("TestResult"));
      suite.Add(new TestTestCase("TestFailedResult"));
      suite.Add(new TestTestCase("TestFailedResultFormatting"));
      suite.Add(new TestTestCase("TestFailedSetUp"));

      TestResult result = new TestResult();
      suite.Run(result);
      Console.WriteLine(result.Summary);
    }
  }
}
