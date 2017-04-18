using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermsConditions : MonoBehaviour {

  const string TERMS = "termsaccepted";  

  public void Accept()
  {
    PlayerPrefs.SetInt(TERMS, 1);
    gameObject.SetActive(false);
  }

  public void Decline()
  {
    Application.Quit();
  }
	// Use this for initialization
	void OnEnable () {
    int accepted = PlayerPrefs.GetInt(TERMS, 0);
    if ( accepted == 1 )
    {
      gameObject.SetActive(false);
    }
    else
    {
      gameObject.SetActive(true);
    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
