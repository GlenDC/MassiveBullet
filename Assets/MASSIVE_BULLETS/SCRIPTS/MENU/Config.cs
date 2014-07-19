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
}
