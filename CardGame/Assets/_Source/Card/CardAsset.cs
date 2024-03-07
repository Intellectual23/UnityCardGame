using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(menuName = "SO/new Card Asset", fileName = "CardAsset")]
    public class CardAsset : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public Sprite Image { get; private set; }
    }
}
