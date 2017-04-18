  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  using Jurassic.Library;
  using Jurassic;

  namespace Singular {

    [System.Serializable]
    public class Scriptable : MonoBehaviour {

    public string File;
    [TextArea(3, 20)]
    public string GlobalDeclarations;
    [TextArea(13, 100)]
    public string Script;
    [TextArea(2, 4)]
    public string Result;

    GameObjectProxy GoInstance;
    ScriptEngine engine;    


    // Use this for initialization
    void Start () {
      //engine = Scripter.GetEngine();
      engine = new ScriptEngine();
      Scripter.Setup(engine);   
      GoInstance = new GameObjectProxy(engine);      

      engine.SetGlobalFunction("SetPos", new System.Action<double, double, double>(jsSetPos));
      //engine.SetGlobalFunction("Find", new System.Action<string>(jsFind));

      //CreateContext();
      engine.SetGlobalValue("self", GoInstance);
      engine.Execute(GlobalDeclarations);      
      engine.Execute(Script);      
      engine.Execute("Start()");

      InvokeRepeating("DoUpdate", 0.21f, 0.21f); 
      
    }

    void CreateContext()
    {

    }

    public void SetValue(string sName, object sValue)
    {
      engine.SetGlobalValue(sName, sValue);
    }

    public void Execute(string s)
    {
      //engine.Execute(Script);
      engine.Execute(s);      
    }

    void DoUpdate()
    {
      //engine.SetGlobalValue("self", GoInstance);
     // Debug.Log("Update");
      //engine.Execute(Script);
      engine.Execute("Update()");
    }

    // Update is called once per frame
    void Update () {
      //DoUpdate();
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