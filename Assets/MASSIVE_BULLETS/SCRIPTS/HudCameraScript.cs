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

	// Use this for initialization
	void Start () {

	}

	public void SetUpHUDCamera(bool gun_to_right){

		if (!gun_to_right){

			PistolPosition.x -= PistolPosition.x * 2;
			PistolOrientation.x += 2 * Mathf.PI;
		}

		PistolObject = Instantiate(PistolPrefab,PistolPosition,Quaternion.identity) as GameObject;
		PistolObject.transform.parent = this.transform;
		PistolObject.name = "HUD_PistolObject";
		
		PistolObject.GetComponent<PistolObjectScript>().SetUpPistolObject();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){



		GUI.Box (new Rect(10,10,Screen.width/2,Screen.height/2), new GUIContent(CrosshairTexture));
	}
}
