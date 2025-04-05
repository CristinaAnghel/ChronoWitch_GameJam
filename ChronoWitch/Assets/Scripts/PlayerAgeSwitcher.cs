using UnityEngine;
public class PlayerAgeSwitcher : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip changeAgeClip;
    public string state;
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
        /*
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
        */
    }

    public void SwitchAge(int index, float timeTillDeath)
    {
        audioSource.PlayOneShot(changeAgeClip);
        timer.ChangeTime(timeTillDeath);
        Debug.Log(playerForms[index]);
        Vector3 currentPos = playerForms[currentIndex].transform.position;
        playerForms[currentIndex].SetActive(false);
        currentIndex = index;
        state = GetAgeState(currentIndex);
        timer.ChangeAgeState(state);
        playerForms[currentIndex].transform.position = currentPos;
        Rigidbody2D rb = playerForms[currentIndex].GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
        playerForms[currentIndex].SetActive(true);
    }

    public string GetAgeState(int currentIndex)
    {
        if (currentIndex == 0)
            state = "Present";
        else if (currentIndex == 1)
            state = "Past";
        else
            state = "Future";
        return state;
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
