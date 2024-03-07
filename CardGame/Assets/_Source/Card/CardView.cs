using UnityEngine;

namespace Card
{
  public class CardView: MonoBehaviour
  {
    private CardInstance _cardInstance;

    public void Init(CardInstance cardInstance)
    {
      _cardInstance = cardInstance;
    }
  }
}