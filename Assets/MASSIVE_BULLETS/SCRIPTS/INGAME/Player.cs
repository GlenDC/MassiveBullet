using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    Transform gameSpawn;

    void Start()
    {
        gameSpawn =
            GameObject.FindWithTag( TAGS.SPAWN ).transform;

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        transform.position = gameSpawn.position;
    }

    public void OnGameOver()
    {
        SpawnPlayer();
    }
}
