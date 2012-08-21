using System;

namespace TDDUnit {
  abstract class Case {
    public Case(string name) {
      m_name = name;
    }

    public virtual void SetUp() {
    }

    public void Run(Result result) {

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
