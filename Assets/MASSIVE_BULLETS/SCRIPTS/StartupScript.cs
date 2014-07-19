using UnityEngine;
using System.Collections;

public class StartupScript : MonoBehaviour
{
	void Start ()
    {
        Application.LoadLevelAdditive( "start_menu" );
        Destroy( gameObject );
	}
}
