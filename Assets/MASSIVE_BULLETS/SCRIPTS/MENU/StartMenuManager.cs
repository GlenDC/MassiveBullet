using UnityEngine;
using System.Collections;

public class StartMenuManager : MonoBehaviour {

	[ SerializeField ] GameObject
		GameTitle;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpStartMenu(){

		//GameTitle.GetComponent<ObjectScaleScript>().SetUpScale();
	}

	public void RemoveTitle(){

		Destroy(GameTitle);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
