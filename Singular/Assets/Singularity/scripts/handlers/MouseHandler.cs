using UnityEngine;
using System.Collections;

namespace Singular { 
public class MouseHandler : MonoBehaviour {

  Scripter scripter;
  // Use this for initialization
  void Start () {
    scripter = GetComponent<Scripter>();
  }
	
	// Update is called once per frame
	void Update () {
    CheckMouse();
  }

  void CheckMouse()
  {
    if (Input.GetMouseButtonDown(0))
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      // var select = GameObject.FindWithTag("select").transform;
      if (Physics.Raycast(ray, out hit, 1000000.0f))
      {
        // scripter.running = true;
        //select.tag = "none";
        //hit.collider.transform.tag = "select";
        //Debug.Log("Clicked");
        scripter.SetValue("Selected", hit.collider.gameObject);
        scripter.Execute("OnClick()");
      }
    }
  }
}

}