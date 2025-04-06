using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImmortalityPickup : MonoBehaviour
{
    [SerializeField] private Image whiteFadeOverlay; // Assign in Inspector
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private string sceneToLoad = "WinScreen";

    private bool isPlayerNearby = false;
    private bool isFading = false;

    void Update()
    {
        if (isPlayerNearby && !isFading && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeAndLoadScene());
        }
    }

    IEnumerator FadeAndLoadScene()
    {
        isFading = true;
        float timer = 0f;

        Color color = whiteFadeOverlay.color;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            whiteFadeOverlay.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = false;
    }
}
