using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class DialogueTest : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueSystemTrigger systemEvents;

    private void OnTriggerEnter(Collider other)
    {
        systemEvents.OnUse(other.transform);
    }
    private void OnCollisionEnter(Collision collision)
    {
      
    }
}
