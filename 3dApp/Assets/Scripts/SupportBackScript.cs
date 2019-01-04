using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportBackScript : MonoBehaviour {

    private void FixedUpdate()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AndroidJavaClass unityplayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("closeActivity");
        }
#endif

    }
}
