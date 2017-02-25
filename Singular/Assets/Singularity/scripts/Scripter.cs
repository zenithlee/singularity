using UnityEngine;
using System.Collections;

using Jurassic.Library;
using Jurassic;
using UnityEngine.UI;

public class Scripter : MonoBehaviour {
 
  public ScriptEngine engine;
  bool running = false;
  string codeString = "Debug.Log(\"Hello World\");";
  Console console;
  public AppInfo app;

  void Awake()
  {
    engine = new ScriptEngine();
    engine.EnableExposedClrTypes = true;

    app = new AppInfo(engine);
    app.SetURL(GetComponent<Browser>().URL.text);
    engine.SetGlobalValue("App", app);

    engine.SetGlobalValue("Mathf", typeof(Mathf));
    engine.SetGlobalValue("Input", typeof(Input));
    engine.SetGlobalValue("Debug", typeof(Debug));
    engine.SetGlobalValue("GameObject", typeof(GameObject));
    engine.SetGlobalValue("PrimitiveType", typeof(PrimitiveType));

    engine.SetGlobalValue("Console", typeof(consoler));
    engine.SetGlobalValue("Vector", new jsVectorConstructor(engine));

    engine.SetGlobalFunction("SetPos", new System.Action<double, double, double>(jsSetPos));
  }
  // Use this for initialization
  void Start () {
	
	}  

  public void Execute(string s)
  {
    engine.Execute(s);
  }
	
	// Update is called once per frame
	void Update () {
    engine.SetGlobalValue("time", Time.time);
    //Execute the contents of the script every frame if Running is ticked.
    engine.CallGlobalFunction("Update");
    if (running)
    {      
     
    }

  }
  public double jsGetX() { return (double)transform.position.x; }
  public double jsGetY() { return (double)transform.position.y; }
  public double jsGetZ() { return (double)transform.position.z; }

  public void jsSetPos(double x, double y, double z)
  {
    transform.position = new Vector3((float)x, (float)y, (float)z);
  }
}
