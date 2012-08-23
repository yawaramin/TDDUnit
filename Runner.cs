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
using System.Linq;
using System.Reflection;
using System.IO;

namespace TDDUnit {
  public class Runner {
    /*
     * output - a destination to write results out to. To write output
     * to the standard output, pass in System.Console.Out
     */
    public static void Run(TextWriter output, Result result) {
      Suite suite = new Suite();

      foreach (Type t in Assembly.GetEntryAssembly().GetTypes()) {
        if (t.IsSubclassOf(typeof(TDDUnit.Case))
          && !Assembly.GetExecutingAssembly().GetTypes().Contains(t)) {
          suite.Add(t);
          Console.WriteLine("Added " + t.Name);
        }
      }

      foreach (string test in suite.FailedTests(result)) {
        output.WriteLine("Failed: " + test);
      }
      output.WriteLine(result.Summary);
    }
  }
}
