using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Chase
{
    [RequireComponent(typeof(ButterflyGroup))]
    public class Blocker : MonoBehaviour
    {
        bool blocked;
        ButterflyGroup butterflyGroup;
        public Butterfly relatedButterfly;
        public List<GameObject> blocks;
        private void OnEnable()
        {
            ButterflySelectedSubject.Instance.Register(BlockRoads);
        }
        private void OnDisable()
        {
            ButterflySelectedSubject.Instance.Unregister(BlockRoads);
        }
        private void Start()
        {
            butterflyGroup = GetComponent<ButterflyGroup>();
        }
        void BlockRoads(object arg)
        {
            if (blocked)
                return;
            Butterfly butterfly = arg as Butterfly;
            if (!butterflyGroup.butterflies.Contains(butterfly)) return;
            if (relatedButterfly==butterfly)
                return;
            foreach(var g in blocks)
                g.SetActive(true);
            blocked = true;
        }
    }
}