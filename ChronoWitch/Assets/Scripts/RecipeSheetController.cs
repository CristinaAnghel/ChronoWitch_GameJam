using UnityEngine;

public class RecipeSheetController : MonoBehaviour
{
    [SerializeField] private RecipeSheet recipeSheet;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (recipeSheet.IsShowing() == false)
            {
                recipeSheet.Show();
                Debug.Log("show");
            }
            else
            {
                Debug.Log("hide");
                recipeSheet.Hide();
            }
        }
    }
}
