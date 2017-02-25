using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class Builder : EditorWindow {

  [MenuItem("Singular/Exporter")]
  public static void ShowWindow()
  {
    EditorWindow.GetWindow(typeof(Builder));
  }

  void OnGUI()
  {
    GUILayout.BeginHorizontal();
    if (GUILayout.Button("Clean") == true)
    {
      Clean();
    }
    if (GUILayout.Button("Build") == true)
    {
      Build();
    }
    GUILayout.EndHorizontal();
  }

  public void Clean()
  {
    string[] files = Directory.GetFiles("Assets/AssetBundles");
    foreach (var a in files)
    {
      if (( a != "." ) && (a != ".." )) { 
        File.Delete(a);
      }

    }
    AssetDatabase.Refresh();
  }


  public void Build()
  {
    Clean();

    AssetDatabase.Refresh();

    string path = "Assets/AssetBundles";
    BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

    string[] files = Directory.GetFiles("Assets/AssetBundles");
    foreach (var a in files)
    {
      if (!a.Contains("."))
      {
        File.Move(a, a + ".sing");
      }
    }

    //create index.html file
    File.WriteAllText(path + "/index.html", "<html><body>Sucks to be you, try viewing with Singular Browser</body></html>");
  }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
