using UnityEngine;
using System.Collections;

public class spinner : MonoBehaviour {

  public int SpeedX = 1;
  public int SpeedY = 1;
  public int SpeedZ = 1;

  // Use this for initialization
  void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    this.gameObject.transform.Rotate(SpeedX, SpeedY, SpeedZ);
	}
}
