namespace TDDUnit {
  class WasRunSetUpFailed : WasRunObj {
    public WasRunSetUpFailed(string name) : base(name) {
    }

    public override void SetUp() {
      base.SetUp();

      throw new TestRunException("SetUp failed");
    }
  }
}
