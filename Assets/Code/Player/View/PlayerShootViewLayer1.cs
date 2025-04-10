using Spine;
using Zenject;

namespace Code
{
    public class PlayerShootViewLayer1
    {
        PlayerViewPresenter _playerViewPresenter;
        PlayerModel _playerModel;

        [Inject]
        void Construct(PlayerViewPresenter playerViewPresenter, PlayerModel playerModel)
        {
            _playerViewPresenter = playerViewPresenter;
            _playerModel = playerModel;
            _playerModel.ShootEvent += PlayShoot;
        }
        
        void PlayShoot () {
            TrackEntry shootTrack = _playerViewPresenter.skeletonAnimation.AnimationState.SetAnimation(1, _playerViewPresenter.shoot, false);
            shootTrack.MixAttachmentThreshold = 1f;
            shootTrack.SetMixDuration(0f, 0f);
            _playerViewPresenter.skeletonAnimation.state.AddEmptyAnimation(1, 0.5f, 0.1f);

            // _playerViewPresenter.shootParticles.Play();
        }
    }
}