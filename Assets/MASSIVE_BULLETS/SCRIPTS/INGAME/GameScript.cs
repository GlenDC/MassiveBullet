using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour
{
    public static Config GetConfig()
    {
        GameObject
            config;

        config = GameObject.FindWithTag( TAGS.CONFIG );

        if( config == null )
        {
            config = ( GameObject ) Instantiate( Resources.Load( "CONFIG" ) );
            config.name = "_CONFIG";
        }

        return config.GetComponent< Config >();
    }

    static GameObject[] GameBullets = new GameObject[0];

    public static GameObject[] GetBullets()
    {
        return GameBullets;
    }

    public int bulletCount { get; private set; }

    public float gameTime, extraGameTime;

    public int gameScore { get; private set; }
    FibonacciObject timeScoreObject, bulletShotObject;

    const float MAX_PAUSE_TIME = 2.0f;
    float CurrentPauseTime;

    public bool GameIsActive { get { return CurrentPauseTime >= MAX_PAUSE_TIME; } }

    Random random;

    Config
        config;

    int hitCounter = 0;

    public bool IsActive
    {
        get
        {
            return GameScript.GetBullets().Length > 0;
        }
    }

    void Start()
    {
        Initialize();

        config = GameScript.GetConfig();

        Screen.lockCursor = true;

        random = new Random();

        CurrentPauseTime = MAX_PAUSE_TIME / 2.0f;
    }

    void Update()
    {
        if( GameIsActive )
        {
            if( IsActive )
            {
                gameTime += Time.deltaTime;
                extraGameTime += Time.deltaTime * ( (float) bulletCount / 3.0f );

                if( (int) gameTime / 5 >= timeScoreObject.GetIdentifier() )
                {
                    AddScore( timeScoreObject.GetNextValue() );
                }
                else if( (int) extraGameTime >= 1.0f )
                {
                    AddScore( 1 );
                    extraGameTime = 0.0f;
                }
            }

            if( Input.GetKey( KeyCode.Escape ) )
            {
                Screen.lockCursor = false;
                Quit();
            }
        }
        else
        {
            if( CurrentPauseTime < MAX_PAUSE_TIME / 2.0f )
            {
                CurrentPauseTime += 5.0f * Time.deltaTime;
            }
            else
            {
                CurrentPauseTime += Time.deltaTime;
            }
        }
    }

    public float GetAlphaBlocker()
    {
        float
            alpha;

        alpha = CurrentPauseTime / MAX_PAUSE_TIME;

        if( alpha > 0.5f )
        {
            alpha = 1.0f - ( ( alpha - 0.5f ) * 2.0f );
        }
        else if( alpha == 0.5f )
        {
            alpha = 0.0f;
        }
        else
        {
            alpha *= 2.0f;
        }

        return alpha;
    }

    void Quit()
    {
        Application.LoadLevel( "start_menu" );
    }

    void Initialize()
    {
        bulletCount = 0;
        gameTime = 0;
        gameScore = 0;
        extraGameTime = 1.0f;
        hitCounter = 0;

        CurrentPauseTime = 0.0f;

        timeScoreObject = new FibonacciObject();
        bulletShotObject = new FibonacciObject();

        GameBullets = new GameObject[0];
    }

    public void OnGameOver()
    {
        if( gameScore > config.highscore )
        {
            config.highscore = gameScore;
        }

        // "reset" game
        Initialize();

        var bullets = GameObject.FindGameObjectsWithTag( TAGS.BULLET );
 
        for( int i = 0; i < bullets.Length; ++i )
        {
            Destroy( bullets[ i ].gameObject );
        }
    }

    public void AddBullet()
    {
        ++bulletCount;
        GameBullets = GameObject.FindGameObjectsWithTag( TAGS.BULLET );
    }

    public void RemoveBullet()
    {
        --bulletCount;
        GameBullets = GameObject.FindGameObjectsWithTag( TAGS.BULLET );

        AddScore( 50 * ( timeScoreObject.GetIdentifier() + 1 ) );

        if( hitCounter++ > 5)
        {
            hitCounter = 0;
            AddScore( timeScoreObject.GetNextValue() );
        }
    }

    public void AddScore( int score )
    {
        gameScore += score;
    }

    public float GetRandomFloat( float min, float max )
    {
        return Random.Range( min, max );
    }

    public int GetRandomInt( int min, int max )
    {
        return Random.Range( min, max );
    }
}
