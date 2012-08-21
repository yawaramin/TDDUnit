/*
   TDDUnit

   Copyright 2012 Yawar Quadir Amin

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.IO;

namespace TDDUnit {
  class TestCase : Case {
    public TestCase(string name) : base(name) {
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

    public void TestTearDownWhenTestFailed() {
      WasRunObj test = new WasRunObj("TestBrokenMethod");
      test.Run(m_result);
      Assert.Equal("SetUp TestBrokenMethod TearDown ", test.Log);
    }

    private Result m_result;
  }

  class TestSuite : Case {
    public TestSuite(string name) : base(name) {
    }

    public override void SetUp() {
      base.SetUp();

      m_result = new Result();
    }

    public void TestSuiteOperation() {
      Suite suite = new Suite();
      suite.Add(new WasRunObj("TestMethod"));
      suite.Add(new WasRunObj("TestBrokenMethod"));

      suite.Run(m_result);
      Assert.Equal("2 run, 1 failed", m_result.Summary);
    }

    public void TestRunAllTests() {
      Suite suite = new Suite(typeof(WasRunObj));
      suite.Run(m_result);
      Assert.Equal("2 run, 1 failed", m_result.Summary);
    }

    public void TestYieldFailedTestNames() {
      Suite suite = new Suite(typeof(WasRunObj));
      Assert.That(new HashSet<string>(suite.FailedTests(m_result))
        .SetEquals(new HashSet<string>(new string[] { "TestBrokenMethod" })));
    }

    private Result m_result;
  }

  class TestRunner : Case {
    public TestRunner(string name) : base(name) {}

    public void TestRunAllTestCases() {
      StringWriter expected = new StringWriter();
      expected.WriteLine("16 run, 0 failed");

      StringWriter actual = new StringWriter();
      Runner.Run(actual, Program.m_result, typeof (TestRunner));

      Assert.Equal(expected.ToString(), actual.ToString());
    }
  }

  class TestAssert : Case {
    public TestAssert(string name) : base(name) {}

    public void TestAssertEqualWhenEqualShouldSucceed() {
      Assert.Equal(1, 1);
    }

    public void TestAssertEqualWhenNotEqualShouldFail() {
      try {
        Assert.Equal(1, 2);
      } catch (TestRunException e) {
        Assert.Equal("Expected:  '1'" + Environment.NewLine + "Actual: '2'", e.Message);
      }
    }

    public void TestAssertNotEqualWhenEqualShouldFail() {
      try {
        Assert.NotEqual(1, 1);
      } catch (TestRunException e) {
        Assert.Equal("Expected: Not '1'" + Environment.NewLine + "Actual: '1'", e.Message);
      }
    }

    public void TestAssertNotEqualWhenNotEqualShouldSucceed() {
      Assert.NotEqual(1, 2);
    }
  }

  class Program {
    public static Result m_result = new Result();

    static void Main() {
      Suite suite = new Suite(typeof(TestRunner));

      foreach (string test in suite.FailedTests(m_result))
        Console.WriteLine("Failed: " + test);
      Console.WriteLine(m_result.Summary);
    }
  }
}
