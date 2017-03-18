  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  using Jurassic.Library;
  using Jurassic;

  namespace Singular {

    [System.Serializable]
    public class Scriptable : MonoBehaviour {

    public string File;
    [TextArea(13, 100)]
    public string Script;
    [TextArea(5, 5)]
    public string Result;

    GameObjectProxy GoInstance;

	
    // Use this for initialization
    void Start () {
      ScriptEngine engine = Scripter.GetEngine();
      GoInstance = new GameObjectProxy(engine);
      

      engine.SetGlobalFunction("SetPos", new System.Action<double, double, double>(jsSetPos));
      //engine.SetGlobalFunction("Find", new System.Action<string>(jsFind));

      

      //CreateContext();
      engine.Execute(Script);
      engine.SetGlobalValue("self", GoInstance);
      engine.Execute("Start()");
      
    }

    void CreateContext()
    {

    }

    public void DoScript(string s)
    {
      Scripter.GetEngine().Execute(s);
    }
	
    // Update is called once per frame
    void Update () {
      ScriptEngine engine = Scripter.GetEngine();
      engine.SetGlobalValue("self", GoInstance);
      engine.Execute(Script);
      engine.Execute("Update()");
    }

    public double jsGetX() { return (double)transform.position.x; }
    public double jsGetY() { return (double)transform.position.y; }
    public double jsGetZ() { return (double)transform.position.z; }

    public void jsSetPos(double x, double y, double z)
    {
      transform.position = new Vector3((float)x, (float)y, (float)z);
    }

    public GameObject jsFind(string s)
    {
      return GameObject.Find(s);
    }

    }

  }