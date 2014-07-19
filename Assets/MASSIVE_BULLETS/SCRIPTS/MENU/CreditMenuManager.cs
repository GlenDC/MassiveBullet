using UnityEngine;
using System.Collections;

public class CreditMenuManager : MonoBehaviour
{
	GameObject thrash;
	
	protected int HSEH { get { return Screen.height / 2; } }
	protected int HSEW { get { return Screen.width / 2; } }

	[ SerializeField ] string
		CreditText;

	[ SerializeField ] GameObject
		CreditTextObject;

	[ SerializeField ] Texture2D
		BackButton;

	// Use this for initialization
	void Start () {

		thrash =
			GameObject.FindWithTag( TAGS.THRASH ).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

		if( GUI.Button(
			new Rect( HSEW - 50, HSEH + 240, 107, 40 ),
			BackButton,GUIStyle.none
			) )
		{
			Destroy( thrash );
			Application.LoadLevelAdditive( "start_menu" );
		}
	}
}
