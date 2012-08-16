using System;
using System.Collections.Generic;

namespace TDDUnit {
  class TestTestCase : Case {
    public TestTestCase(string name) : base(name) {
    }

    public override void SetUp() {
      base.SetUp();

      m_result = new Result();
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
        test.Run(new Result());
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
      Suite suite = new Suite();
      suite.Add(new WasRunObj("TestMethod"));
      suite.Add(new WasRunObj("TestBrokenMethod"));

      suite.Run(m_result);
      Assert.Equal("2 run, 1 failed", m_result.Summary);
    }

    public void TestRunAllTests() {
      Suite suite = new Suite(typeof (WasRunObj));
      suite.Run(m_result);
      Assert.Equal("2 run, 1 failed", m_result.Summary);
    }

    public void TestTearDownWhenTestFailed() {
      WasRunObj test = new WasRunObj("TestBrokenMethod");
      test.Run(m_result);
      Assert.Equal("SetUp TestBrokenMethod TearDown ", test.Log);
    }

    public void TestYieldFailedTestNames() {
      Suite suite = new Suite(typeof (WasRunObj));
      Assert.That(new HashSet<string>(suite.FailedTests(m_result))
        .SetEquals(new HashSet<string>(new string[] { "TestBrokenMethod" })));
    }

    private Result m_result;
  }

  class Program {
    static void Main() {
      Suite suite = new Suite(typeof (TestTestCase));
      Result result = new Result();

      foreach (string test in suite.FailedTests(result)) {
        Console.WriteLine("Failed: " + test);
      }
      Console.WriteLine(result.Summary);
    }
  }
}
