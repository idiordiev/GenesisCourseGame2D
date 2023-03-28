using System.Collections.Generic;
using System.Linq;

namespace Player
{
    public class PlayerBrain
    {
        private readonly PlayerEntity _playerEntity;
        private readonly List<IEntityInputSource> _inputSources;

        public PlayerBrain(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _inputSources = inputSources;
        }

        public void OnFixedUpdate()
        {
            _playerEntity.MoveHorizontally(GetHorizontalDirection());

            if (IsJump())
            {
                _playerEntity.Jump();
            }

            if (IsAttack())
            {
                _playerEntity.StartAttack();
            }

            foreach (var inputSource in _inputSources)
            {
                inputSource.ResetOneTimeActions();
            }
        }

        private float GetHorizontalDirection()
        {
            foreach (var inputSource in _inputSources)
            {
                if (inputSource.Direction != 0)
                    return inputSource.Direction;
            }

            return 0;
        }

        private bool IsJump()
        {
            return _inputSources.Any(s => s.Jump);
        }
        
        private bool IsAttack()
        {
            return _inputSources.Any(s => s.Attack);
        }
    }
}