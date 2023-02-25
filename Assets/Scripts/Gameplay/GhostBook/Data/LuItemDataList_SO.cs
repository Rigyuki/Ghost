using Scripts.Gameplay.GhostBook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LuItemDataList_SO", menuName = "GhostBook/LuItemDataList_SO")]
public class LuItemDataList_SO : ScriptableObject
{
    public List<LuItemDetails> luItemDetails;

    public LuItemDetails GetLuItemDetails(LuType luType)
    {
        return luItemDetails.Find(i => i.luType == luType);
    }
}

[System.Serializable]
public class LuItemDetails
{
    public LuType luType;
    public Sprite luSprite;
}
