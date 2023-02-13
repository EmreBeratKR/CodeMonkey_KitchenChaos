using EmreBeratKR.ServiceLocator;
using UnityEngine;

public class GameInput : ServiceBehaviour
{
    private PlayerInputActions.PlayerActions m_PlayerActions;


    private void Awake()
    {
        var handler = new PlayerInputActions();
        m_PlayerActions = handler.Player;
       
        m_PlayerActions.Move.Enable();
    }


    public Vector3 GetDirectionNormalized()
    {
        var value = m_PlayerActions.Move.ReadValue<Vector2>();
        return new Vector3(value.x, 0f, value.y);
    }
}