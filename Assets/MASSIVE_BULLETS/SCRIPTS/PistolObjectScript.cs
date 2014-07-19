using UnityEngine;
using System.Collections;

public class PistolObjectScript : MonoBehaviour
{
	public static GameObject GetPistol()
	{
		return GameObject.FindWithTag( TAGS.PISTOL );
	}

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpPistolObject(){

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
