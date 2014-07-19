using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    Config configScript;
    GameObject thrash;

    protected int HSEH { get { return Screen.height / 2; } }
    protected int HSEW { get { return Screen.width / 2; } }
	
	[ SerializeField ] Font
		ControlButtonFont;

	[ SerializeField ] Texture2D
		SelectedBackground,
		UnselectedBackground,
		StartButton,
		QuitButton;

    void Start()
    {
		this.GetComponent<StartMenuManager>().SetUpStartMenu();

		configScript =
            GameObject.FindWithTag( TAGS.CONFIG ).GetComponent< Config >();

        thrash =
            GameObject.FindWithTag( TAGS.THRASH ).gameObject;

        leftHandButton = new ToggleButton();
		rightHandButton = new ToggleButton();
		azertyButton = new ToggleButton();
		qwertyButton = new ToggleButton();

		leftHandButton.SetButtonFont(ControlButtonFont);
		rightHandButton.SetButtonFont(ControlButtonFont);
		azertyButton.SetButtonFont(ControlButtonFont);
		qwertyButton.SetButtonFont(ControlButtonFont);

		leftHandButton.SetBackground(SelectedBackground,UnselectedBackground);
		rightHandButton.SetBackground(SelectedBackground,UnselectedBackground);
		azertyButton.SetBackground(SelectedBackground,UnselectedBackground);
		qwertyButton.SetBackground(SelectedBackground,UnselectedBackground);

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

		Texture2D
			selectedBackground,
			unselectedBackground;

		Font
			buttonFont;

		bool
			buttonSelected;

		public void SetButtonFont( Font button_Font ){

			buttonFont = button_Font;
		}

		public void SetBackground(Texture2D selected_background, Texture2D unselected_background){

			selectedBackground = selected_background;
			unselectedBackground = unselected_background;
		}

        public void SetToggled()
        {
            textColor = Color.white;

			buttonSelected = true;
        }

        public void SetUntoggled()
        {
            textColor = Color.black;

			buttonSelected = false;
        }

        public GUIStyle GetStyle()
        {
            GUIStyle style;

            style = new GUIStyle( GUI.skin.box );
            style.normal.textColor = textColor;
			style.font = buttonFont;
			style.alignment = TextAnchor.MiddleCenter;

			if (buttonSelected){
				style.fontSize = 20;
				style.normal.background = selectedBackground;
			}
			else if (!buttonSelected){
				style.fontSize = 15;
				style.normal.background = unselectedBackground;
			}

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
                new Rect( HSEW - 300, HSEH + 80, 256, 64 ),
                "LEFT HANDED",
                leftHandButton.GetStyle()
                ) )
        {
            SetLeftHanded();
        }

        if( GUI.Button(
                new Rect( HSEW + 44, HSEH + 80, 256, 64 ),
                "RIGHT HANDED",
                rightHandButton.GetStyle()
                ) )
        {
            SetRightHanded();
        }

        if( GUI.Button(
                new Rect( HSEW - 300, HSEH + 150, 256, 64 ),
                "AZERTY",
                azertyButton.GetStyle()
                ) )
        {
            SetAzerty();
        }

        if( GUI.Button(
                new Rect( HSEW + 44, HSEH + 150, 256, 64 ),
                "QWERTY",
                qwertyButton.GetStyle()
                ) )
        {
            SetQwerty();
        }

        if( GUI.Button(
                new Rect( HSEW - 128, HSEH - 10, 320, 80 ),
                StartButton,GUIStyle.none
                ) )
        {
			this.GetComponent<StartMenuManager>().RemoveTitle();
			Destroy( thrash );
            Application.LoadLevelAdditive( "main" );
        }

        if( GUI.Button(
                new Rect( HSEW - 50, HSEH + 240, 107, 40 ),
				QuitButton,GUIStyle.none
                ) )
        {
            Application.Quit();
        }

        // test fb

        GUI.DrawTexture(
            new Rect( 10, 10, 100, 100 ),
            Config.FacebookSelfie
            );

        GUI.TextField(
            new Rect( 10, 110, 100, 20),
            Config.FacebookName
            );
    }
}
