using Scripts.CustomTool.DesignPatterns;
using Scripts.CustomTool.DesignPatterns.ObserverPattern;

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
            {
                LanternUnlockSubject.Instance.Notify(null);
                MsgCenterByList.SendMessage(new CommonMsg
                {
                    MsgId = MsgCenterByList.SAFE_DOOR_OPEN
                });
            }
        }
    }
    public class LanternUnlockSubject : SubjectSingleton<LanternUnlockSubject>
    { }
    public class LanternResetSubject : SubjectSingleton<LanternResetSubject>
    { }
}