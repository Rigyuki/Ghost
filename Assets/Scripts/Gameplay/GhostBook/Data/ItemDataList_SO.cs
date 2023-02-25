using Scripts.Gameplay.GhostBook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList_SO",menuName = "GhostBook/ItemDataList_SO")]
public class ItemDataList_SO : ScriptableObject
{
    public List<GhostItemDetails> ghostitemDetails;
   // public List<LuItemDetails> luItemDetails;
    public GhostItemDetails GetItemDetails(GhostType ghostType) 
    {
        return ghostitemDetails.Find(i => i.ghostType == ghostType);
    }

/*    public LuItemDetails GetLuItemDetails(LuType luType)
    {
        return luItemDetails.Find(i => i.luType == luType);
    }*/
}

[System.Serializable]
public class GhostItemDetails 
{
    public GhostType ghostType;
    public Sprite gSprite;
}

/*public class LuItemDetails 
{
    public LuType luType;
    public Sprite luSprite;
}*/




