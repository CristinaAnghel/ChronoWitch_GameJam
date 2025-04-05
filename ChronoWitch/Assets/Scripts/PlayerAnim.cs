using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator animator;

    Vector2 movement;

    //[SerializeField] SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.transform.localScale = new Vector3(.65f, .65f, 1);
        //spriteRenderer.transform.localPosition = new Vector3(100, -100, 0);
    }
}
