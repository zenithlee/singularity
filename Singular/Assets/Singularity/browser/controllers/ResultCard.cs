using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Singular { 

public class ResultCard : MonoBehaviour {

    public Text URL;
    public Text Name;
    public Text Description;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnClick()
    {
      Debug.Log("GOTO:" + URL.text);
      SendMessageUpwards("Goto", URL.text);
    }
}

}