﻿using System.IO;
using UnityEngine;
using System.Collections;

using Jurassic.Library;
using Jurassic;
using UnityEngine.UI;

namespace Singular { 
public class Scripter : MonoBehaviour {
 
  public static ScriptEngine _engine;
  public bool running = false;
  string codeString = "Debug.Log(\"Hello World\");";
  Console console;
  public static AppInfo app;
  public TextAsset TestScript;

  public static ScriptEngine GetEngine()
  {
      //Scripter.engine = null;
      if (Scripter._engine == null)
      {
        Debug.Log("Initialising Script Engine");
        Scripter.Setup(new ScriptEngine());        
      }
      return Scripter._engine;      
  }

    public static void Reset()
    {
      Scripter._engine = null;
    }

    public static void Setup(ScriptEngine engine)
    {      
      engine.EnableExposedClrTypes = true;
      //engine.EnableDebugging = true;

      app = new AppInfo(engine);      
      engine.SetGlobalValue("App", app);
      engine.SetGlobalValue("Selected", 0);

      engine.SetGlobalValue("Mathf", typeof(Mathf));
      engine.SetGlobalValue("Input", typeof(Input));
      engine.SetGlobalValue("Debug", typeof(Debug));
      //engine.SetGlobalValue("GameObject", new GameObjectProxy(engine));
      engine.SetGlobalValue("GameObject", typeof(GameObject));
      //engine.SetGlobalValue("GameObject", new GameObjectProxy(engine));
      engine.SetGlobalValue("Input", typeof(Input));
      engine.SetGlobalValue("PrimitiveType", typeof(PrimitiveType));

      engine.SetGlobalValue("Console", typeof(consoler));
      engine.SetGlobalValue("Vector", new jsVectorConstructor(engine));
      engine.SetGlobalValue("Vector3", typeof(Vector3));

      // engine.SetGlobalFunction("SetPos", new System.Action<double, double, double>(jsSetPos));
      //engine.SetGlobalFunction("GetObjectByName", new System.Action<string>(GetObjectByName));


    }

    public static GameObject GetObjectByName(string s)
    {
      return GameObject.Find(s);
    }

  void Awake()
  {      
      Scripter.GetEngine();
      Browser b = GetComponent<Browser>();
      app.SetURL(b.URL.text);
      //LoadTestScript();
  }

  void LoadTestScript()
  {
    //engine.Execute(codeString);
    //string s = File.ReadAllText("Assets/Singularity/scripts/client/main.sing");
    _engine.Execute(TestScript.text);
    //engine.Execute("OnClick()");    
  }

  // Use this for initialization
  void Start () {
	
	}  

  public void SetValue(string variable, object o)
  {
    _engine.SetGlobalValue(variable, o);
  }

  public void Execute(string s)
  {
      GetEngine().Execute(s);
  }

    public static void Pulse()
    {
      GetEngine().SetGlobalValue("time", Time.time);
      //GetEngine().Execute("Update()");      
    }
	
	// Update is called once per frame
	void Update () {
      Scripter.Pulse();
  }  
}

}