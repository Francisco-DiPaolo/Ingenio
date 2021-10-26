using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataformas : MonoBehaviour
{
    public Transform[] target;
    public float speed = 6.0f;

    int curPos = 0;
    int nextPos = 1;

    bool moveNext = true;
    public float timeToNext = 2.0f;

    CharacterController characterController;

    Vector3 groundPos;
    Vector3 lastGroundPos;
    Vector3 currentPos;

    string groundName;
    string lastGroundName;

    bool isJump;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target[nextPos].position) <= 0)
        {
            StartCoroutine(TimeMove());
            curPos = nextPos;
            nextPos++;

            if(nextPos > target.Length - 1)
            {
                nextPos = 0;
            }
        }

        if (moveNext)
            transform.position = Vector3.MoveTowards(transform.position, target[nextPos].position, speed * Time.deltaTime);
    }

    IEnumerator TimeMove()
    {
        moveNext = false;
        yield return new WaitForSeconds(timeToNext);
        moveNext = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Platform")
        {
            if (!isJump)
            {
                RaycastHit hit;
                if (Physics.SphereCast(transform.position, characterController.radius, -transform.up, out hit))
                {
                    GameObject inGround = hit.collider.gameObject;
                    groundName = inGround.name;
                    groundPos = inGround.transform.position;

                    if (groundPos != lastGroundPos && groundName == lastGroundName)
                    {
                        currentPos = Vector3.zero;
                        currentPos += groundPos - lastGroundPos;
                        characterController.Move(currentPos);
                    }
                    lastGroundName = groundName;
                    lastGroundPos = groundPos;
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (!characterController.isGrounded)
                {
                    currentPos = Vector3.zero;
                    lastGroundPos = Vector3.zero;
                    lastGroundName = null;
                    isJump = true;
                }

                if (characterController.isGrounded)
                {
                    isJump = false;
                }
            }
        }
    }
}
