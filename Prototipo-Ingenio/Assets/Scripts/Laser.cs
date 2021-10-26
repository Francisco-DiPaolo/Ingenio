using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Rayo
    private Ray ray;
    private RaycastHit hit;

    [SerializeField] LineRenderer lineRenderer;

    public Transform torre;
    public GameObject Puerta;
    public GameObject ReceptorTorre;
    public Material material;
    public Material material2;

    void Update()
    {
        if (Physics.Raycast(torre.position, torre.forward, out RaycastHit hit) && hit.collider.gameObject.CompareTag("Receptor"))
        {
            Destroy(Puerta);
            ReceptorTorre.GetComponent<Renderer>().material = material2;
        }

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, torre.transform.position);
        lineRenderer.SetPosition(1, hit.point);
    }
}

