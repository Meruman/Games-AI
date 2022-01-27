using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    int DNALength = 5;
    public DNA dna;
    public float timeAlive = 0;
    public GameObject eyes;
    bool alive = true;
    public float distanceTravelled = 0;
    bool seeUpWall = false;
    bool seeDownWall = false;
    bool seeTop = false;
    bool seeBottom = false;
    Vector3 startPosition;
    public int crash = 0;
    Rigidbody2D rb; 

    
    private void OnCollisionEnter2D(Collision2D obj) {
        if (obj.gameObject.tag == "dead")
        {
            alive = false;
        }
    }

    private void OnCollisionStay2D(Collision2D obj) {
        if(obj.gameObject.tag == "top"||
                obj.gameObject.tag == "bottom"||
                obj.gameObject.tag == "upwall"||
                obj.gameObject.tag == "downwall")
                {
                    crash++;
                }
    }

    public void Init()
    {
        //Initialize DNA
        //0 forward
        //1 upwall
        //2 Downwall
        //3 normal upward
        dna = new DNA(DNALength,200);
        this.transform.Translate(UnityEngine.Random.Range(-1.5f,1.5f), UnityEngine.Random.Range(-1.5f,1.5f),0);
        timeAlive = 0;
        alive = true;
        startPosition = this.transform.position;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!alive) return;
        seeUpWall = false;
        seeDownWall = false;
        seeTop = false;
        seeBottom = false;

        //The forward is the z axis(blue) and right vector is the x axis (red)
        RaycastHit2D hit = Physics2D.Raycast(eyes.transform.position, eyes.transform.forward,1.0f);
        
        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 1.0f,Color.red);
        Debug.DrawRay(eyes.transform.position, eyes.transform.up * 1.0f,Color.red);
        Debug.DrawRay(eyes.transform.position, -eyes.transform.up * 1.0f,Color.red);

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "upwall")
            {
                seeUpWall = true;
            }
            else if(hit.collider.gameObject.tag == "downwall")
            {
                seeDownWall = true;
            }
        }

        hit = Physics2D.Raycast(eyes.transform.position, eyes.transform.up,1.0f);
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "top")
            {
                seeTop = true;
            }
        }
        hit = Physics2D.Raycast(eyes.transform.position, -eyes.transform.up,1.0f);
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "bottom")
            {
                seeBottom = true;
            }
        }

        timeAlive = PopulationManager.elapsed;
    }

    private void FixedUpdate() {
        if(!alive) return;
        //readDNA
        float upforce = 0;
        float forwardForce = 1.0f;

        if(seeUpWall)
        {
            upforce = dna.GetGene(0);
        }
        else if(seeDownWall)
        {
            upforce = dna.GetGene(1);
        }
        else if(seeTop)
        {
            upforce = dna.GetGene(2);
        }
        else if(seeBottom)
        {
            upforce = dna.GetGene(3);
        }
        else
        {
            upforce = dna.GetGene(4);
        }
        

        rb.AddForce(this.transform.right*forwardForce);
        rb.AddForce(this.transform.up*upforce*0.1f);
        distanceTravelled =  this.transform.position.x - startPosition.x;
    }
}
