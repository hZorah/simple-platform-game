using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Core.Input
{
    public class GameInputHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent Jump;
        [SerializeField] private App.Utils.UnityEventFloat MoveHorizontally;
        public void Move(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            MoveHorizontally?.Invoke(input.x);

            if (input.y > 0)
            {
                Jump?.Invoke();
            }
        }

        public void Fire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Jump?.Invoke();
            }
        }
    }
}
