using UnityEngine;
using UnityEngine.UI;

namespace Card
{
  public class CardView: MonoBehaviour
  {
    private CardInstance _cardInstance;

    public void Init(CardInstance cardInstance)
    {
      _cardInstance = cardInstance;
    }

    public CardInstance GetCardInstance()
    {
      return _cardInstance;
    }
  }
}