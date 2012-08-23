/*
   TDDUnit

   Copyright 2012 Yawar Quadir Amin

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using System;

namespace TDDUnit {
  public class Assert {
    private static void Fail(object expected, object actual, bool equal) {
      string message = string.Format("Expected {2} '{0}'{3}Actual == '{1}'", expected, actual, (equal ? "==" : "=/="), Environment.NewLine);
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

    public static void Throws<TException>(Action action) where TException : Exception {
      try {
        action();
      } catch (TException) {
        return;
      }
      Fail("<" + (typeof (TException)).Name + ">", string.Empty, true);
    }
  }
}
