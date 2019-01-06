using RestSharp;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetPdf : MonoBehaviour
{
    // Use this for initialization

    public List<PDF> pdfArrayName;
    public static List<byte[]> pdfArray;
    AndroidJavaClass unityplayer;
    AndroidJavaObject currentActivity;
    List<GameObject> plans;

    void Start()
    {
        pdfArray = new List<byte[]>();
        var client = new RestClient("http://floorplanner-env.bsux2m9paw.eu-central-1.elasticbeanstalk.com");
        var request = new RestRequest("projects/" + RecieveFromSimon.projectID, Method.GET, DataFormat.Json);
        IRestResponse response = client.Execute(request);
        RestSharp.Deserializers.JsonDeserializer deserializer = new RestSharp.Deserializers.JsonDeserializer();
        pdfArrayName = deserializer.Deserialize<List<PDF>>(response);
        unityplayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
        for (int i = 0; i < RecieveFromSimon.pngCount; i++)
        {
            pdfArray.Add(currentActivity.Call<byte[]>("getPNG", new object[] { i }));
        }
        Debug.Log("We got " + pdfArray.Count + " pngs from java");

        //plans = new List<GameObject>();
        //int j = 0;
        //foreach (byte[] plan in pdfArray)
        //{
        //    Texture2D planTex = new Texture2D(2, 2);
        //    bool isLoaded = planTex.LoadImage(plan);
        //    Debug.Log("Loaded image into 2d texture");
        //    if (isLoaded)
        //    {
        //        Debug.Log("Sprite loaded");
        //        plans.Add(new GameObject());
        //        plans.ElementAt(j).SetActive(false);
        //        plans.ElementAt(j).AddComponent<SpriteRenderer>();
        //        plans.ElementAt(j).name = "planSprite" + j;
        //        Debug.Log("Added spriterenderer");
        //        SpriteRenderer planRenderer = plans.ElementAt(j).GetComponent<SpriteRenderer>();
        //        Sprite planImage = Sprite.Create(planTex, new Rect(0, 0, planTex.width, planTex.height), new Vector2(0.5f, 0.5f));
        //        Debug.Log("Created sprite");
        //        planRenderer.sprite = planImage;
        //        plans.ElementAt(j).transform.SetParent(gameObject.transform);
        //        j++;

        //    }
        //    else
        //    {
        //        Debug.Log("Load image failed");
        //    }
        //    Debug.Log("There are " + plans.Count + "gameobject plans in the plans array");
        //}
    }


    //Texture2D RemoveColor(Color c, Texture2D imgs)
    //{
    //    Color[] pixels = imgs.GetPixels(0, 0, imgs.width, imgs.height, 0);

    //    Color newcol = new Color(1, 1, 1, 0); //tried various combos even Color.clear, currently replaces with white

    //    for (int p = 0; p < pixels.Length; p++)
    //    {
    //        if (pixels[p] == c)
    //            // pixels[p] = new Color(0,0,0,0);
    //            pixels[p] = newcol;
    //    }

    //    imgs.SetPixels(0, 0, imgs.width, imgs.height, pixels, 0);
    //    imgs.Apply();
    //    return imgs;
    //}

    public void HidePlans()
    {
        foreach (GameObject plan in plans)
        {
            plan.SetActive(false);
        }
    }

    public void ShowPlans()
    {
        foreach (GameObject plan in plans)
        {
            plan.SetActive(true);
        }
    }
}

