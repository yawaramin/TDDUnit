namespace TDDUnit {
  class TestResult {
    public void TestStarted() {
      m_runCount++;
    }

    public void TestFailed() {
      m_errorCount++;
    }

    public string Summary {
      get {
        return string.Format("{0} run, {1} failed", m_runCount, m_errorCount);
      }
    }

    private int m_runCount = 0;
    private int m_errorCount = 0;
  }
}
