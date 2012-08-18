using System;

namespace TDDUnit {
  class Assert {
    private static void Fail(object expected, object actual) {
      string message = string.Format("Expected: '{0}'\nActual: '{1}'", expected, actual);
      Console.WriteLine(message);
      throw new TestRunException(message);
    }

    public static void Equal(object expected, object actual) {
      if (!expected.Equals(actual)) {
        Fail(expected, actual);
      }
    }

    public static void NotEqual(object expected, object actual) {
      if (expected.Equals(actual)) {
        Fail(expected, actual);
      }
    }

    public static void That(bool condition) {
      Equal(true, condition);
    }
  }
}
