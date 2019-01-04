using RestSharp;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class GetPdf : MonoBehaviour {
    // Use this for initialization
    public List<PDF> pdfArray;
	void Start () {
        var client = new RestClient("http://floorplanner-env.bsux2m9paw.eu-central-1.elasticbeanstalk.com");
        var request = new RestRequest("projects/" + RecieveFromSimon.projectID, Method.GET, DataFormat.Json);

        IRestResponse response = client.Execute(request);
        RestSharp.Deserializers.JsonDeserializer deserializer = new RestSharp.Deserializers.JsonDeserializer();
        pdfArray = deserializer.Deserialize<List<PDF>>(response);
        
	}   
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                byte[] stueplan;
                byte[] soejler;
                if (Application.platform == RuntimePlatform.Android)
                {
                    AndroidJavaClass unityplayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject currentAct = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
                    string path1 = pdfArray[0].Path;
                    string path2 = pdfArray[1].Path;
                    stueplan = currentAct.Call<byte[]>("getPNG", new object[] { path1 });
                }
                else
                {
                    stueplan = File.ReadAllBytes(@"C:\Users\Viktor\Pictures\loftplan_gennemsigtig.png");
                    soejler = File.ReadAllBytes(@"C:\Users\Viktor\Pictures\stueplan_gennemsigtig.png");

                }


                Texture2D stueplanTex = new Texture2D(2, 2);
                //Texture2D soejlerTex = new Texture2D(2, 2);
                bool isLoaded = stueplanTex.LoadImage(stueplan);
                //soejlerTex.LoadImage(soejler);
                if (isLoaded)
                {
                    Debug.Log("Sprite loaded");
                    GameObject stueplanSprite = new GameObject();
                    //GameObject soejlerSprite = new GameObject();
                    stueplanSprite.AddComponent<SpriteRenderer>();
                    //soejlerSprite.AddComponent<SpriteRenderer>();
                    stueplanSprite.name = "StueplanSprite";
                    //soejlerSprite.name = "SoejlerSprite";
                    SpriteRenderer stueplanRenderer = stueplanSprite.GetComponent<SpriteRenderer>();
                    //SpriteRenderer soejlerRenderer = soejlerSprite.GetComponent<SpriteRenderer>();
                    Sprite stueplanImage = Sprite.Create(stueplanTex, new Rect(0, 0, stueplanTex.width, stueplanTex.height), new Vector2(0.5f, 0.5f));
                    //Sprite soejlerImage = Sprite.Create(soejlerTex, new Rect(0, 0, soejlerTex.width, soejlerTex.height), new Vector2(0.5f, 0.5f));
                    stueplanRenderer.sprite = stueplanImage;
                    //soejlerRenderer.sprite = soejlerImage;
                    GameObject.Instantiate<GameObject>(stueplanSprite, gameObject.transform);
                    //GameObject.Instantiate<GameObject>(soejlerSprite, gameObject.transform);
                }
                else
                {
                    Debug.Log("Load image failed");
                }
            }

        }
    }
    Texture2D RemoveColor(Color c, Texture2D imgs)
    {
        Color[] pixels = imgs.GetPixels(0, 0, imgs.width, imgs.height, 0);

        Color newcol = new Color(1, 1, 1, 0); //tried various combos even Color.clear, currently replaces with white

        for (int p = 0; p < pixels.Length; p++)
        {
            if (pixels[p] == c)
                // pixels[p] = new Color(0,0,0,0);
                pixels[p] = newcol;
        }

        imgs.SetPixels(0, 0, imgs.width, imgs.height, pixels, 0);
        imgs.Apply();
        return imgs;
    }

}
