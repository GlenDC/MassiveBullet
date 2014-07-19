using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    const float SPEED = 2500.0f;

    const float MIN_DISTANCE = 2.5f;
    const float MAX_DISTANCE = 5.0f;

    const float MIN_ROTATION_TIME = 3.0f;
    const float MAX_ROTATION_TIME = 10.0f;

    [SerializeField]
        Transform VisualBullet;

    Rigidbody rigidBody;

    BULLET_STATE state;

    Transform player;

    GameScript gameScript;

    Vector3 RotationTime;
    float currentTiming;

    Vector3 direction, originalDirection, targetDirection;

    void Start()
    {
        player = GameObject.FindWithTag( TAGS.PLAYER ).transform;
        gameScript = GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >();

        rigidBody = GetComponent< Rigidbody >();
        state = BULLET_STATE.GOING;

        direction = Vector3.zero;
    }

    void Update()
    {

        float
            distance;

        if( direction == Vector3.zero )
        {
            direction = transform.forward;
            distance = 0.0f;
        }

        targetDirection = player.position - transform.position;
        distance = targetDirection.magnitude;
        targetDirection.Normalize();

        if( state == BULLET_STATE.GOING )
        {
            if( distance > MAX_DISTANCE )
            {
                state = BULLET_STATE.RETURNING;

                RotationTime = Vector3.zero;

                currentTiming = 0.0f;

                RotationTime.x = gameScript.GetRandomFloat( MIN_ROTATION_TIME, MAX_ROTATION_TIME );
                RotationTime.y = gameScript.GetRandomFloat( MIN_ROTATION_TIME, MAX_ROTATION_TIME );
                RotationTime.z = gameScript.GetRandomFloat( MIN_ROTATION_TIME, MAX_ROTATION_TIME );

                originalDirection = direction;
            }

            rigidBody.velocity = direction * SPEED * Time.deltaTime;
        }
        else // returning
        {
            currentTiming += Time.deltaTime;

            direction.x = Mathf.Lerp( originalDirection.x, targetDirection.x, currentTiming / RotationTime.x );
            direction.y = Mathf.Lerp( originalDirection.y, targetDirection.y, currentTiming / RotationTime.y );
            direction.z = Mathf.Lerp( originalDirection.z, targetDirection.z, currentTiming / RotationTime.z );

            rigidBody.velocity = direction * SPEED * Time.deltaTime;

            if( distance < MIN_DISTANCE )
            {
                state = BULLET_STATE.GOING;
            }
        }

        transform.LookAt( transform.position + direction );
    }
}
