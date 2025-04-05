using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu]
public class Item : ScriptableObject, IDestroyableItem
{
    public int ID => GetInstanceID();

    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    [field: TextArea]
    public string Description { get; set; }

    [field: SerializeField]
    public Sprite ItemImage { get; set; }
}
