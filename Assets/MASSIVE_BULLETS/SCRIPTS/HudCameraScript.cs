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

	// Use this for initialization
	void Start () {

		PistolObject = Instantiate(PistolPrefab,PistolPosition,Quaternion.identity) as GameObject;
		PistolObject.transform.parent = this.transform;
		PistolObject.name = "HUD_PistolObject";

		//PistolObject.GetComponent<PistolObjectScript>().SetUpPistolObject();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
