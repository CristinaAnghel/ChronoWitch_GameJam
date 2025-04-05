using UnityEngine;
public class PlayerAgeSwitcher : MonoBehaviour
{
    public enum PlayerFormType
    {
        Past,
        Present,
        Future
    }

    public GameObject[] playerForms; // Assign in Inspector
    private int currentIndex = 0;

    void Start()
    {
        SetActiveForm(currentIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Save position of current form
            Vector3 currentPos = playerForms[currentIndex].transform.position;

            // Disable current form
            playerForms[currentIndex].SetActive(false);

            // Increment and loop index
            currentIndex = (currentIndex + 1) % playerForms.Length;

            // Set new form's position
            playerForms[currentIndex].transform.position = currentPos;

            // Optional: reset velocity if using Rigidbody2D
            Rigidbody2D rb = playerForms[currentIndex].GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;

            // Enable new form
            playerForms[currentIndex].SetActive(true);
        }
    }

    void SetActiveForm(int index)
    {
        for (int i = 0; i < playerForms.Length; i++)
        {
            playerForms[i].SetActive(i == index);
        }
    }

    public void SwitchToForm(PlayerFormType formType)
    {
        int newIndex = (int)formType;
        if (newIndex >= 0 && newIndex < playerForms.Length)
        {
            for (int i = 0; i < playerForms.Length; i++)
            {
                playerForms[i].SetActive(i == newIndex);
            }
            currentIndex = newIndex;
        }
    }
}
