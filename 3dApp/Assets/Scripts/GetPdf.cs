using RestSharp;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class GetPdf : MonoBehaviour {
    List<PDF> pdfArray = new List<PDF>();

	// Use this for initialization
	void Start () {
        var client = new RestClient("http://floorplanner-env.bsux2m9paw.eu-central-1.elasticbeanstalk.com");
        var request = new RestRequest("projects/" + RecieveFromSimon.projectID, Method.GET, DataFormat.Json);

        IRestResponse response = client.Execute(request);
        RestSharp.Deserializers.JsonDeserializer deserializer = new RestSharp.Deserializers.JsonDeserializer();
        pdfArray = deserializer.Deserialize<List<PDF>>(response);
        
	}   
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            using (var client = new WebClient())
            {
                foreach (PDF pdf in pdfArray)
                {
                    client.DownloadFile(pdf.Path, "C:\\Users\\Viktor\\Desktop\\" + pdf.Filename + ".pdf");
                }
                
            }
        }
	}
}
