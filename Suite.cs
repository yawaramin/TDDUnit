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
using System.Collections.Generic;
using System.Linq;

namespace TDDUnit {
  public class Suite {
    public Suite() {}

    public Suite(Type type) {
      if (type != null) Add(type);
    }

    public void Add(Type type) {
      var methodInfos = from methodInfo in type.GetMethods()
                        where methodInfo.Name.StartsWith("Test")
                        select methodInfo;
      foreach (var methodInfo in methodInfos)
        Add((Case)type.GetConstructor(new Type[] { typeof(string) }).Invoke(new string[] { methodInfo.Name }));
    }

    public void Add(Case test) {
      m_tests.Add(test);
    }

    public void Run(Result result) {
      foreach (Case test in m_tests) test.Run(result);
    }

    public IEnumerable<string> FailedTests(Result result) {
      int oldErrorCount;

      foreach (Case test in m_tests) {
        oldErrorCount = result.ErrorCount;
        test.Run(result);
        if (result.ErrorCount == oldErrorCount + 1) {
          yield return test.Name;
        }
      }
    }

    private List<Case> m_tests = new List<Case>();
  }
}
