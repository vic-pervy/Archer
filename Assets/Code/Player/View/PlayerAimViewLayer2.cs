using Spine;
using Spine.Unity;
using Zenject;

namespace Code
{
    public class PlayerAimViewLayer2 : IGameObjectActiveListener, IGameTickable
    {
        public bool enabled {get; private set;}
        
        PlayerViewPresenter _playerViewPresenter;
        PlayerModel _playerModel;
        Bone bone;

        [Inject]
        void Construct(PlayerViewPresenter playerViewPresenter, PlayerModel playerModel)
        {
            _playerViewPresenter = playerViewPresenter;
            _playerModel = playerModel;
            bone = _playerViewPresenter.skeletonAnimation.Skeleton.FindBone(_playerViewPresenter.boneName);

        }
        
        void IGameObjectActiveListener.OnEnable()
        {
            enabled = true;
            StartPlayingAim();
        }

        void IGameObjectActiveListener.OnDisable()
        {
            enabled = false;
        }

        void IGameTickable.GameTick()
        {
            var skeletonSpacePoint = _playerModel.LocalAimPoint;
            skeletonSpacePoint.x *= _playerViewPresenter.skeletonAnimation.Skeleton.ScaleX;
            skeletonSpacePoint.y *= _playerViewPresenter.skeletonAnimation.Skeleton.ScaleY;
            bone.SetLocalPosition(skeletonSpacePoint);
        }
        
        void StartPlayingAim () {
            TrackEntry aimTrack = _playerViewPresenter.skeletonAnimation.AnimationState.SetAnimation(2, _playerViewPresenter.aim, true);
            aimTrack.MixAttachmentThreshold = 1f;
            aimTrack.SetMixDuration(0f, 0f);
        }
    }
}