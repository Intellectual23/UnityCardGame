using System;
using System.Collections.Generic;
using System.Linq;
using Card;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardGame : MonoBehaviour
{
  [SerializeField] private GameObject _cardPrefab;

  public static CardGame Instance;
  public List<CardAsset> _startCards = new();
  private Dictionary<CardInstance, CardView> _cards = new();
  public List<CardLayout> _layouts = new();
  public int HandCapacity { get; }

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
    ShuffleLayout(0);
    StartTurn();
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
  public List<CardView> GetCardsInLayout(int layoutId) => _cards.Where(pair => pair.Key.LayoutId == layoutId).Select(pair => pair.Value).ToList();

  public void RecalculateLayout(int layoutId)
  {
    List<CardView> cardsInLayout = GetCardsInLayout(layoutId);
    for (int i = 0; i < cardsInLayout.Count; i++)
    {
      cardsInLayout[i].CardInstance.CardPosition = i;
    }
  }
  
  public void StartTurn()
  {
    for (int i = 0; i < HandCapacity; ++i)
    {
      var deck = GetCardsInLayout(0);
      if (deck.Count == 0)
      {
        Debug.Log("- Deck is empty!");
        return;
      }
      deck[i].CardInstance.MoveToLayout(1, deck.Count + 1);
    }
  }
  
  public void ShuffleLayout(int layoutId)
  {
    List<CardView> cardsInLayout = GetCardsInLayout(layoutId);
    for (int i = 0; i < cardsInLayout.Count; i++)
    {
      int randomIndex = Random.Range(i, cardsInLayout.Count);
      CardInstance tempCard = cardsInLayout[i].CardInstance;
      cardsInLayout[i] = cardsInLayout[randomIndex];
      cardsInLayout[randomIndex].CardInstance = tempCard;
      cardsInLayout[i].CardInstance.CardPosition = i;
      cardsInLayout[randomIndex].CardInstance.CardPosition = randomIndex;
    }
  }
}