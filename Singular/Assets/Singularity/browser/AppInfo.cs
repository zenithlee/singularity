using UnityEngine;
using Jurassic;
using Jurassic.Library;

public class AppInfo : ObjectInstance
{
  string Version = "1.0";
  string Platform = Application.platform.ToString();

  public AppInfo(ScriptEngine engine)
      : base(engine)
  {
    // Read-write property (name).
    this["Name"] = "Singular";

    // Read-only property (version).
    this.DefineProperty("Version", new PropertyDescriptor(Version, PropertyAttributes.Sealed), true);    
    this.DefineProperty("Platform", new PropertyDescriptor(Platform, PropertyAttributes.Sealed), true);
  }

  public void SetURL(string s)
  {
    this["URL"] = s;
  }
}