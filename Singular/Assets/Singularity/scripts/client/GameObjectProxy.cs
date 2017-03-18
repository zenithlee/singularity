using UnityEngine;
using Jurassic;
using Jurassic.Library;

namespace Singular { 
public class GameObjectProxy : ObjectInstance
{

    public string _name = "hello";
    public GameObject _proxy;

  public GameObjectProxy(ScriptEngine engine)
      : base(engine)
  {
    this.PopulateFunctions();
  }

  [JSFunction(Name = "Find")]
  public GameObjectProxy Find(string name)
  {      
      GameObjectProxy fo = new GameObjectProxy(this.Engine);
      fo._proxy = GameObject.Find(name);
      return fo;
  }

    [JSFunction(Name = "Debug")]
    public void Debug(object o)
    {
      UnityEngine.Debug.Log(o);    
    }

    [JSFunction(Name = "Proxy")]
    public GameObject Proxy()
    {
      return _proxy; ;
    }
  }

}