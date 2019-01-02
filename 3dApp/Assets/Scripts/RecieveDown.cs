using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecieveDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text text = gameObject.GetComponent<Text>();
        AndroidJavaClass unityplayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject current = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
        text.text = current.Call<string>("sayHi", new object[] { "Viktor" });
	}

}
