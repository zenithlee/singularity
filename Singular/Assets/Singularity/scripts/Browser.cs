using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace Singular { 

public class Browser : MonoBehaviour {

    string DefaultHomeURL = "FLUX";

  public InputField URL;
  public GameObject ConsolePanel;
  public GameObject ContentHolder;    

    public GameObject ProgressPanel;
    public Slider ProgressBar;
    public GameObject TermsPanel;

  AssetBundle abundle;
  
  // Use this for initialization
  void Start () {
      ProgressPanel.SetActive(false);
      TermsPanel.SetActive(true); //will trigger an acceptance check
    }
	
	// Update is called once per frame
	void Update () {
    CheckKeys();
    
  }

  void CheckKeys()
  {
    if ( Input.GetKeyUp(KeyCode.F12))
    {
      ConsolePanel.SetActive(true);
    }
  }

    public void HideAllPanels()
    {
      ConsolePanel.SetActive(false);
      ProgressPanel.SetActive(false);
    }

 public void Goto(string newURL)
    {
      URL.text = newURL;
      Go3D();

    }

  public void Go()
  {    
    StartCoroutine(Get2DPage());
  }
    void ShowSearch()
    {
      Search s = GetComponent<Search>();
      s.Show();
    }

  public void Go3D()
  {
      string url = URL.text;
      if (( url.ToLower() == "flux" ) || ((url.ToLower() == "search")))
      {
        ShowSearch();
      }      
      if ( url.StartsWith("http")) { 
        Scripter.app.SetURL(URL.text);
        StartCoroutine(Get3D(URL.text));
      }
    }

   public void GoHome()
    {      
      string hom = PlayerPrefs.GetString("homeurl", DefaultHomeURL);
      URL.text = hom;
      Scripter.app.SetURL(hom);
      StartCoroutine(Get3D(hom));
    }

  void ClearContent()
  {
    if ( ContentHolder.transform.childCount > 0) { 
      Transform to = ContentHolder.transform.GetChild(0);
      GameObject.Destroy(to.gameObject);
    }
  }

  IEnumerator Get3D(string site)
  {
    if ( abundle != null )
    {
      abundle.Unload(true);
    }
    WWW awww = new WWW(site + "/assets.sing");
      ProgressBar.value = awww.progress;
      Debug.Log(awww.progress);
      ProgressPanel.SetActive(true);
      while (!awww.isDone)
      {
        ProgressBar.value = awww.progress;
        yield return null;
      }
      ProgressPanel.SetActive(false);
    Debug.Log(awww.error);
    abundle = awww.assetBundle;
    if ( abundle ) {
      ClearContent();
      Debug.Log("Found assets");
      Object[] assets =abundle.LoadAllAssets();
      Object o = abundle.LoadAsset("index");
      GameObject go = Instantiate(o, Vector3.zero, Quaternion.identity) as GameObject;
      go.transform.parent = ContentHolder.transform;
      string m = abundle.LoadAsset("main").ToString();
      Debug.Log("Main:" + m);
      GetComponent<Scripter>().Execute(m);
    }
    Debug.Log("Done");
    /*
    WWW www = new WWW(URL.text + "/assets");
    yield return www;
    Debug.Log("Error:"+www.error);
    AssetBundle bundle = www.assetBundle;
    //Instantiate(getMainAsset(bundle));
    //GameObject go = bundle.LoadAsset("index", typeof(GameObject)) as GameObject;    
    string[] scenes = bundle.GetAllScenePaths();
    var async = SceneManager.LoadSceneAsync(System.IO.Path.GetFileNameWithoutExtension(scenes[0]), LoadSceneMode.Additive);
    //SceneManager.LoadSceneAsync(scenes[0]);
    //bundle.Unload(false);
    */

  }

  public static UnityEngine.Object getMainAsset(AssetBundle assetBundle)
  {
    string[] names = assetBundle.GetAllAssetNames();    

   
    return assetBundle.LoadAsset(names[0]);
  }

  IEnumerator Get2DPage()
  {
    WWW www = new WWW(URL.text);
    yield return www;
    if ( www.error != "" )
    {
      Debug.Log(www.error);
    }

    Debug.Log(www.text);
  }
}

}