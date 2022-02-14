using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour {

	Vector3 ballStartPosition;
	Rigidbody2D rb;
	float speed = 700;
	public AudioSource blip;
	public AudioSource blop;
	public int scoreRight = 0;
	public int scoreLeft = 0;
	public static float elapsed = 0;

	GUIStyle guiStyle = new GUIStyle();
    void OnGUI()
    {
        guiStyle.fontSize = 25;
        guiStyle.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(10,10,250,150));
        GUI.Box(new Rect(0,0,140,140), "Stats", guiStyle);
        GUI.Label(new Rect(10, 25, 200, 30), "Human Score: " + scoreLeft);
        GUI.Label(new Rect(10, 50, 200, 30), "AI Score: " + scoreRight);
        GUI.Label(new Rect(10, 75, 200, 30), string.Format("Time: {0:0.00}", elapsed), guiStyle);
        GUI.EndGroup();
    }

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
		ballStartPosition = this.transform.position;
		ResetBall();
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "backwall")
		{
			scoreLeft += 1;
			blop.Play();
			ResetBall();
		}
		else if(col.gameObject.tag == "backwallLeft")
		{
			scoreRight += 1;
			blop.Play();
			ResetBall();
		}
		else
			blip.Play();
	}

	public void ResetBall()
	{
		this.transform.position = ballStartPosition;
		rb.velocity = Vector3.zero;
		Vector3 dir = new Vector3(Random.Range(100,300), Random.Range(-100,100),0).normalized;
		rb.AddForce(dir*speed);
	}
	
	// Update is called once per frame
	void Update () {
		elapsed += Time.deltaTime;
		if(Input.GetKeyDown("space"))
			ResetBall();
	}
}
