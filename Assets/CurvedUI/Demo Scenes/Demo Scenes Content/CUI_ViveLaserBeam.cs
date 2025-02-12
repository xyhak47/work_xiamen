﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CurvedUI
{
    /// <summary>
    /// This class contains code that controls the mockup vive controller. 
    /// Its made to make demo sceen look better. Its not made to be used with actual vive controller.
    /// </summary>
    public class CUI_ViveLaserBeam : MonoBehaviour
    {

        [SerializeField]
        Transform LaserBeamTransform;
        [SerializeField]
        Transform LaserBeamDot;

        

        // Update is called once per frame
        protected void Update()
        {

            //get direction of the controller
            Ray myRay = new Ray(this.transform.position, this.transform.forward);


            //make laser beam hit stuff it points at.
            if(LaserBeamTransform && LaserBeamDot) {
                //change the laser's length depending on where it hits
                float length = 10000;

                RaycastHit hit;
                if (Physics.Raycast(myRay, out hit, length))
                {
                    length = Vector3.Distance(hit.point, this.transform.position);

                    //Debug.LogWarning("hit:" + hit.transform.name);
                    //If we hit a canvas, we only want transforms with graphics to block the pointer. (that are drawn by canvas => depth not -1)
                    if (hit.transform.GetComponent<CurvedUIRaycaster>() != null)  {
                        int SelectablesUnderPointer = hit.transform.GetComponent<CurvedUIRaycaster>().GetObjectsUnderPointer().FindAll(x => x.GetComponent<Graphic>() != null && x.GetComponent<Graphic>().depth != -1).Count;

                        //Debug.Log("found graphics: " + SelectablesUnderPointer);
                        length = SelectablesUnderPointer == 0 ? 10000 : Vector3.Distance(hit.point, this.transform.position);
                    }  

                }

                LaserBeamTransform.localScale = LaserBeamTransform.localScale.ModifyZ(length);
            }
           

        }
    }
}
