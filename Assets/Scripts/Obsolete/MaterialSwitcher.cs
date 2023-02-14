using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Lantern
{
    [System.Obsolete]
    public class MaterialSwitcher : MonoBehaviour
    {
        public Material on;
        public Material off;

        public void Switch(string args)
        {
            string[] ids = args.Split();
            foreach (string id in ids)
            {
                Material target = transform.Find(id).GetComponent<MeshRenderer>().sharedMaterial;
                if (target == on)
                    transform.Find(id).GetComponent<MeshRenderer>().material = off;
                else
                    transform.Find(id).GetComponent<MeshRenderer>().material = on;
            }
        }
    }
}