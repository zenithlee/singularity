using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Singular { 

public class RegisterSite : MonoBehaviour {

    string url = "http://bigfun.co.za/singular/flux/?f=register&name={name}&ownerid={ownerid}&url={url}&description={description}";
    public Text SiteName;
    public Text SiteURL;
    public Text UserID;
    public Text Description;
    public GameObject SuccessPanel;
    public GameObject FailPanel;
    public Text FailText;

    public void DoRegister()
    {
      StartCoroutine(Register());
    }

  IEnumerator Register()
  {
      if ( SiteName.text == "" )
      {
        Fail("No Site Name");
        yield break;
      }
      string full = url.Replace("{name}", SiteName.text);

      if ( SiteURL.text == "" )
      {
        Fail("No Site URL");
        yield break;
      }
      full = full.Replace("{url}", WWW.EscapeURL(SiteURL.text));

      if ( UserID.text == "" )
      {
        Fail("No User ID / Email");
        yield break;
      }
      full = full.Replace("{ownerid}", WWW.EscapeURL(UserID.text));

      PlayerPrefs.SetString("ownerid", UserID.text);

      //if (Description.text == "")
      //{
        //Fail("Description");
        //yield break;
      //}
      full = full.Replace("{description}", WWW.EscapeURL(Description.text));

      WWW www = new WWW(full);
      yield return www;

      Debug.Log(www.error);
      if ( string.IsNullOrEmpty(www.error))
      {
        SuccessPanel.gameObject.SetActive(true);
      }
      else
      {
        Fail(www.error);
      }
      Debug.Log(www.text);
  }

    void Fail(string s)
    {
      FailPanel.gameObject.SetActive(true);
      FailText.text = s;
    }
  // Use this for initialization
  void Start () {
      UserID.text = PlayerPrefs.GetString("ownerid", "");
      SuccessPanel.gameObject.SetActive(false);
      FailPanel.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

}