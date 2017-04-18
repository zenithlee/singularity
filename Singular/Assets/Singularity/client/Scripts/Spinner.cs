using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

  public float SpeedX = 1;
  public float SpeedY = 1;
  public float SpeedZ = 1;

  // Use this for initialization
  void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    this.gameObject.transform.Rotate(SpeedX, SpeedY, SpeedZ);
	}
}
