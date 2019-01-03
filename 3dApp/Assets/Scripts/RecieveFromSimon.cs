using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecieveFromSimon : MonoBehaviour {
    bool hasExtra;
    AndroidJavaObject intent;
    AndroidJavaObject extras;
    public static int projectID = 1;
    // Use this for initialization
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            intent = currentActivity.Call<AndroidJavaObject>("getIntent");
            hasExtra = intent.Call<bool>("hasExtra", "Projectid");
            if (hasExtra)
            {
                extras = intent.Call<AndroidJavaObject>("getExtras");
                projectID = extras.Call<int>("getInt", "Projectid");
                gameObject.GetComponent<Text>().text = projectID.ToString();
            }
        }
    }

}
