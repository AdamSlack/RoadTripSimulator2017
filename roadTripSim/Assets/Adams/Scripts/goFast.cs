// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

/* 
 * please please please ignore the name, i was tired after work...
 * This script implements Google's VR Gaze Responder methods.
 * used to handle the user gazing at the car's pedals.
 * i know this is terrible, i just wanted to play with VR
 * 
 * to be used inconjunction with the waypoint follow script to increase the speed of the car
 * thinking about it... the waypointFollow script is useless for this purpose
 * but at the time, at least... at first, before i got the idea to make the car speed up and
 * turn and things, it was needed. now i suppose i should remove the waypoints, and just change the velocity...
 */

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class goFast : MonoBehaviour, IGvrGazeResponder
{
    /* 
     * The gameobject that is gonna have it's waypoint script modified.
     */
    public GameObject car;

    [Range(-1.0f, 1.0f)]
    public float accelleration = 0.0f;

    void Start()
    {
    }

    void LateUpdate()
    {
        GvrViewer.Instance.UpdateState();
        if (GvrViewer.Instance.BackButtonPressed)
        {
            Application.Quit();
        }

    }


    /* 
     * If the user is literally looking at the object, set it to green. otherwise grey.
     * should really set it back to the original colour...
    */
    public void SetGazedAt(bool gazedAt)
    {
        GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.grey;
    }

    /* Not really used. always gonna be in VR mode soo.....*/
    public void ToggleVRMode()
    {
        GvrViewer.Instance.VRModeEnabled = !GvrViewer.Instance.VRModeEnabled;
    }

    /* 
     * something to do with displaying things on the screen correctly? 
     * dunno, should look into google's documentation though, heard its good
     */
    public void ToggleDistortionCorrection()
    {
        switch (GvrViewer.Instance.DistortionCorrection)
        {
            case GvrViewer.DistortionCorrectionMethod.Unity:
                GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.Native;
                break;
            case GvrViewer.DistortionCorrectionMethod.Native:
                GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.None;
                break;
            case GvrViewer.DistortionCorrectionMethod.None:
            default:
                GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.Unity;
                break;
        }
    }

    /* 
     * So this does something.
     */
    public void ToggleDirectRender()
    {
        GvrViewer.Controller.directRender = !GvrViewer.Controller.directRender;
    }

    /*
     * increase the speed that the gameobject follows the waypoints by...
     * originally there was an actually set of waypoints, and you could speed up and
     * follow them faster, but then i liked the idea of adding a turning thing... so yeah... sorry adam
     */
    public void changeSpeed()
    {
        car.GetComponent<wayPointFollowScript>().speed += accelleration;
    }



    /*
     * Note for future me. read up on it, you don't really
     * know how gaze respondr is implemented with with anythin.. 
     */

    /** Some stuff from google **/
    /** I Didnt write this...  **/
    #region IGvrGazeResponder implementation

    /// Called when the user is looking on a GameObject with this script,
    /// as long as it is set to an appropriate layer (see GvrGaze).
    public void OnGazeEnter()
    {
        SetGazedAt(true);
    }

    /// Called when the user stops looking on the GameObject, after OnGazeEnter
    /// was already called.
    public void OnGazeExit()
    {
        SetGazedAt(false);
    }

    /// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
    public void OnGazeTrigger()
    {
        changeSpeed();
    }

    #endregion
}
