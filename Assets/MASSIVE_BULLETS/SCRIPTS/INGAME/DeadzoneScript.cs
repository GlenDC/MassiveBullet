using UnityEngine;
using System.Collections;

public class DeadzoneScript : MonoBehaviour
{
    GameScript gameScript;

    void Start()
    {
        gameScript =
            GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >();
    }

    void OnTriggerEnter( Collider other )
    {
        if( other.gameObject.tag == TAGS.PLAYER )
        {
            gameScript.OnGameOver();
            other.gameObject.GetComponent< Player >().OnGameOver();
        }
    }
}
