using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Card
{
  public class CardView : MonoBehaviour, IPointerClickHandler
  {
    private CardInstance _cardInstance;
    public Image _cardImage;

    public void Init(CardInstance cardInstance)
    {
      _cardInstance = cardInstance;
      _cardImage.sprite = cardInstance.CardAsset.Image;
      _cardImage.color = cardInstance.CardAsset.Color;
      gameObject.name = cardInstance.CardAsset.Name;
    }

    public CardInstance CardInstance
    {
      get => _cardInstance;
      set => _cardInstance = value;
    }

    public void Rotate(bool up)
    {
      // Assuming there is a back image as a child object named "BackImage"
      Transform backImage = transform.GetChild(0);
      backImage.gameObject.SetActive(!up);
      _cardImage.gameObject.SetActive(up);
    }

    public void PlayCard()
    {
      CardInstance.MoveToLayout(2,CardGame.Instance.GetCardsInLayout(2).Count + 1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      PlayCard();
    }
  }
}