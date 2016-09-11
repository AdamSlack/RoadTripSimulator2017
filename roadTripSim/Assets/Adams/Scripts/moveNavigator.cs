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

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class moveNavigator : MonoBehaviour, IGvrGazeResponder
{
    public GameObject navigator;
    [Range(-1.0f, 1.0f)]
    public float rot = 0.0f;
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

    public void SetGazedAt(bool gazedAt)
    {
        GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.grey;
    }

    public void ToggleVRMode()
    {
        GvrViewer.Instance.VRModeEnabled = !GvrViewer.Instance.VRModeEnabled;
    }

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

    public void ToggleDirectRender()
    {
        GvrViewer.Controller.directRender = !GvrViewer.Controller.directRender;
    }

    /*
     * will move the navigator left or right depending on the the value of Rot.
     * should be called offset, but it causes a rotation effect in the game.
     * called when the player looks at one of the steering wheel objects and triggers it.
     */
    public void moveNav()
    {
        Vector3 pos = navigator.GetComponent<Transform>().localPosition;
        navigator.GetComponent<Transform>().localPosition = new Vector3(pos.x + rot, 0, 5);
    }

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
        moveNav();
    }

    #endregion
}
