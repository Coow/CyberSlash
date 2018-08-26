using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EasyFadeObstruction : MonoBehaviour {
	public GameObject playerObject;
	public float fadeRadius = 0.5f;
	public float fadeDistanceOffset = 0;
	public LayerMask fadeMaskLayer;
	public float fadeInSpeed = 10f;
	public float fadeOutSpeed = 10f;
	[Range(0.0f, 1.0f)]
	public float fadeTransparency;

	HashSet<GameObject> fadeSet;
	List<GameObject> toRemove;
	HashSet<Renderer> fadingBack;
	List<Renderer> removeFadingBack;

	void Start () {
		fadeSet = new HashSet<GameObject>();
		toRemove = new List<GameObject>();
		fadingBack = new HashSet<Renderer>();
		removeFadingBack = new List<Renderer>();

	}

	void Awake(){
		fadeSet = new HashSet<GameObject>();
		toRemove = new List<GameObject>();
		fadingBack = new HashSet<Renderer>();
		removeFadingBack = new List<Renderer>();
	}
	
	
	
	Color tempColor;
	void makeTransparent(Renderer r){
		tempColor = r.material.color;
		tempColor.a = Mathf.Lerp(tempColor.a, fadeTransparency, Time.deltaTime * fadeOutSpeed);
		r.material.color = tempColor;
	}
	
	void makeSolid(Renderer r){

		fadingBack.Add(r);
		/*tempColor = r.material.color;
		tempColor.a = 1f;
		r.material.color = tempColor;*/
	}

	void fadeBackObjects(){
		Color c;
		if(fadingBack != null){
			foreach(Renderer r in fadingBack){	
				try{
					if(!fadeSet.Contains(r.gameObject)){
						c = r.material.color;
						c.a = Mathf.Lerp(c.a, 1.0f, Time.deltaTime * fadeInSpeed);
						r.material.color = c;
						if(1.0 - c.a <= 0.1f){
							c.a = 1.0f;
							r.material.color = c;
							removeFadingBack.Add(r);
						}
					}
				}catch(MissingReferenceException mre){
					//No need to announce the exception, reference will be cleared from the to be removed list
					mre.GetType();
					fadingBack.Remove(r);
				}

			}
			foreach(Renderer r in removeFadingBack){
				fadingBack.Remove(r);
			}
			removeFadingBack.Clear();
		}
	}
	


	Vector3 direction;
	float distance;
	bool exists = false;
	void fadeBlockingObjects(){

		try{
			direction = playerObject.transform.position - transform.position;
		}catch(UnassignedReferenceException e){
			Debug.LogWarning(e.GetType()+": GameObject not assigned to playerObject variable in EasyFadeObstruction.");
			this.enabled = false;
		}
		distance = direction.magnitude - fadeRadius + fadeDistanceOffset;
		
		RaycastHit[] hits = Physics.SphereCastAll( new Ray(transform.position, direction), fadeRadius, distance, fadeMaskLayer);
			//RaycastAll(transform.position, direction , distance, fadeMaskLayer);
		try{
		//if there is nothing being hit, clear fade set
		if(hits.Length > 0){
			foreach(RaycastHit h in hits){
				if(h.collider.gameObject.GetComponent<Renderer>() != null){
					fadeSet.Add(h.collider.gameObject);
					//h.collider.gameObject.GetComponent<Renderer>().enabled = false;
					makeTransparent(h.collider.gameObject.GetComponent<Renderer>());
				}
			}
			if(fadeSet != null){
				//for every object that exists, add it to the fade set
				foreach(GameObject g in fadeSet){
					foreach(RaycastHit h in hits){
						if(g.Equals(h.collider.gameObject)){
							//	Debug.Log(g.name+"found in racasts");
							exists = true;
							break;
						}else{
							exists = false;
						}
					}
					//if it doesn't exist, re enable the renderer and add it the the list of items to be removed
					if(!exists){
						//Debug.Log(g.name+"removed");
						//g.GetComponent<Renderer>().enabled = true;
						if(g != null){
							makeSolid(g.GetComponent<Renderer>());
						}
						toRemove.Add(g);
					}
				}
				//remove then clear the list of items to be removed
				foreach(GameObject g in toRemove){
					fadeSet.Remove(g);
				}
				toRemove.Clear();
			}
		}else{
			if(fadeSet != null){
				if(fadeSet.Count > 0){
					//if there is nothing being hit, we clear the fade set and re enable the renderers
					foreach(GameObject g in fadeSet){
						//g.GetComponent<Renderer>().enabled = true;
						if(g != null){
							makeSolid(g.GetComponent<Renderer>());
						}
						toRemove.Add(g);
					}
					//remove then clear the list of items to be removed
					foreach(GameObject g in toRemove){
						fadeSet.Remove(g);
					}
					toRemove.Clear();
				}
			}
		}
		}catch(UnityException e){
			e.ToString();
			fadeSet.Clear(); 
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		fadeBlockingObjects();
		fadeBackObjects();
	}
}
