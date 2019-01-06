using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportBackScript : MonoBehaviour {
    AndroidJavaObject currentActivity;
    AndroidJavaClass UnityPlayer;
    // Use this for initialization
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }
    private void FixedUpdate()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            currentActivity.Call("closeActivity");
        }
#endif

    }
}
