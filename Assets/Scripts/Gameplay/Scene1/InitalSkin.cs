using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class InitalSkin : MonoBehaviour
{
    private SkeletonAnimation sa;
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SkeletonAnimation>();
    }

    public void SkinChange()
    {
        sa.initialSkinName = "d";
        sa.timeScale = 1;
        sa.Initialize(true);
    }
}
