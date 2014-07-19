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

    void OnTriggerEnter( Collider other )
    {
        if( other.gameObject.tag == TAGS.PLAYER_COLLISION )
        {
            other.transform.parent.gameObject.GetComponent< Player >().OnGameOver();
        }
        else if( other.gameObject.tag == TAGS.BULLET_COLLISION )
        {
            Bullet
                otherBullet;

            otherBullet = other.gameObject.transform.parent.GetComponent< Bullet >();

            if( !otherBullet.IsDead && !BulletInfo.IsDead && BulletInfo.groupID != otherBullet.groupID )
            {
                Destroy( other.transform.parent.gameObject );
                Destroy( transform.parent.gameObject );

                GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >().RemoveBullet();
            }
        }
    }
}
