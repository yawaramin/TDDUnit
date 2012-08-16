﻿using System;

namespace TDDUnit {
  class TestCase {
    public TestCase(string name) {
      m_name = name;
    }

    public virtual void SetUp() {
    }

    public void Run(TestResult result) {

      try {
        SetUp();
      } catch (Exception) {
        return;
      }

      result.TestStarted();
      try {
        GetType().GetMethod(m_name).Invoke(this, null);
      } catch (Exception) {
        result.TestFailed();
      } finally {
        TearDown();
      }
    }

    public string Name {
      get {
        return m_name;
      }
    }

    public virtual void TearDown() {
    }

    private string m_name;
  }
}
