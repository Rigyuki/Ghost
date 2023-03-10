using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerArea : MonoBehaviour
{
    public DialogueData dialogueData;
    public bool isDisposable;

    private void OnTriggerEnter(Collider other)
    {
        if (dialogueData == null) return;
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        DialogueManager.instance.Load(dialogueData);
        DialogueManager.instance.Next();
        if (isDisposable) dialogueData = null;
    }
}
