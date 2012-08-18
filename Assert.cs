using System;

namespace TDDUnit {
  class Assert {
    private static void Fail(object expected, object actual, bool equal) {
      string message = string.Format("Expected: {2} '{0}'\nActual: '{1}'", expected, actual, (equal ? "" : "Not"));
      Console.WriteLine(message);
      throw new TestRunException(message);
    }

    public static void Equal(object expected, object actual) {
      if (!expected.Equals(actual)) {
        Fail(expected, actual, true);
      }
    }

    public static void NotEqual(object expected, object actual) {
      if (expected.Equals(actual)) {
        Fail(expected, actual, false);
      }
    }

    public static void That(bool condition) {
      Equal(true, condition);
    }
  }
}
