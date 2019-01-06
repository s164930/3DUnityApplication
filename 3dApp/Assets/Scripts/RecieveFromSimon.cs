using UnityEngine;
using UnityEngine.UI;

public class RecieveFromSimon : MonoBehaviour {
    bool hasExtra;
    AndroidJavaObject intent;
    AndroidJavaObject extras;
    public static int projectID = 1;
    public static int pngCount;
    // Use this for initialization
    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            intent = currentActivity.Call<AndroidJavaObject>("getIntent");
            hasExtra = intent.Call<bool>("hasExtra", "pngCount");
            if (hasExtra)
            {
                Debug.Log("There are extras in the intent, from unity");
                extras = intent.Call<AndroidJavaObject>("getExtras");
                pngCount = extras.Call<int>("getInt", "pngCount");
                projectID = extras.Call<int>("getInt", "Projectid");
            }
        }
    }

}
