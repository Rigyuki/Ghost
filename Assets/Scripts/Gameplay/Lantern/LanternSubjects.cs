using Scripts.CustomTool.DesignPatterns;

namespace Scripts.Gameplay.Lantern
{
    public class LanternSwitchSubject : SubjectSingleton<LanternSwitchSubject>
    {
        public bool allLanternOn;
        public override void Notify(object arg)
        {
            allLanternOn = true;
            base.Notify(arg);
            if (allLanternOn)
                LanternUnlockSubject.Instance.Notify(null);
        }
    }
    public class LanternUnlockSubject : SubjectSingleton<LanternUnlockSubject>
    { }
}