using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class RespawnTimer : MonoBehaviour
{
    float elapsedTime;

    public void Start()
    {
        elapsedTime = 0;
    }

    public void Update()
    { 
        if (elapsedTime >= 0)
        {
            elapsedTime += Time.deltaTime;
        }

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
       
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
