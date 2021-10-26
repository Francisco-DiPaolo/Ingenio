using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AgarrarObjecto : MonoBehaviour
{
    public GameObject handPoint;
    private GameObject pickedObject;

    public GameObject Boton;
    public GameObject Trofeo;
    public Material material;
    public Material material2;

    public GameObject textControles;
    public GameObject textControles2;

    private void Start()
    {
        Trofeo.SetActive(false);
        textControles.SetActive(false);
        textControles2.SetActive(false);
    }
    void Update()
    {
       if (pickedObject != null)
        {
            if (Input.GetKey("r"))
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;

                pickedObject.GetComponent<Rigidbody>().isKinematic = false;

                pickedObject.gameObject.transform.SetParent(null);

                pickedObject = null;
            }
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            if (Input.GetKey("e") && pickedObject == null)
            {
                other.GetComponent<Rigidbody>().useGravity = false;

                other.GetComponent<Rigidbody>().isKinematic = true;

                other.transform.position = handPoint.transform.position;

                other.gameObject.transform.SetParent(handPoint.gameObject.transform);

                pickedObject = other.gameObject;
            }

            Trofeo.SetActive(true);
            Boton.GetComponent<Renderer>().material = material2;
        }

        if (other.gameObject.CompareTag("TriggerControl"))
        {
            textControles.SetActive(true);

        } else
        {
            textControles.SetActive(false);
        }

        if (other.gameObject.CompareTag("TriggerControl2"))
        {
            textControles2.SetActive(true);

        }
        else
        {
            textControles2.SetActive(false);
        }
    }
}
