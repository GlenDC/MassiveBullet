using UnityEngine;
using System.Collections;

public class CameraBlockScript : MonoBehaviour
{
    GameScript gameScript;
    Material objMaterial;

	void Start()
    {
	    gameScript =
            GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >();

        objMaterial = renderer.material;
	}

	void Update()
    {
	    Color
            color;

        color = objMaterial.color;
        color.a = gameScript.GetAlphaBlocker();

        objMaterial.color = color;
	}
}
