using System.Collections.Generic;
using Card;
using UnityEngine;

namespace DefaultNamespace
{
  public class CardGame : MonoBehaviour
  {
    [SerializeField] private GameObject _cardPrefab;

    public static CardGame Instance;
    public List<CardAsset> _startCards = new();
    private Dictionary<CardInstance, CardView> _cards = new();

    private void Awake()
    {
      if (Instance == null)
      {
        Instance = this;
      }
      else
      {
        Destroy(gameObject);
      }

      DontDestroyOnLoad(this);
    }

    private void StartGame()
    {
      for (int i = 0; i < _startCards.Count; ++i)
      {
        CardInstance cardInstance = new CardInstance(_startCards[i]);
        GameObject cardObject = Instantiate(_cardPrefab);
        CardView cardView = _cardPrefab.AddComponent<CardView>();
        cardView.Init(new CardInstance(_startCards[i]));
        _cards.Add(cardInstance, cardView);
      }
    }
  }
}