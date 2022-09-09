using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitcher : MonoBehaviour
{
    public Material on;
    public Material off;

    public void TrapSignalReceiver(object args)
    {
        if(args is string stringArgs)
        {
            string[] ids = stringArgs.Split();
            foreach(string id in ids)
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
