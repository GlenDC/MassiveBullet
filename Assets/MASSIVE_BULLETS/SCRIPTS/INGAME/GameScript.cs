using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour
{
    void Update()
    {
        if( Input.GetKey( KeyCode.Escape ) )
        {
            Quit();
        }
    }

    void Quit()
    {
        Application.LoadLevel( "start_menu" );
    }

    public void OnGameOver()
    {
        Debug.Log( ":TODO: GameOver!" );
    }
}
