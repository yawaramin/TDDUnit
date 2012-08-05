namespace TDDUnit {
  class TestCase {
    public TestCase(string name) {
      m_name = name;
    }

    public void Run() {
      GetType().GetMethod(m_name).Invoke(this, null);
    }

    private string m_name;
  }
}
