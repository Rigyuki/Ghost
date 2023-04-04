using Scripts.CustomTool.DesignPatterns.ObserverPattern;
using Scripts.Gameplay.GhostBook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Gameplay.Achievement;
public class EndingManager : MonoBehaviour
{
    public int chooseRoadNo;
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
        if (obj.MsgId == MsgCenterByList.ROAD_CHOOSING)
        {
            chooseRoadNo += obj.intParam;
        }
        else if(obj.MsgId==MsgCenterByList.ENDING)
        {
            OnShowEnding();
        }
    }

    private void OnShowEnding()
    {
        switch(chooseRoadNo)
        {
            case 3:
                break;
            case -3:
                break;
            default:
                break;
        }
        //TODO: show last ending 
    }



}
