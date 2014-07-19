using UnityEngine;
using System.Collections;

public class Config : MonoBehaviour
{
    public HAND_STATE
        HandState { set; get; }

    public KEYBOARD_STATE
        KeyboardState { set; get; }

    void Start ()
    {
        HandState =
            ( HAND_STATE ) PlayerPrefs.GetInt(
                "MASSIVE_BULLET_PF_STATE_HAND", 2
                );

        KeyboardState =
            ( KEYBOARD_STATE ) PlayerPrefs.GetInt(
                "MASSIVE_BULLET_PF_STATE_KEYBOARD", 2
                );
    }

    public float GetLateralMovement()
    {
        float
            movement;

        if( KeyboardState == KEYBOARD_STATE.AZERTY )
        {
            movement = Input.GetKey( KeyCode.Z ) ? 1.0f : 0.0f;
        }
        else
        {
            movement = Input.GetKey( KeyCode.W ) ? 1.0f : 0.0f;
        }

        return movement - ( Input.GetKey( KeyCode.S ) ? 1.0f : 0.0f );
    }

    public float GetStrafeMovement()
    {
        float
            movement;

        if( KeyboardState == KEYBOARD_STATE.AZERTY )
        {
            movement = Input.GetKey( KeyCode.Q ) ? -1.0f : 0.0f;
        }
        else
        {
            movement = Input.GetKey( KeyCode.A ) ? -1.0f : 0.0f;
        }

        return movement + ( Input.GetKey( KeyCode.D ) ? 1.0f : 0.0f );
    }
}
