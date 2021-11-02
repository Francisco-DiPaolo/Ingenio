using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    int maxBounces = 5;
    private LineRenderer lr;
    [SerializeField]
    private bool reflectOnlyMirror;

    public Transform torre;
    public GameObject Puerta;
    public GameObject ReceptorTorre;
    public Material material;
    public Material material2;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, torre.position);
    }

    void Update()
    {
        CastLaser(transform.position, -transform.forward);
    }

    void CastLaser(Vector3 position, Vector3 direction)
    {
        lr.SetPosition(0, torre.position);

        for(int i=0; i < maxBounces; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, 1))
            {
                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
                lr.SetPosition(i + 1, hit.point);

                if (hit.collider.gameObject.CompareTag("Receptor"))
                {
                    Destroy(Puerta);
                    ReceptorTorre.GetComponent<Renderer>().material = material2;
                }

                if (hit.transform.tag != "Mirror" && reflectOnlyMirror)
                {
                    for(int j = (i + 1); j <= 5; j++)
                    {
                        lr.SetPosition(j, hit.point);
                    }
                    break;
                }
            }
        }
    }
}

