using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float w = 1.0f, wCohe=0.5f, wTarget = 0.5f, k = 0.5f;
    public Vector3 currentVelocity;
    public float speed = 2.0f, accuarcy = 1.0f, rotSpeed = 1.0f;
    public GameObject target; 
    public Vector3 startPos;
    Vector3 zero = new Vector3(0.0f,0.0f,0.0f);
    //Constantes de la funcion III
    public float a = 0.1f, b = 0.2f, c;
    Vector3 cohesionForce;
    List<Vector3> targetPattern = new List<Vector3> (){new Vector3(0.0f,3.0f,2.0f),new Vector3(1.5f,3.0f,1.5f),
                                        new Vector3(2.0f,3.0f,0.0f),new Vector3(1.5f,3.0f,-1.5f),new Vector3(0.0f,3.0f,-2.0f),
                                        new Vector3(-1.5f,3.0f,-1.5f),new Vector3(-2.0f,3.0f,0.0f),new Vector3(-1.5f,3.0f,1.5f)};


    public void Init(){
        currentVelocity = new Vector3(0,0,0);
        target = GameObject.FindWithTag("Objective");
    }

    public void Move(List<GameObject> population, int currentParticle)
    {
        Vector3 objectivePosition = new Vector3(target.transform.position.x,
                                        target.transform.position.y,
                                        target.transform.position.z);
        //Linear attraction
        //Vector3 nextVelocity = w*currentVelocity - k * (this.transform.position-objectivePosition);

        //Confortable distance
        //Vector3 nextVelocity = w*currentVelocity - k *(Vector3.Distance(this.transform.position,objectivePosition)-5) * (this.transform.position-objectivePosition);

        //Attraction Repulsion Function III
        //Vector3 nextVelocity = w*currentVelocity - (this.transform.position-objectivePosition) * (a - b*Mathf.Exp((-Mathf.Pow(Vector3.Distance(this.transform.position,objectivePosition),2))/c));

        //Tracking an object and form a formation around it
        cohesionForce = new Vector3 (0.0f,0.0f,0.0f);
        for (int i = 0; i < 8; i++)
        {       
                if (i != currentParticle){
                float div = Mathf.Log(b/a);
                float nom = Mathf.Pow(Vector3.Distance(targetPattern[i],targetPattern[currentParticle]),2);
                c =  nom/div;
                print ( "C entre " + i + " y " + currentParticle + " es " + c);
                float temp = (Mathf.Exp(-1 * Mathf.Pow(Vector3.Distance(this.transform.position,population[i].transform.position),2)))/c;
                print("temp is " + temp);
                cohesionForce += (this.transform.position - population[i].transform.position) * 
                    (a - b*temp);
                    }
        }
        
        Vector3 nextVelocity = w*currentVelocity - wTarget * (this.transform.position-objectivePosition) - wCohe*cohesionForce;

        Vector3 newPosition = this.transform.position + nextVelocity;
        Vector3 distanceToMove = newPosition - this.transform.position;
        currentVelocity = nextVelocity;
        if (distanceToMove != zero){
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                            Quaternion.LookRotation(distanceToMove),
                                            Time.deltaTime * rotSpeed);
            this.transform.Translate(0,0,speed*Time.deltaTime);
        }
        
    }
}
