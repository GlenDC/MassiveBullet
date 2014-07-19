using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour
{
    void OnCollisionEnter( Collision collision )
    {
        if( collision.gameObject.tag == TAGS.BULLET_COLLISION )
        {
            Destroy( collision.transform.parent.gameObject );
            Destroy( transform.parent.gameObject );

            GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >().RemoveBullet();
        }
        else if( collision.gameObject.tag == TAGS.PLAYER_COLLISION )
        {
            collision.gameObject.GetComponent< Player >().OnGameOver();
        }
    }
}
