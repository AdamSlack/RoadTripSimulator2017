using UnityEngine;
using System.Collections;

public class destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject,0.5f);
    }
}
