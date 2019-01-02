using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecieveFromSimon : MonoBehaviour {
    bool hasExtra;
    AndroidJavaObject intent;
    Text TextBoxText;
    AndroidJavaObject extras;
    string arguments;
    // Use this for initialization
    void Start()
    {
        TextBoxText = gameObject.GetComponent<Text>();
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        intent = currentActivity.Call<AndroidJavaObject>("getIntent");
        hasExtra = intent.Call<bool>("hasExtra", "arguments");
        Debug.Log("start");
    }

   // Update is called once per frame
    void Update()
    {
        if (hasExtra)
        {
            Debug.Log("has extra");
            extras = intent.Call<AndroidJavaObject>("getExtras");
            arguments = extras.Call<string>("getString", "arguments");
            TextBoxText.text = arguments;
            Debug.Log(arguments);
        }
        else
        {
            TextBoxText.text = "No Extra from Android";
            Debug.Log("no extra");
        }
    }
}
