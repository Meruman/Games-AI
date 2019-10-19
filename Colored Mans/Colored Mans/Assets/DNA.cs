using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {

	//Gene for color
	public float r;
	public float g;
	public float b;

	//Gene of size
	public float scaleY;

	//Did they die?
	bool dead = false;

	public float timeToDie = 0;
	SpriteRenderer sRenderer;
	Collider2D sCollider;

	//To know if something has been clicked, we use this unity method
	void OnMouseDown()
	{
		dead = true;
		timeToDie = PopulationManager.elapsed;
		Debug.Log("Dead at: " + timeToDie);
		sRenderer.enabled = false;
		sCollider.enabled = false;
	}

	// Use this for initialization
	void Start () {
		//Start the render and collider2D
		sRenderer = GetComponent<SpriteRenderer>();
		sCollider = GetComponent<Collider2D>();
		sRenderer.color = new Color(r,g,b);
		this.transform.localScale = new Vector3(0.267065f,scaleY, 0.267065f);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
