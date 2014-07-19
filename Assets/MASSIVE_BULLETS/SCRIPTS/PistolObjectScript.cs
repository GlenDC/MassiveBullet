using UnityEngine;
using System.Collections;

public class PistolObjectScript : MonoBehaviour
{
	[ SerializeField ] GameObject
		PistolObject,
		PistolShotEffect1,
		PistolShotEffect2,
		PistolShotEffect3;

	public static GameObject GetPistol()
	{
		return GameObject.FindWithTag( TAGS.PISTOL );
	}

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpPistolObject(){

	}

	public void PlayShootAnimation(){

		PistolObject.GetComponent<Animation>().Play();

		PistolShotEffect1.GetComponent<ParticleSystem>().Play();
		PistolShotEffect2.GetComponent<ParticleSystem>().Play();
		PistolShotEffect3.GetComponent<ParticleSystem>().Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
