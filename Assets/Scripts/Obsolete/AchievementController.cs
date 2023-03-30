using Scripts.CustomTool.DesignPatterns.ObserverPattern;
using Scripts.Gameplay.GhostBook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Achievement
{
    [System.Obsolete]
    public enum endingNo { 
        ending1,
        ending2,
        ending3   
    }

    //TODO: optimize performance....
    [System.Obsolete]
    public class AchievementController : MonoBehaviour
    {
        float ChooseRoadNo = 0;
        
        [SerializeField] Transform player;
        private endingNo ending;
        
        private void judgeRoad()
        {
            
            if (player.position.z < 0)
                ChooseRoadNo -= ChooseRoadNo;
            else ChooseRoadNo += ChooseRoadNo;

            switch (ChooseRoadNo) {
                case 3:
                    ending = endingNo.ending3;
                    break;
                case -3:
                    ending = endingNo.ending1;
                    break;
                default:
                    ending = endingNo.ending2;
                    break;
            }
            MsgCenterByList.SendMessage(new CommonMsg()
            {
                MsgId = MsgCenterByList.ENDING,
                intParam = (int)ending,
            });
            Debug.Log(ending.ToString());
        }
        
    }
}

