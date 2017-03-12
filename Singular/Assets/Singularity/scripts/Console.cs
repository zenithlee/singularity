using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Jurassic.Library;
using Jurassic;
using System;

//samples
// Console.Log(GameObject.Find("Browser").transform.position.x)
namespace Singular { 

public class Console : MonoBehaviour {

  public InputField ConsoleText;
  public static InputField s_ConsoleText;

  public InputField ConsoleInput;  

  // Use this for initialization
  void Start () {
    s_ConsoleText = ConsoleText;
	}

  public void Run()
  {
    ConsoleText.text += "\n";    
    try { 
      ConsoleText.text += Scripter.engine.Evaluate(ConsoleInput.text);
      ConsoleInput.text = "";
    } catch( Exception e )
    {
      Debug.Log(e);
      ConsoleText.text += e.ToString();
    }
    //Log(engine.SyntaxError.ToString());
    //Log(engine.ReferenceError.ToString());
  }

  void Log(string s)
  {
    Debug.Log(s);
    ConsoleText.text += s + "\n";
  }

  // Update is called once per frame
  void Update () {
	
	}
}


#region Implementation of our custom static JavaScript Day class
// This is the implementation of the JavaScript Day class which just returns next/prev 
// string from an array for the purpose of demonstrating a custom static class in JS.
public class consoler : ObjectInstance
{
  public consoler(ScriptEngine engine) 
		: base(engine)	    
	{
    //PopulateFunctions searches the class for JSFunction attributes and creates a function for each one it finds.
    this.PopulateFunctions();
  }

  [JSFunction(Name = "Log")]
  public static void Log(string n)
  {
      if ( Console.s_ConsoleText ) { 
      Console.s_ConsoleText.text += n + "\n";
      }
    }

  [JSFunction(Name = "Log")]
  public static void Log(UnityEngine.Object o)
  {
      if (Console.s_ConsoleText)
      {
        Console.s_ConsoleText.text += o.ToString() + "\n";
      }
  }

  [JSFunction(Name = "Log")]
  public static void Log(float o)
  {
      if (Console.s_ConsoleText)
      {
        Console.s_ConsoleText.text += o.ToString() + "\n";
      }
  }
}
  #endregion



}