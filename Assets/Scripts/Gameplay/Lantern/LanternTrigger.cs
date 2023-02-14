using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.CustomTool.EditorTools;

namespace Scripts.Gameplay.Lantern
{
    public enum LanternEnum
    {
        lantern1 = 1 << 0,
        lantern2 = 1 << 1,
        lantern3 = 1 << 2,
        lantern4 = 1 << 3,
        lantern5 = 1 << 4,
        lantern6 = 1 << 5,
        lantern7 = 1 << 6,
        lantern8 = 1 << 7,
        lantern9 = 1 << 8,
    }
    public class LanternTrigger : MonoBehaviour
    {
        [MultiSelector] public LanternEnum relatedLantern;

        private void OnTriggerEnter(Collider other)
        {
            LanternSwitchSubject.Instance.Notify(relatedLantern);
            if (CheckUnlock())
            {
                LanternUnlockSubject.Instance.Notify(null);
            }
        }
        bool CheckUnlock()
        {
            //TODO: µãµÆ½âËøÅÐ¶¨
            return false;
        }
    }
}