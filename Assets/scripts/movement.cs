using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
  Rigidbody rb;
  [SerializeField] float MainThrust = 100f;
  [SerializeField] float MainRotate = 100f;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
    ProcessThrust();
    ProcessRotation();
  }
  void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      rb.AddRelativeForce(Vector3.up * MainThrust * Time.deltaTime);
    }
  }

  void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
        {
           ApplyRotation(MainRotate);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-MainRotate);
        }
    }



     void ApplyRotation(float RotationThisFrame)
    {
      rb.freezeRotation = true; //freeze rotation to manually rotate
        transform.Rotate(Vector3.forward * RotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;// unfreeze rotation so physics system can take over
    }
}
