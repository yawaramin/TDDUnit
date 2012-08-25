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
namespace TDDUnit {
  namespace Tests {
    class WasRunObj : Case {
      public WasRunObj(string name)
        : base(name) {
      }

      public bool WasRun {
        get {
          return m_wasRun;
        }
      }

      public string Log {
        get {
          return m_log;
        }
      }

      public override void SetUp() {
        m_wasRun = false;
        m_log = "SetUp ";
      }

      public void TestMethod() {
        m_wasRun = true;
        m_log += "TestMethod ";
      }

      public void TestBrokenMethod() {
        m_log += "TestBrokenMethod ";
        throw new TestRunException("This test is meant to fail");
      }

      public override void TearDown() {
        m_log += "TearDown ";
      }

      private bool m_wasRun;
      private string m_log;
    }
  }
}
