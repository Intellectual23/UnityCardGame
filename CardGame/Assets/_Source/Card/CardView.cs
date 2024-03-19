using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Card
{
  public class CardView : MonoBehaviour
  {
    private CardInstance _cardInstance;

    public void Init(CardInstance cardInstance)
    {
      _cardInstance = cardInstance;
      Transform image = transform.GetChild(0).transform.GetChild(0);
      if (image == null) return;
      image.GetComponent<SpriteRenderer>().sprite = CardInstance.CardAsset.Image;
    }

    public CardInstance CardInstance
    {
      get => _cardInstance;
      set => _cardInstance = value;
    }

    public void Rotate(bool up)
    {
      Transform frontSide = transform.GetChild(0);
      Transform backSide = transform.GetChild(1);
      frontSide.gameObject.SetActive(up);
      backSide.gameObject.SetActive(!up);
    }

    public void PlayCard()
    {
      if (CardInstance.LayoutId == 1)
      {
        CardInstance.MoveToLayout(2, CardGame.Instance.GetCardsInLayout(2).Count + 1);
        CardGame.Instance.RecalculateLayout(1);
      }
      else if (CardInstance.LayoutId == 2)
      {
        CardInstance.MoveToLayout(3, CardGame.Instance.GetCardsInLayout(3).Count + 1);
      }

      CardGame.Instance.RecalculateLayout(2);
    }

    private void OnMouseDown()
    {
      Debug.Log("Clicked on " + gameObject.name);
      PlayCard();
    }
  }
}