using Spine;
using Zenject;

namespace Code
{
    public class PlayerMoveViewLayer0 : IGameObjectActiveListener, IGameTickable
    {
        public bool enabled { get; private set; }

        PlayerViewPresenter _playerViewPresenter;
        PlayerModel _playerModel;
        PlayerBodyState _previousViewState;

        [Inject]
        void Construct(PlayerViewPresenter playerViewPresenter, PlayerModel playerModel)
        {
            _playerViewPresenter = playerViewPresenter;
            _playerModel = playerModel;
        }

        void IGameObjectActiveListener.OnEnable()
        {
            enabled = true;
        }

        void IGameObjectActiveListener.OnDisable()
        {
            enabled = false;
        }

        void IGameTickable.GameTick()
        {
            if ((_playerViewPresenter.skeletonAnimation.skeleton.ScaleX < 0) != _playerModel.FacingLeft)
            {
                Turn(_playerModel.FacingLeft);
            }

            PlayerBodyState currentModelState = _playerModel.CurrentBodyState;

            if (_previousViewState != currentModelState)
            {
                PlayNewStableAnimation();
            }

            _previousViewState = currentModelState;
        }

        void PlayNewStableAnimation()
        {
            PlayerBodyState newModelState = _playerModel.CurrentBodyState;
            Animation nextAnimation;

            if (newModelState == PlayerBodyState.Jumping)
            {
                nextAnimation = _playerViewPresenter.jump;
            }
            else
            {
                if (newModelState == PlayerBodyState.Running)
                {
                    nextAnimation = _playerViewPresenter.run;
                }
                else
                {
                    nextAnimation = _playerViewPresenter.idle;
                }
            }

            _playerViewPresenter.skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, true);
        }


        void Turn(bool facingLeft)
        {
            _playerViewPresenter.skeletonAnimation.Skeleton.ScaleX = facingLeft ? -1f : 1f;
        }
    }
}