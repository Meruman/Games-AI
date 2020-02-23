using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SwarmBehaviour : MonoBehaviour
{

    public GameObject botPrefab;
    public int populationSize = 8;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public float speed = 3.0f, accuarcy = 1.0f, rotSpeed = 1.0f;
    public GameObject target; 

    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindWithTag("Objective");
        for(int i = 0; i < populationSize; i++)
        {
            Vector3 startingPos = new Vector3(Random.Range(-20,20),
                                                Random.Range(-2,2),
                                                Random.Range(-20,20));

            GameObject b = Instantiate(botPrefab, startingPos, this.transform.rotation);
            b.GetComponent<Movement>().Init();
            population.Add(b);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < populationSize; i++)
        {
            
            population[i].GetComponent<Movement>().Move(population,i);

        }
    }
}
