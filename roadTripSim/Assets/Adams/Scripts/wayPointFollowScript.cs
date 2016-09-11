using UnityEngine;
using System.Collections;
/*  Way Point Follow Script.
 *  Will move and rotate towards a gameobject.
 *  Iterates over a list of waypoints, allows for the creation of a track.
 *  Single gameobject can be used to create a simple guidance thingie.
 */
public class wayPointFollowScript : MonoBehaviour {

    public GameObject[] waypoints;
    private Transform currentWaypoint;
    private int currentIdx = 0;

    [Range(0.0f, 10.0f)]
    public float speed = 1.0f;

	// Use this for initialization
	void Start () 
    {
        // Initalise the first waypoint.
	    currentWaypoint = waypoints[currentIdx].GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        moveTowards();
        checkWaypoint();
	}

    /******************************************************************************************/
    /**     Directs and moves object towards the current waypoint object.                    **/
    /**     Uses Transform and not velocity... i know it's bad, but it's just for testing... **/
    /******************************************************************************************/
    void moveTowards()
    {
        // Rotate Towards the current Waypoint
        transform.forward = Vector3.RotateTowards( transform.forward,
                                                   currentWaypoint.position - transform.position,
                                                   speed/3 * Time.deltaTime, 
                                                   0.0f );

        // Move towards the current Waypoint.
        transform.position = Vector3.MoveTowards( transform.position,
                                                  currentWaypoint.position,
                                                  speed * Time.deltaTime );

    }
    /******************************************************************************************/
    /**     Checks if waypoint has been met, increments to the next waypoint if so.          **/
    /**     will wrap round to the first waypoint , should probably make this optional...    **/
    /******************************************************************************************/

    void checkWaypoint()
    {
        if (transform.position == currentWaypoint.position)
        {
            currentIdx++;
            if (currentIdx == waypoints.Length)
            {
                currentIdx = 0;
            }
            currentWaypoint = waypoints[currentIdx].GetComponent<Transform>();
        }
    }
}
