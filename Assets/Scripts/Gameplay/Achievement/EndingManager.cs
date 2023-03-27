using Scripts.CustomTool.DesignPatterns.ObserverPattern;
using Scripts.Gameplay.GhostBook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Gameplay.Achievement;
public class EndingManager : MonoBehaviour
{
     
    private void OnEnable()
    {
        MsgCenterByList.AddListener(OnMsg);
    }

    private void OnDisable()
    {
        MsgCenterByList.RemoveListener(OnMsg);
    }

    private void OnMsg(CommonMsg obj)
    {
        if (obj.MsgId != MsgCenterByList.ENDING) return;
        OnShowEnding((endingNo)obj.intParam);
       
    }

    private void OnShowEnding(endingNo no)
    {
        var ending = no;

        //TODO: show last ending 
    }



}
