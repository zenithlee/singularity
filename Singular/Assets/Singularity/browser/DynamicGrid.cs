using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGrid : MonoBehaviour {

  public int Rows = 3, Columns = 3;

	// Use this for initialization
	void OnEnable () {
    UpdateLayout();
	}

  public void UpdateLayout()
  {
    RectTransform parent = GetComponent<RectTransform>();
    GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
    grid.cellSize = new Vector2(parent.rect.width / (float)Columns, parent.rect.height / (float)Rows);
  }
	
	// Update is called once per frame
	void Update () {
		
	}
}
