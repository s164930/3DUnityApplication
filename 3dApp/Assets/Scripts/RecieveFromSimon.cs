using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecieveFromSimon : MonoBehaviour {
    bool hasExtra;
    AndroidJavaObject intent;
    AndroidJavaObject extras;
    public static string projectID = "1";
    // Use this for initialization
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            intent = currentActivity.Call<AndroidJavaObject>("getIntent");
            hasExtra = intent.Call<bool>("hasExtra", "arguments");
            if (hasExtra)
            {
                extras = intent.Call<AndroidJavaObject>("getExtras");
                projectID = extras.Call<string>("getString", "arguments");
            }
        }
    }

}
