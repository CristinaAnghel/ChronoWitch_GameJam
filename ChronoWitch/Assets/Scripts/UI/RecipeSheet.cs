using System;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSheet : MonoBehaviour
{
    [SerializeField] private Image recipes;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip recipeClip;

    private void Awake()
    {
        recipes.gameObject.SetActive(false);
    }

    internal void Show()
    {
        audioSource.PlayOneShot(recipeClip);
        recipes.gameObject.SetActive(true);
    }

    internal void Hide()
    {
        audioSource.PlayOneShot(recipeClip);
        recipes.gameObject.SetActive(false);
    }

    public bool IsShowing()
    {
        return recipes.gameObject.activeSelf;
    }
}
