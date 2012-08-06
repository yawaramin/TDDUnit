namespace TDDUnit {
  class TestResult {
    public void TestStarted() {
      m_runCount++;
    }

    public string Summary {
      get {
        return string.Format("{0} run, 0 failed", m_runCount);
      }
    }

    private int m_runCount = 0;
  }
}
