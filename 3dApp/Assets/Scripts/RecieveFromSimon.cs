using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecieveFromSimon : MonoBehaviour {

	void GetString(string hej)
    {
        gameObject.GetComponent<Text>().text = hej;
    }
}
