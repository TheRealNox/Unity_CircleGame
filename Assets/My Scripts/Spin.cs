using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

    public float speed = 80f;
	
	// Update is called once per frame
	void Update ()
    {
        //transform.Rotate(0, 0, Time.deltaTime * speed);
		transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.back, Time.deltaTime * speed);
    }
}
