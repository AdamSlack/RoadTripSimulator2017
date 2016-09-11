/* 
 * Featured in another game, as the title suggests, this was a script that tested movement
 * but it was then hijacked for another purpose. throwing fish... so yeah... when the 
 * person playing taps the screen, they toggle movement. 
 * (note for future me, Toggle Movement is a goodname for a function)
 */

using UnityEngine;
using System.Collections;

public class movementTestScript : MonoBehaviour
{

    public bool isMoving;
    private Rigidbody rb;
    private Camera cam;
    [Range(0, 10)]
    public float speed = 2.0f;

    /*
     * The game object that get's fired out.. really needs to be a fish in the game it was made for
     */
    public GameObject projectile;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponentInParent<Rigidbody>();
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 dir = cam.transform.forward;
        /* if the player is in VR (which they always are really) and the screen is touched....*/
        if(GvrViewer.Instance.VRModeEnabled && GvrViewer.Instance.Triggered)
        {
            /* Toggle movement... (should really call the function that...)*/
            setIsMoving();

            /*also, instantiate a fish object... not at all relevant to this script.*/
            GameObject fish = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
            fish.GetComponent<Rigidbody>().velocity = new Vector3(dir.x * 7, dir.y * 7, dir.z * 7);
            fish.tag = "fish";
        }
        
        /* if the player isMoving (IE movement is enabled) then move the player.*/
	    if(isMoving)
        {
            rb.velocity = new Vector3(dir.x*speed, 0, dir.z*speed); 
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
	}

    public void setIsMoving()
    {
        if(isMoving)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

}
