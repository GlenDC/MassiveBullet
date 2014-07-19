using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour
{
    public Bullet BulletInfo { get; private set; }

    void Start()
    {
        BulletInfo = transform.parent.gameObject.GetComponent< Bullet >();
        BulletInfo.PhysicsBullet.active = false;
    }

    void OnCollisionEnter( Collision collision )
    {
        if( collision.gameObject.tag == TAGS.BULLET_COLLISION )
        {
            if( BulletInfo.groupID != collision.gameObject.GetComponent< BulletCollision >().BulletInfo.groupID )
            {
                Destroy( collision.transform.parent.gameObject );
                Destroy( transform.parent.gameObject );

                GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >().RemoveBullet();

                Debug.Log( "Kill!" );
            }
        }
    }

    void OnTriggerEnter( Collider other )
    {
        if( other.gameObject.tag == TAGS.PLAYER_COLLISION )
        {
            other.transform.parent.gameObject.GetComponent< Player >().OnGameOver();
        }
    }
}
