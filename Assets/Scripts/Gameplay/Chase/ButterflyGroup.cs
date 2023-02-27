using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.CustomTool.DesignPatterns;

namespace Scripts.Gameplay.Chase
{
    public class ButterflyGroup : MonoBehaviour
    {
        public List<Butterfly> butterflies;
        private void OnEnable()
        {
            ButterflySelectedSubject.Instance.Register(ButterflySelected);
        }
        private void OnDisable()
        {
            ButterflySelectedSubject.Instance.Register(ButterflySelected);
        }
        void ButterflySelected(object butterfly)
        {
            Butterfly b = butterfly as Butterfly;
            if (!butterflies.Contains(b))
                return;
            foreach (Butterfly bu in butterflies)
                if (bu != b)
                    bu.Disappear();
        }
    }
    public class ButterflySelectedSubject : SubjectSingleton<ButterflySelectedSubject> { }
}