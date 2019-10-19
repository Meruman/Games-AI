using UnityEngine;

public class Move : MonoBehaviour {

    public Vector3 goal = new Vector3(5, 0, 4);
    public float speed = 0.1f;

    void Start() 
    {
    }

//it is better for movementes in lights
    private void LateUpdate() 
    {
        //To avoid inconcistencies in the speed due to updates we use time.deltaTime
        this.transform.Translate(goal.normalized * speed * Time.deltaTime);
    }
}
