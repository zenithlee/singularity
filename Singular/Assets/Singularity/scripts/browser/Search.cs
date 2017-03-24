using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Singular
{

  [System.Serializable]
public class SearchResultData
{
  public string name;
  public string url;
  public string description;
  public string date;
  public string imgurl;
}

public class JsonHelper
{
  public static T[] getJsonArray<T>(string json)
  {
    string newJson = "{ \"array\": " + json + "}";
    Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
    return wrapper.array;
  }

  [System.Serializable]
  private class Wrapper<T>
  {
    public T[] array;
  }
}



public class Search : MonoBehaviour {

  public GameObject SearchPanel;
  public Slider ProgressBar;
  public GameObject ProgressPanel;
  string DefaultSearchURL = "http://bigfun.co.za/singular/flux/?f=search&term={term}&page={page}&numperpage={numperpage}";
  public GameObject CardProto;
  public GameObject ResultsLocator;

    public void Show()
    {
      SearchPanel.SetActive(true);
    }

  public void ListSites()
  {
    StartCoroutine(DoList());
  }

  IEnumerator DoList()
  {
    int page = 1;
    int num = 20;
    string site = DefaultSearchURL;
    site = site.Replace("{term}", "");
    site = site.Replace("{page}", page.ToString());
    site = site.Replace("{numperpage}", num.ToString());

    Debug.Log(site);

    WWW awww = new WWW(site);
    ProgressBar.value = awww.progress;    
    ProgressPanel.SetActive(true);
    while (!awww.isDone)
    {
      ProgressBar.value = awww.progress;
      yield return null;
    }
    ProgressPanel.SetActive(false);

    Debug.Log("Error:" + awww.error);
    Debug.Log("Result" + awww.text);
    //string result = WrapToClass(awww.text, "SearchCollection");
    string result = awww.text.Replace("\\\"","\"");
    SearchResultData[] objects = JsonHelper.getJsonArray<SearchResultData>(awww.text);
    Debug.Log(objects);
    CreateCards(objects);
  }

  void CreateCards(SearchResultData[] results)
  {
    foreach(SearchResultData r in results )
    {
      GameObject go = Instantiate(CardProto);
      go.transform.SetParent(ResultsLocator.transform);
      go.SetActive(true);
        go.transform.localScale = Vector3.one;
      ResultCard sr = go.GetComponent<ResultCard>();
        sr.Name.text = r.name;
        sr.Description.text = r.description;
        sr.URL.text = r.url;
    }
  }

  public string WrapToClass(string source, string topClass)
  {
    return string.Format("{{ \"{0}\": {1}}}", topClass, source);
  }

  public void SearchForSites(string term)
  {

  }
  
  // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

}