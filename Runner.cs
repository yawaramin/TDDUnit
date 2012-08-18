using System;
using System.Linq;
using System.Reflection;
using System.IO;

namespace TDDUnit {
  class Runner {
    /*
     * output - a destination to write results out to. To write output
     * to the standard output, pass in System.Console.Out
     */
    public Runner(Type callingType, TextWriter output, Result result) {
      Suite suite = new Suite();
      Type[] forbiddenTypes = new Type[] {
        callingType
      , typeof (TDDUnit.WasRunObj)
      , typeof (TDDUnit.WasRunSetUpFailed)
      };

      foreach (Type t in Assembly.GetEntryAssembly().GetTypes()) {
        if (t.IsSubclassOf(typeof (TDDUnit.Case)) && !forbiddenTypes.Contains(t))
          suite.Add(t);
      }

      foreach (string test in suite.FailedTests(result)) {
        output.WriteLine("Failed: " + test);
      }
      output.WriteLine(result.Summary);
    }
  }
}
