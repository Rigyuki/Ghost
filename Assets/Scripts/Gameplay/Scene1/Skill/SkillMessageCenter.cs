using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMessageCenter : MonoBehaviour
{
    public  void sendSkillMessage(string skillName)
    {
        Sequencer.Message(skillName);
    }
}
