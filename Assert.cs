using System;

namespace TDDUnit {
  class Assert {
    public static void Equal(object expected, object actual) {
      if (!expected.Equals(actual)) {
        string message = string.Format("Expected: '{0}'; Actual: '{1}'", expected, actual);
        Console.WriteLine(message);
        throw new TestRunException(message);
      }
    }

    public static void NotEqual(object expected, object actual) {
      if (expected.Equals(actual)) {
        string message = string.Format("Expected: Not '{0}'; Actual: '{1}'", expected, actual);
        Console.WriteLine(message);
        throw new TestRunException(message);
      }
    }

    public static void That(bool condition) {
      Equal(true, condition);
    }
  }
}
