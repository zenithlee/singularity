using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class SiteBuilder : EditorWindow {

  string SiteName = "";
  bool BuildWindows32 = true;
  //bool BuildWindows64 = true;
  bool BuildAndroid = true;
  const string BASEPATH = "Assets/AssetBundles";
  const string WINDOWS32PATH = "Assets/AssetBundles/PC";
  const string ANDROIDPATH = "Assets/AssetBundles/Android";

  [MenuItem("Singular/SiteBuilder")]
  public static void ShowWindow()
  {
    EditorWindow.GetWindow(typeof(SiteBuilder));
  }

  void OnGUI()
  {
    GUILayout.BeginHorizontal();
    SiteName = GUILayout.TextField(SiteName);

    if (GUILayout.Button("New Site") == true)
    {
      NewSite();
    }
    GUILayout.EndHorizontal();


    GUILayout.BeginHorizontal();
    if (GUILayout.Button("Clean") == true)
    {
      CleanFiles(WINDOWS32PATH);
      CleanFiles(ANDROIDPATH);
    }
    if (GUILayout.Button("Build Site") == true)
    {
      Build();
    }
    GUILayout.EndHorizontal();
  }

 

  void Upload()
  {

  }

  public void CleanFiles(string path)
  {
    string[] files = Directory.GetFiles(path);
    foreach (var a in files)
    {
      if (( a != "." ) && (a != ".." )) { 
        File.Delete(a);
      }

    }
    AssetDatabase.Refresh();
  }

  void RenameFiles(string path)
  {
    string[] files = Directory.GetFiles(path);
    foreach (var a in files)
    {
      if (!a.Contains("."))
      {
        File.Move(a, a + ".sing");
      }
    }
  }


  public void Build()
  {
    AssetDatabase.Refresh();
    if (!AssetDatabase.IsValidFolder(BASEPATH))
    {
      AssetDatabase.CreateFolder("Assets", "AssetBundles");
    }

    string path = "";
    if ( BuildWindows32) {       
      path = WINDOWS32PATH;
      CleanFiles(path);
      if (!AssetDatabase.IsValidFolder(path))
      {
        AssetDatabase.CreateFolder(BASEPATH, "PC");
      }
      BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
      RenameFiles(path);
    }

    if ( BuildAndroid ) { 
      path = ANDROIDPATH;
      CleanFiles(path);
      if (!AssetDatabase.IsValidFolder(path))
      {
        AssetDatabase.CreateFolder(BASEPATH, "Android");
      }
      BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.Android);
      RenameFiles(path);
    }

   

    //create index.html file
    File.WriteAllText(BASEPATH + "/index.html", "<html><body>Sucks to be you, try viewing with Singular Browser</body></html>");

    AssetDatabase.Refresh();
  }

  void NewSite()
  {
    string guid = AssetDatabase.CreateFolder("Assets", SiteName);

    guid = AssetDatabase.CreateFolder("Assets/"+SiteName, "assets");
    string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
    Debug.Log("New Site:" + SiteName + ", Site Assets Folder:" + newFolderPath);

    //string stringpath = AssetDatabase.GetAssetPath(SiteName);
    AssetImporter assetImporter = AssetImporter.GetAtPath(newFolderPath);
    assetImporter.assetBundleName = "assets";


    AssetDatabase.Refresh();
  }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
