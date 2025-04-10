using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    public class PlayerViewPresenter : MonoBehaviour
    {
        public SkeletonAnimation skeletonAnimation;
        public AnimationReferenceAsset run, idle, aim, shoot, jump;
        public ParticleSystem shootParticles;
        [SpineBone(dataField: "skeletonAnimation")]
        public string boneName;
    }
}