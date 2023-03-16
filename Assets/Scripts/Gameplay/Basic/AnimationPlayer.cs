using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Basic
{
    [RequireComponent(typeof(SkeletonAnimation))]
    public class AnimationPlayer : MonoBehaviour
    {
        [HideInInspector]public SkeletonAnimation sa;
        bool dontInterrupt;
        private void Awake()
        {
            sa = GetComponent<SkeletonAnimation>();
        }
        public string AddSuffix(string baseName, int facing)
        {
            switch (facing)
            {
                case 1:
                case 9:
                    return baseName + "_315";
                case 2:
                case 6:
                    return baseName + "_135";
                case 4:
                case 5:
                    return baseName + "_225";
                case 8:
                case 10:
                    return baseName + "_45";
            }
            return baseName;
        }
        public void Play(int track,string name,bool loop,bool dontInterrupt)
        {
            if(sa.state.GetCurrent(track)==null)
            {
                //sa.Skeleton.SetToSetupPose();
                //sa.AnimationState.ClearTracks();
                sa.state.SetAnimation(track, name, loop);
                return;
            }
            if (dontInterrupt)
                this.dontInterrupt = false;
            if (this.dontInterrupt && !sa.state.GetCurrent(0).IsComplete)
                return;
            this.dontInterrupt = dontInterrupt;
            if (sa.state.GetCurrent(track).Animation.Name == name)
                return;
            //sa.Skeleton.SetToSetupPose();
            //sa.AnimationState.ClearTracks();
            sa.state.SetAnimation(track, name, loop);
        }
        public void Play(int track, string nameBase, int facing, bool loop, bool dontInterrupt = false)
        {
            Play(track, AddSuffix(nameBase, facing), loop, dontInterrupt);
        }
    }
}