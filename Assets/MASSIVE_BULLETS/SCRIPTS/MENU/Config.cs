using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using System;

public class Config : MonoBehaviour
{
    const string
        HAND_HANDLE = "MASSIVE_BULLET_PF_STATE_HAND",
        KEYBOARD_HANDLE = "MASSIVE_BULLET_PF_STATE_KEYBOARD",
        HIGHSCORE_HANDLE = "MASSIVE_BULLET_PF_HIGHSCORE";

    HAND_STATE _HandState;
    public HAND_STATE
        HandState
        {
            set
            {
                PlayerPrefs.SetInt( HAND_HANDLE, ( int ) value );
                _HandState = value;
            }
            get
            {
                return _HandState;
            }
        }

    KEYBOARD_STATE _KeyboardState;
    public KEYBOARD_STATE
        KeyboardState
        {
            set
            {
                PlayerPrefs.SetInt( KEYBOARD_HANDLE, ( int ) value );
                _KeyboardState = value;
            }
            get
            {
                return _KeyboardState;
            }
        }

    int _highscore;
    public int
        highscore
        {
            set
            {
                PlayerPrefs.SetInt( HIGHSCORE_HANDLE, value );
                _highscore = value;
            }
            get
            {
                return _highscore;
            }
        }

    public bool
        FacebookIsInitialized { get; private set; }

    static public Texture
        FacebookSelfie { get; private set; }

    static public string
        FacebookName { get; private set; }

    public struct ScoreInformation
    {
        public int score;
        public string name;
        public Texture picture;
    };

    public List<ScoreInformation> HighScores { get; private set; }

    void Start ()
    {
        _HandState =
            ( HAND_STATE ) PlayerPrefs.GetInt(
                HAND_HANDLE, 2
                );

        _KeyboardState =
            ( KEYBOARD_STATE ) PlayerPrefs.GetInt(
                KEYBOARD_HANDLE, 2
                );

        _highscore = PlayerPrefs.GetInt(
            HIGHSCORE_HANDLE, 0
            );

        HighScores = new List<ScoreInformation>();

        if( FacebookSelfie == null )
        {
            FacebookSelfie = ( Texture ) Instantiate( Resources.Load( "DEFAULT_PROFILE_PIC" ) );
            FacebookName = "Ranger \"Glen\"";

            // enable when publishing to fb
            // FacebookIsInitialized = false;
            // FB.Init(OnInitComplete, OnHideUnity);
        }
    }

    public float GetLateralMovement()
    {
        float
            movement;

        if( KeyboardState == KEYBOARD_STATE.AZERTY )
        {
            movement = Input.GetKey( KeyCode.Z ) ? 1.0f : 0.0f;
        }
        else
        {
            movement = Input.GetKey( KeyCode.W ) ? 1.0f : 0.0f;
        }

        return movement - ( Input.GetKey( KeyCode.S ) ? 1.0f : 0.0f );
    }

    public float GetStrafeMovement()
    {
        float
            movement;

        if( KeyboardState == KEYBOARD_STATE.AZERTY )
        {
            movement = Input.GetKey( KeyCode.Q ) ? -1.0f : 0.0f;
        }
        else
        {
            movement = Input.GetKey( KeyCode.A ) ? -1.0f : 0.0f;
        }

        return movement + ( Input.GetKey( KeyCode.D ) ? 1.0f : 0.0f );
    }

    void OnInitComplete()
    {
        FacebookIsInitialized = true;
        FB.Login("email,publish_actions", LoginCallback);
    }

    void LoginCallback(FBResult result)                                                        
    {                                                                                          
        Util.Log("LoginCallback");                                                          

        if (FB.IsLoggedIn)                                                                     
        {                                                                                      
            OnLoggedIn();                                                                      
        }                                                                                      
    }                                                                                          

    void OnHideUnity(bool isGameShown)
    {
        Debug.Log("Is game showing? " + isGameShown);
    }

    void APICallback(FBResult result)
    {
        Util.Log("APICallback");
        if (result.Error != null)
        {
            Util.LogError(result.Error);
            // Let's just try again
            FB.API("/me?fields=id,first_name,friends.limit(100).fields(first_name,id)", Facebook.HttpMethod.GET, APICallback);
            return;
        }

        var profile = Util.DeserializeJSONProfile(result.Text);
        FacebookName = "Ranger \"" + profile["first_name"] + "\"";
    }

    void OnLoggedIn()
    {
        Util.Log("Logged in. ID: " + FB.UserId);

        // Reqest player info and profile picture    
        FB.API("/me?fields=id,first_name,friends.limit(100).fields(first_name,id)", Facebook.HttpMethod.GET, APICallback);  
        LoadPicture(Util.GetPictureURL("me", 256, 256), MyPictureCallback);

        retrieveHighscores();
    }

    delegate void LoadPictureCallback (Texture texture);

    IEnumerator LoadPictureEnumerator(string url, LoadPictureCallback callback)
    {
        WWW www = new WWW(url);
        yield return www;
        callback(www.texture);
    }

    void LoadPicture (string url, LoadPictureCallback callback)
    {
        FB.API(url,Facebook.HttpMethod.GET,result =>
        {
            if (result.Error != null)
            {
                Util.LogError(result.Error);
                return;
            }

            var imageUrl = Util.DeserializePictureURLString(result.Text);

            StartCoroutine(LoadPictureEnumerator(imageUrl,callback));
        });
    }

    void MyPictureCallback(Texture texture)
    {
        Util.Log("MyPictureCallback");

        if (texture == null)
        {
            // Let's just try again
            LoadPicture(Util.GetPictureURL("me", 256, 256), MyPictureCallback);
            return;
        }

        FacebookSelfie = texture;
    }

    public void retrieveHighscores()
    {
        FB.API("/app/scores?fields=score,user.limit(10)", Facebook.HttpMethod.GET, ScoresCallback);
    }

    private int getScoreFromEntry(object obj)
    {
        Dictionary<string,object> entry = (Dictionary<string,object>) obj;
        return Convert.ToInt32(entry["score"]);
    }

    void ScoresCallback(FBResult result) 
    {
        Debug.Log("ScoresCallback");
        if (result.Error != null)
        {
            Debug.LogError(result.Error);
            return;
        }

        HighScores = new List<ScoreInformation>();
        List<object> scoresList = Util.DeserializeScores(result.Text);

        foreach(object score in scoresList) 
        {
            ScoreInformation
                scoreInfo = new ScoreInformation();

            var entry = (Dictionary<string,object>) score;
            var user = (Dictionary<string,object>) entry["user"];

            string userId = (string)user["id"];

            scoreInfo.name = (string)user["first_name"];
            scoreInfo.score = getScoreFromEntry(entry);

            LoadPicture(Util.GetPictureURL(userId, 128, 128),pictureTexture =>
            {
                if (pictureTexture != null)
                {
                    scoreInfo.picture = pictureTexture;
                }
            });

            HighScores.Add( scoreInfo );
        }
    }
}
