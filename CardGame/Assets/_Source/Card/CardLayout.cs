using UnityEngine;

namespace Card
{
  public class CardLayout : MonoBehaviour
  {
    [field: SerializeField] public int LayoutId { get; private set; }
    [field: SerializeField] public Vector2 Offset { get; private set; }
    [field: SerializeField] public bool FaceUp { get; private set; }

    void Update()
    {
      var layoutCards = CardGame.Instance.GetCardsInLayout(LayoutId);
      for (int i = 0; i < layoutCards.Count; ++i)
      {
        Transform cardTransform = layoutCards[i].transform;
        cardTransform.localPosition = new Vector3(i * Offset.x, i* Offset.y, 0f);
        cardTransform.SetSiblingIndex(layoutCards[i].CardInstance.CardPosition);
        layoutCards[i].Rotate(FaceUp);
      }
    }
  }
}