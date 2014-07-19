using UnityEngine;
using System.Collections;

public class Config : MonoBehaviour
{
    const string
        HAND_HANDLE = "MASSIVE_BULLET_PF_STATE_HAND",
        KEYBOARD_HANDLE = "MASSIVE_BULLET_PF_STATE_KEYBOARD",
        HIGHSCORE_HANDLE = "MASSIVE_BULLET_PF_HIGHSCORE";

    HAND_STATE _HandState;
    public HAND_STATE
        HandState
        {
            set
            {
                PlayerPrefs.SetInt( HAND_HANDLE, ( int ) value );
                _HandState = value;
            }
            get
            {
                return _HandState;
            }
        }

    KEYBOARD_STATE _KeyboardState;
    public KEYBOARD_STATE
        KeyboardState
        {
            set
            {
                PlayerPrefs.SetInt( KEYBOARD_HANDLE, ( int ) value );
                _KeyboardState = value;
            }
            get
            {
                return _KeyboardState;
            }
        }

    int _highscore;
    public int
        highscore
        {
            set
            {
                PlayerPrefs.SetInt( HIGHSCORE_HANDLE, value );
                _highscore = value;
            }
            get
            {
                return _highscore;
            }
        }

    void Start ()
    {
        _HandState =
            ( HAND_STATE ) PlayerPrefs.GetInt(
                HAND_HANDLE, 2
                );

        _KeyboardState =
            ( KEYBOARD_STATE ) PlayerPrefs.GetInt(
                KEYBOARD_HANDLE, 2
                );

        _highscore = PlayerPrefs.GetInt(
            HIGHSCORE_HANDLE, 0
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
