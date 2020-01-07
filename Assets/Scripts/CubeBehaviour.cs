using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour {


    public Material blackMat;
    public Material whiteMat;
    public bool lit = false;
    private GameObject childText;

	// Use this for initialization
	void Start () {
        //blackMat = Resources.Load<Material>("Materials/Black.mat");
        //whiteMat = Resources.Load<Material>("Materials/White.mat");
	}
	
	// Update is called once per frame
	void Update () {
        if(lit){
            this.gameObject.GetComponent<Renderer>().material = whiteMat;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball"){
            this.gameObject.GetComponent<Renderer>().material = whiteMat;
            if(!lit){
                //play point animation
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball")
        {
            this.gameObject.GetComponent<Renderer>().material = whiteMat;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ball")
        {
            //this.gameObject.GetComponent<Renderer>().material = blackMat;
        }
    }


}
