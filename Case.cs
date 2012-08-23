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
  public abstract class Case {
    public Case(string name) {
      m_name = name;
    }

    public virtual void SetUp() {
    }

    public void Run(Result result) {

      try {
        SetUp();
      } catch (Exception) {
        return;
      }

      result.TestStarted();
      try {
        GetType().GetMethod(m_name).Invoke(this, null);
      } catch (Exception) {
        result.TestFailed();
      } finally {
        TearDown();
      }
    }

    public string Name {
      get {
        return m_name;
      }
    }

    public virtual void TearDown() {
    }

    private string m_name;
  }
}
