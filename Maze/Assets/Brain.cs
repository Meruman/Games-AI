
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    int DNALength = 2;
    public float timeAlive = 0;
    public int timeWalking = 0;
    public float distanceWalked = 0;
    public Vector3 initialPos;
    public DNA dna;
    public GameObject eyes;
    bool alive = true;
    bool seeWall = true;
    public float maxDistanceWalked = 0;

    /* public GameObject ethanPrefab;
    GameObject ethan; */

    /* private void OnDestroy() {
        Destroy(ethan);
    } */
    private void OnCollisionEnter(Collision obj) {
        if (obj.gameObject.tag == "dead")
        {
            alive = false;
            distanceWalked = 0;
            maxDistanceWalked = 0;
        }
    }

    public void Init()
    {
        //Initialize DNA
        //0 forward
        //1 rotation angle
        dna = new DNA(DNALength,360);
        timeAlive = 0;
        alive = true;
        distanceWalked = 0;
        initialPos = this.transform.position;
        maxDistanceWalked = 0;

        /* ethan = Instantiate(ethanPrefab, this.transform.position, this.transform.rotation);
        ethan.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = this.transform; */
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!alive) return;

        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 0.5f,Color.red,10);
        seeWall = false;
        RaycastHit hit;
        if(Physics.SphereCast(eyes.transform.position, 0.1f, eyes.transform.forward, out hit, 0.5f))
        {
            if(hit.collider.gameObject.tag == "wall")
            {
                seeWall = true;
            }
        }
        timeAlive = PopulationManager.elapsed;
    }

    private void FixedUpdate() {
        if(!alive) return;
        //readDNA
        float turn = 0;
        float move = dna.GetGene(0);

        if(seeWall)
        {
            turn = dna.GetGene(1);
        }
        

        this.transform.Translate(0,0,move*0.0007f);
        this.transform.Rotate(0,turn,0);
        distanceWalked += Vector3.Distance(this.transform.position, initialPos);
        /* if (distanceWalked > maxDistanceWalked)
        {
            maxDistanceWalked = distanceWalked;
        }   */  
    }
}
