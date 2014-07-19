using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    Config configScript;

    protected int HSEH { get { return Screen.height / 2; } }
    protected int HSEW { get { return Screen.width / 2; } }

    void Start()
    {
        configScript =
            GameObject.FindWithTag( TAGS.CONFIG ).GetComponent< Config >();

        leftHandButton = new ToggleButton();
        rightHandButton = new ToggleButton();
        azertyButton = new ToggleButton();
        qwertyButton = new ToggleButton();

        if( configScript.HandState == HAND_STATE.LEFT )
        {
            SetLeftHanded();
        }
        else
        {
            SetRightHanded();
        }

        if( configScript.KeyboardState == KEYBOARD_STATE.AZERTY )
        {
            SetAzerty();
        }
        else
        {
            SetQwerty();
        }
    }

    struct ToggleButton
    {
        Color
            backgroundColor,
            textColor;

        public void SetToggled()
        {
            textColor = Color.green;
        }

        public void SetUntoggled()
        {
            textColor = Color.red;
        }

        public GUIStyle GetStyle()
        {
            GUIStyle style;

            style = new GUIStyle( GUI.skin.box );
            style.normal.textColor = textColor;
            style.fontSize = 13;

            return style;
        }
    }

    ToggleButton
        leftHandButton,
        rightHandButton,
        azertyButton,
        qwertyButton;

    void SetLeftHanded()
    {
        leftHandButton.SetToggled();
        rightHandButton.SetUntoggled();

        configScript.HandState = HAND_STATE.LEFT;
    }

    void SetRightHanded()
    {
        leftHandButton.SetUntoggled();
        rightHandButton.SetToggled();

        configScript.HandState = HAND_STATE.RIGHT;
    }

    void SetQwerty()
    {
        qwertyButton.SetToggled();
        azertyButton.SetUntoggled();

        configScript.KeyboardState = KEYBOARD_STATE.QWERTY;
    }

    void SetAzerty()
    {
        qwertyButton.SetUntoggled();
        azertyButton.SetToggled();

        configScript.KeyboardState = KEYBOARD_STATE.AZERTY;
    }

    void OnGUI()
    {
        if( GUI.Button(
                new Rect( HSEW - 175, HSEH - 50, 150, 25 ),
                "LEFT HANDED",
                leftHandButton.GetStyle()
                ) )
        {
            SetLeftHanded();
        }

        if( GUI.Button(
                new Rect( HSEW + 25, HSEH - 50, 150, 25 ),
                "RIGHT HANDED",
                rightHandButton.GetStyle()
                ) )
        {
            SetRightHanded();
        }

        if( GUI.Button(
                new Rect( HSEW - 175, HSEH + 50, 150, 25 ),
                "AZERTY",
                azertyButton.GetStyle()
                ) )
        {
            SetAzerty();
        }

        if( GUI.Button(
                new Rect( HSEW + 25, HSEH + 50, 150, 25 ),
                "QWERTY",
                qwertyButton.GetStyle()
                ) )
        {
            SetQwerty();
        }
    }
}
