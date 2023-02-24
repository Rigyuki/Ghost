using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.PlayerController
{
    public class AnimationPlayer : MonoBehaviour
    {
        public SkeletonAnimation sa;
        public string animName;
        // Start is called before the first frame update
        void Start()
        {
            sa.state.SetAnimation(0, animName, true);
        }
    }
}