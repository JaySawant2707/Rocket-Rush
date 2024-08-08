using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 2f;
    [SerializeField] AudioClip successClip;
    [SerializeField] AudioClip crashClip;

    AudioSource audioSource;

    bool isTransitioning = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }
        else
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("friendly");
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                case "Fuel":
                    Debug.Log("Fuel");
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }


    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successClip);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadLevelDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashClip);
        //todo add particle effect on crash
        GetComponent<Movement>().enabled = false;
        Invoke("RelodeLevel", loadLevelDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void RelodeLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}