namespace TDDUnit {
  class Assert {
    public static void That(bool condition, string message = "Assertion failed") {
      if (!condition) throw new TestRunException(message);
    }
  }
}
