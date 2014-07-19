using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    const float SPEED = 1000.0f;

    Vector3 Velocity
        { get { return transform.forward * SPEED * Time.deltaTime; } }

    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent< Rigidbody >();
    }

    void Update()
    {
        rigidBody.velocity = Velocity;
    }
}
