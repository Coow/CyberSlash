using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

// https://forum.unity.com/threads/unitywebrequest-and-https-not-working.539601/

public class CustomizerController : MonoBehaviour
{   
    [Header("Customization")]
	[SerializeField] private SkinnedMeshRenderer _renderer;
	[SerializeField] private GameObject meshToChange;
	[SerializeField] private Color Eyes, Body;
	private Color _eyes, _body;

	//?uuid=49c7332a-06c9-11ea-90fd-001dd8b70034

    void Awake(){
		//_renderer.materials[0].SetColor("_BaseColor", Body);
		//_renderer.materials[1].SetColor("_BaseColor", Eyes);
		
    }

	void Start(){
		StartCoroutine(GetRequest("http://api.cyberslash.net/user/v2/get-colors.php?uuid=49c7332a-06c9-11ea-90fd-001dd8b70034"));
	}


    IEnumerator GetRequest(string uri)
    {
		UnityWebRequest webRequest = UnityWebRequest.Get(uri);
		
		webRequest.SetRequestHeader("Content-Type", "application/json");
		webRequest.useHttpContinue = true;

		// Request and wait for the desired page.
		yield return webRequest.SendWebRequest();

		string[] pages = uri.Split('/');
		int page = pages.Length - 1;

		//Debug.Log(webRequest.uploadedBytes + " bytes uploaded.");
		//Debug.Log(webRequest.responseCode + " response code");

		if (webRequest.isNetworkError || webRequest.isHttpError)
		{
			//Debug.Log(pages[page] + ": Error: " + webRequest.downloadHandler.text);
			//Debug.Log(webRequest.downloadedBytes + " bytes downloaded.");
			//Debug.Log(pages[page] + ": Error: " + webRequest.error);
		}
		else
		{
			//Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
			JSONObject json = new JSONObject(webRequest.downloadHandler.text);
			//Debug.Log(json[0].GetField("eyes").str);
			//Debug.Log(json[0].GetField("body").str);

			SetColorFromAPI(json[0].GetField("eyes").str, json[0].GetField("body").str);
		}
        
    }

	void SetColorFromAPI(string _eyeColor, string _bodyColor){
		ColorUtility.TryParseHtmlString("#"+_eyeColor  ,out _eyes);
		ColorUtility.TryParseHtmlString("#"+_bodyColor ,out _body);

		_renderer.materials[0].SetColor("_BaseColor", _body);
		_renderer.materials[1].SetColor("_BaseColor", _eyes);	
		Debug.Log("changed colors from API");	
	}


}
