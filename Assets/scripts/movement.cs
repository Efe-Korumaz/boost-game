using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
  Rigidbody rb;
  AudioSource audioSource;
  [SerializeField] float MainThrust = 100f;
  [SerializeField] float MainRotate = 100f;
  [SerializeField] AudioClip mainengine;
  [SerializeField] ParticleSystem thrust;
  bool isTransition = false;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    Thrusting();
    Rotating();
  }
  void Thrusting()
{
    if (Input.GetKey(KeyCode.Space))
    {
        rb.AddRelativeForce(Vector3.up * MainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainengine);
        }
      
    if (!thrust.isPlaying)
    {
      thrust.Play();
    }
    }
       else
    {
        audioSource.Stop(); // Stop audio playback when space key is released
    }
   
}

  void Rotating()
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
