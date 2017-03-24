﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

  public GameObject[] Panels;

  public void HideAllPanels()
  {
    foreach( GameObject go in Panels )
    {
      go.SetActive(false);
    }
  }

  public void ShowPanel(string name)
  {
    Transform t = transform.Find(name);
  }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
