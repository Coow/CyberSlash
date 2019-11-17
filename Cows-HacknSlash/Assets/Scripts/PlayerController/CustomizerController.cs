using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class CustomizerController : MonoBehaviour
{   
    [Header("Customization")]
	[SerializeField] private SkinnedMeshRenderer _renderer;
	[SerializeField] private GameObject meshToChange;
	[SerializeField] private Color Eyes, Body;

	//?uuid=49c7332a-06c9-11ea-90fd-001dd8b70034

    void Awake(){
		_renderer.materials[0].SetColor("_BaseColor", Body);
		_renderer.materials[1].SetColor("_BaseColor", Eyes);
		
    }

	void Start(){
		//StartCoroutine(GetRequest("https://api.cyberslash.net/user/v2/get-colors.php"));
	}


    IEnumerator GetRequest(string uri)
    {
		UnityWebRequest webRequest = UnityWebRequest.Get(uri);
		WWWForm form = new WWWForm();
		form.AddField("myField", "myData");

		//webRequest.useHttpContinue = false;
		//webRequest.chunkedTransfer = false;


		// Request and wait for the desired page.
		yield return webRequest.SendWebRequest();

		

		string[] pages = uri.Split('/');
		int page = pages.Length - 1;

		Debug.Log(webRequest.uploadedBytes);
		Debug.Log(webRequest.responseCode);

		if (webRequest.isNetworkError || webRequest.isHttpError)
		{
			Debug.Log(pages[page] + ": Error: " + webRequest.downloadHandler.text);
			Debug.Log(webRequest.downloadedBytes);
			Debug.Log(pages[page] + ": Error: " + webRequest.error);
		}
		else
		{
			Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
		}
        
    }


}
