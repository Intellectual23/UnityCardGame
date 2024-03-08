using System.Collections.Generic;
using System.Linq;
using Card;
using UnityEngine;

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
      CreateCard(_startCards[i], 0);
    }
  }

  private void CreateCardView(CardInstance cardInstance)
  {
    GameObject cardObject = Instantiate(_cardPrefab);
    CardView cardView = _cardPrefab.AddComponent<CardView>();
    cardView.Init(cardInstance);
    _cards.Add(cardInstance, cardView);
  }

  private void CreateCard(CardAsset cardAsset, int layoutId)
  {
    CardInstance cardInstance = new CardInstance(cardAsset);
    CreateCardView(cardInstance);
    // Moving card to Layout. Use LINQ to get position(starts from 1).
    var layoutCards = _cards.Where(pair => pair.Key.LayoutId == layoutId).Select(pair => pair.Value).ToList();
    cardInstance.MoveToLayout(layoutId, layoutCards.Count + 1);
  }
}