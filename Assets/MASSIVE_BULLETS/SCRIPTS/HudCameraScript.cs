using UnityEngine;
using System.Collections;

public class HudCameraScript : MonoBehaviour {

	[ SerializeField ] GameObject
		PistolPrefab;

	GameObject
		PistolObject;

	[ SerializeField ] Vector3
		PistolPosition;

	[ SerializeField ] Quaternion
		PistolOrientation;

	[ SerializeField ] Texture2D
		CrosshairTexture;

	Config
		configScript;

	// Use this for initialization
	void Start () {
		configScript = GameScript.GetConfig();

		SetUpHUDCamera( configScript.HandState == HAND_STATE.RIGHT );
	}

	public void SetUpHUDCamera(bool gun_to_right){

		if ( !gun_to_right ){

			PistolPosition.x -= PistolPosition.x * 2;
			PistolOrientation.x += 2 * Mathf.PI;
		}

		PistolObject = Instantiate(PistolPrefab,Vector3.zero,Quaternion.identity) as GameObject;
		PistolObject.transform.parent = transform;
		PistolObject.name = "HUD_PistolObject";

		PistolObject.transform.localPosition = PistolPosition;
		
		PistolObject.GetComponent<PistolObjectScript>().SetUpPistolObject();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.DrawTexture(
			new Rect( ( Screen.width / 2 ) - 25, ( Screen.height / 2 ) - 25, 50, 50 ),
			CrosshairTexture
			);
	}
}
