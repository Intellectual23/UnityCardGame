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
        CardView card = layoutCards[i];
        RectTransform cardRectTransform = gameObject.GetComponent<RectTransform>();
        card.transform.localPosition = cardRectTransform.position + new Vector3(i * Offset.x, i * Offset.y, 0f);
        card.transform.SetSiblingIndex(layoutCards[i].CardInstance.CardPosition);
        card.Rotate(FaceUp);
      }
    }
  }
}