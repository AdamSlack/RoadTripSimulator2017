using UnityEngine;
using System.Collections;

public class feedScript : MonoBehaviour {

    private AudioSource audio;
	// Use this for initialization
	void Start () 
    {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnCollisionEnter(Collision col)
    {

        if(col.transform.tag == "fish")
        {
            audio.Play();
            Destroy(col.rigidbody.GetComponent<GameObject>());
            Vector3 scale = GetComponent<Transform>().localScale;
            GetComponent<Transform>().localScale = new Vector3(scale.x + 0.25f, scale.y + 0.25f, scale.z + 0.25f);
        }
    }
}
