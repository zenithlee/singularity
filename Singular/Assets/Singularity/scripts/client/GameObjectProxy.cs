using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jurassic;
using Jurassic.Library;

namespace Singular {

  public class GameObjectProxy : ObjectInstance
{

    public GameObject myObject;

  public GameObjectProxy(ScriptEngine prototype)
      : base(prototype)
    {
      this.PopulateFunctions();      
    }

    [JSFunction(Name = "Find")]
    public static GameObject Find(string s)
    {
      return GameObject.Find(s);
    }
    
  }

}