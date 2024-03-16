using System;
using System.Collections.Generic;
using System.Linq;
using Card;
using UnityEditor;
using UnityEngine;

public class CardGame : MonoBehaviour
{
  [SerializeField] private GameObject _cardPrefab;

  public static CardGame Instance;
  public List<CardAsset> _startCards = new();
  private Dictionary<CardInstance, CardView> _cards = new();
  public List<CardLayout> _layouts = new();
  [SerializeField] public int HandCapacity;

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

  private void Start()
  {
    StartGame();
  }

  private void StartGame()
  {
    for (int i = 0; i < _startCards.Count; ++i)
    {
      CreateCard(_startCards[i], 0);
    }
    StartTurn();
  }

  private void CreateCardView(CardInstance cardInstance)
  {
    GameObject cardObject = Instantiate(_cardPrefab);
    CardView cardView = cardObject.GetComponent<CardView>();
    cardView.Init(cardInstance);
    _cards.Add(cardInstance, cardView);
  }

  private void CreateCard(CardAsset cardAsset, int layoutId)
  {
    CardInstance cardInstance = new CardInstance(cardAsset);
    CreateCardView(cardInstance);
    // Moving card to Layout.
    var layoutCards = GetCardsInLayout(layoutId);
    cardInstance.MoveToLayout(layoutId, layoutCards.Count + 1);
  }
  
  //Use LINQ to get position(starts from 1).
  public List<CardView> GetCardsInLayout(int layoutId) => _cards.Where(pair => pair.Key.LayoutId == layoutId).Select(pair => pair.Value).ToList();

  public void RecalculateLayout(int layoutId)
  {
    List<CardView> cardsInLayout = GetCardsInLayout(layoutId);
    for (int i = 0; i < cardsInLayout.Count; i++)
    {
      cardsInLayout[i].CardInstance.CardPosition = i+1;
    }
  }
  
  public void StartTurn()
  {
    ShuffleLayout(0);
    for (int i = 0; i < HandCapacity; ++i)
    {
      var deck = GetCardsInLayout(0);
      if (deck.Count == 0)
      {
        Debug.Log("- Deck is empty!");
        return;
      }
      deck[i].CardInstance.MoveToLayout(1, GetCardsInLayout(1).Count + 1);
    }
    Instance.RecalculateLayout(0);
  }
  
  public void ShuffleLayout(int layoutId)
  {
    List<CardView> layoutCards = GetCardsInLayout(layoutId);
    for (int i = 0; i < layoutCards.Count; ++i)
    {
      int randomIndex = UnityEngine.Random.Range(0, layoutCards.Count);
      var curCard = layoutCards[i];
      var cardToSwap = layoutCards[randomIndex];
      Debug.Log($"Changed {i}:{curCard.CardInstance.CardPosition} - {randomIndex}:{cardToSwap.CardInstance.CardPosition}");
      (curCard.CardInstance.CardPosition, cardToSwap.CardInstance.CardPosition) = (cardToSwap.CardInstance.CardPosition, curCard.CardInstance.CardPosition);
    }
  }
}