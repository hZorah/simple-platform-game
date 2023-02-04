using UnityEngine;

namespace Core.PlayerCharacter
{
    [CreateAssetMenu(fileName = "Character Movement Parameters", menuName = "Core/PlayerCharacter/CharacterMovementParameters", order = 0)]
    public class CharacterMovementParameters : ScriptableObject
    {
        #region Inspector Fields
        [SerializeField] private float _movementSpeed;
        [Range(0, .3f)][SerializeField] private float _movementSmoothing;
        [SerializeField] private float _jumpForce;
        [SerializeField] private LayerMask _whatIsGround;
        #endregion
        #region Properties
        public float MovementSpeed => _movementSpeed;
        public float MovementSmoothing => _movementSmoothing;
        public float JumpForce => _jumpForce;
        public LayerMask WhatIsGround => _whatIsGround;

        #endregion
    }
}