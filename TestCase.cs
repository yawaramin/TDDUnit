namespace TDDUnit {
  class TestCase {
    public TestCase(string name) {
      m_name = name;
    }

    public virtual void SetUp() {
    }

    public TestResult Run() {
      TestResult result = new TestResult();

      result.TestStarted();
      SetUp();
      GetType().GetMethod(m_name).Invoke(this, null);
      TearDown();
      return result;
    }

    public virtual void TearDown() {
    }

    private string m_name;
  }
}
