using Jurassic;
using Jurassic.Library;

public class AppInfo : ObjectInstance
{
  string Version = "1.0";  

  public AppInfo(ScriptEngine engine)
      : base(engine)
  {
    // Read-write property (name).
    this["Name"] = "Singular";

    // Read-only property (version).
    this.DefineProperty("Version", new PropertyDescriptor(Version, PropertyAttributes.Sealed), true);
  }

  public void SetURL(string s)
  {
    this["URL"] = s;
  }
}