using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PotionItemSO : Item, IDestroyableItem, IItemAction
{

    [SerializeField] private List<ModifierData> modifierData = new List<ModifierData>();
    public string ActionName => "Drink";

    [field: SerializeField]
    public int modify { get; private set; }

    [field: SerializeField]
    public AudioClip actionSFX {get; private set;}

    [field: SerializeField]
    public int addTime { get; private set; }

    [field: SerializeField]
    public float timeTillDeath { get; private set; }

    public bool PerformAction(GameObject character)
    {
        foreach(ModifierData data in modifierData)
        {
            data.modifier.AffectCharacter(character, data.value);
        }
        return true;
    }
}


public interface IDestroyableItem
{

}

public interface IItemAction
{
    public string ActionName { get; }
    public AudioClip actionSFX { get; }

    bool PerformAction(GameObject character);
}

[Serializable]
public class ModifierData
{
    public CharacterStatModifier modifier;
    public float value;
}

