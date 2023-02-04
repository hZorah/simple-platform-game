using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Core.Input
{
    public class GameInputHandler : MonoBehaviour
    {
         #region Inspector Fields
        [SerializeField] private UnityEvent _jump;
        [SerializeField] private App.Utils.UnityEventFloat _moveHorizontally;
        #endregion
        
        /// <summary>
        /// TODO: write summary
        /// </summary>
        public void Move(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _moveHorizontally?.Invoke(input.x);

            if (input.y > 0)
            {
                _jump?.Invoke();
            }
        }

        /// <summary>
        /// TODO: write summary
        /// </summary>
        public void Fire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _jump?.Invoke();
            }
        }
    }
}
