using UnityEngine;
using System.Collections;

public class PlayerBulletCollision : MonoBehaviour
{
    void OnTriggerEnter( Collider other )
    {
        if( other.gameObject.tag == TAGS.PLAYER_COLLISION )
        {
            other.transform.parent.gameObject.GetComponent< Player >().OnGameOver();
        }
    }
}
