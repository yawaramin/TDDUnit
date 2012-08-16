using System;
using System.Collections.Generic;

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

    public void TestFailedSetUpDontCountTest() {
      WasRunSetUpFailed test = new WasRunSetUpFailed("TestMethod");

      test.Run(m_result);
      Assert.Equal("0 run, 0 failed", m_result.Summary);
    }

    public void TestNoTearDownWhenSetUpFailed() {
      WasRunSetUpFailed test = new WasRunSetUpFailed("TestMethod");

      test.Run(m_result);
      Assert.Equal("SetUp ", test.Log);
    }

    public void TestSuite() {
      TestSuite suite = new TestSuite();
      suite.Add(new WasRunObj("TestMethod"));
      suite.Add(new WasRunObj("TestBrokenMethod"));

      suite.Run(m_result);
      Assert.Equal("2 run, 1 failed", m_result.Summary);
    }

    public void TestRunAllTests() {
      TestSuite suite = new TestSuite(typeof (WasRunObj));
      suite.Run(m_result);
      Assert.Equal("2 run, 1 failed", m_result.Summary);
    }

    public void TestTearDownWhenTestFailed() {
      WasRunObj test = new WasRunObj("TestBrokenMethod");
      test.Run(m_result);
      Assert.Equal("SetUp TestBrokenMethod TearDown ", test.Log);
    }

    public void TestYieldFailedTestNames() {
      TestSuite suite = new TestSuite(typeof (WasRunObj));
      Assert.That(new HashSet<string>(suite.FailedTests(m_result))
        .SetEquals(new HashSet<string>(new string[] { "TestBrokenMethod" })));
    }

    private TestResult m_result;
  }

  class Program {
    static void Main() {
      TestSuite suite = new TestSuite(typeof (TestTestCase));
      TestResult result = new TestResult();

      foreach (string test in suite.FailedTests(result)) {
        Console.WriteLine("Failed: " + test);
      }
      Console.WriteLine(result.Summary);
    }
  }
}
