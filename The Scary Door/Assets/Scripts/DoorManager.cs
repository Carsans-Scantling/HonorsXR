using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DoorManager : MonoBehaviour
{

    public GameObject ARCamera;
    public GameObject InsideDoor;
    public Material clear;
    public Material doorMat;

    private Material[] DoorMats;
    private bool insideRoom = false;
    private GameObject backDoorPlane;
    private GameObject frontDoorPlane;

    // Start is called before the first frame update
    void Start()
    {
        ARCamera = GameObject.Find("AR Camera");
        DoorMats = InsideDoor.GetComponent<Renderer>().sharedMaterials;
        backDoorPlane = GameObject.Find("BackDoorPlane");
        frontDoorPlane = GameObject.Find("FrontDoorPlane");
    }


    void OnTriggerEnter(Collider collider)
    {
        if (this.name == "FrontDoorPlane") {
            if(insideRoom == false) {
                //Disable stencil when colliding
                backDoorPlane.GetComponent<Renderer>().material = clear;
                frontDoorPlane.GetComponent<Renderer>().material = clear;
                backDoorPlane.SetActive(false);
                for (int i = 0; i < DoorMats.Length; ++i) {
                    DoorMats[i].SetInt("_StencilComp", (int)CompareFunction.Always);
                }
                insideRoom = true;
            } else {
                //Enable stencil when exiting
                backDoorPlane.SetActive(true);
                backDoorPlane.GetComponent<Renderer>().material = doorMat;
                frontDoorPlane.GetComponent<Renderer>().material = doorMat;
                for (int i = 0; i < DoorMats.Length; ++i) {
                    DoorMats[i].SetInt("_StencilComp", (int)CompareFunction.Equal);
                }
                insideRoom = false;
            }
        } else {
            if(insideRoom == false) {
                //Disable stencil when colliding
                frontDoorPlane.GetComponent<Renderer>().material = clear;
                backDoorPlane.GetComponent<Renderer>().material = clear;
                frontDoorPlane.SetActive(false);
                for (int i = 0; i < DoorMats.Length; ++i) {
                    DoorMats[i].SetInt("_StencilComp", (int)CompareFunction.Always);
                }
                insideRoom = true;
            } else {
                //Enable stencil when exiting
                frontDoorPlane.SetActive(true);
                backDoorPlane.GetComponent<Renderer>().material = doorMat;
                frontDoorPlane.GetComponent<Renderer>().material = doorMat;
                for (int i = 0; i < DoorMats.Length; ++i) {
                    DoorMats[i].SetInt("_StencilComp", (int)CompareFunction.Equal);
                }
                insideRoom = false;
            }
        }
    }
}
