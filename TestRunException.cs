namespace TDDUnit {
  public class TestRunException : System.Exception {
    public TestRunException(string message) {
      m_message = message;
    }

    public override string Message {
      get {
        return m_message;
      }
    }

    private string m_message;
  }
}
