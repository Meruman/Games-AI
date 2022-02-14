using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : MonoBehaviour
{
    public GameObject paddle;
    private Rigidbody rb;
    float paddleMaxSpeed = 15;
    float paddleMinY = 8.8f;
    float paddleMaxY = 17.4f;
    Vector3 startPosition;
    float dirY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dirY = Input.GetAxis("Vertical");
			
    }

    private void FixedUpdate() {
        float posy = Mathf.Clamp(paddle.transform.position.y + (dirY*Time.deltaTime*paddleMaxSpeed), paddleMinY,paddleMaxY);
        paddle.transform.position = new Vector3(paddle.transform.position.x, posy, paddle.transform.position.z);
    }
}
