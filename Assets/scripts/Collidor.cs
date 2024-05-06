using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Collidor : MonoBehaviour
{
    [SerializeField] float crash = 1f;
    [SerializeField] AudioClip finishedLevel;
    [SerializeField] AudioClip restartLevel;
    [SerializeField] ParticleSystem sucess;
    [SerializeField] ParticleSystem boom;

    AudioSource audioSource;
bool isTransition = false;
bool collisionDisabled = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on the object.");
        }
    }

    private void Update(){
        DebugRespond();

    }
    void DebugRespond()
{
    if(Input.GetKey(KeyCode.L)){
        nextLevel();
    }
    else if (Input.GetKey(KeyCode.C)){
        collisionDisabled = !collisionDisabled; // we can toggle collision.

    }
}
    void OnCollisionEnter(Collision other)
    {
        if (isTransition || collisionDisabled) {return;}
        switch (other.gameObject.tag)
        {
            case "friendly":
                Debug.Log("is a friend");
                break;
            case "Finish":
                Debug.Log("you are done congrats");
                startSuccesPlan();
                break;
            default:
                Debug.Log("you died. L");
                startCrashPlan();
                break;
        }
    }

    void startSuccesPlan()
    {
        isTransition = true;
        audioSource.PlayOneShot(finishedLevel);
        GetComponent<movement>().enabled = false;
        sucess.Play();
       Invoke( "nextLevel", crash);
       GetComponent<ParticleSystem>().Play(sucess);
       DebugRespond();
    }

    void startCrashPlan()
    {
        isTransition = true;
        audioSource.PlayOneShot(restartLevel);
        boom.Play();
        GetComponent<movement>().enabled = false;
        Invoke("ReloadLevel", crash);
            }

    void ReloadLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void nextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

 

}