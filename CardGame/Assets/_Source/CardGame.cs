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
  [field: SerializeField] public int HandCapacity { get; set; }

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
  public List<CardView> GetCardsInLayout(int layoutId) =>
    _cards.Where(pair => pair.Key.LayoutId == layoutId).Select(pair => pair.Value)
      .OrderBy(card => card.CardInstance.CardPosition).ToList();

  public void RecalculateLayout(int layoutId)
  {
    List<CardView> cardsInLayout = GetCardsInLayout(layoutId);
    for (int i = 0; i < cardsInLayout.Count; i++)
    {
      cardsInLayout[i].CardInstance.CardPosition = i + 1;
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

      deck[0].CardInstance.MoveToLayout(1, GetCardsInLayout(1).Count + 1);
      RecalculateLayout(0);
    }
  }

  private void ShuffleLayout(int layoutId)
  {
    List<CardView> layoutCards = GetCardsInLayout(layoutId);
    for (int i = 0; i < layoutCards.Count; ++i)
    {
      int randomIndex = UnityEngine.Random.Range(0, layoutCards.Count);
      CardView curCard = layoutCards[i];
      CardView cardToSwap = layoutCards[randomIndex];
      (curCard.CardInstance.CardPosition, cardToSwap.CardInstance.CardPosition) = (cardToSwap.CardInstance.CardPosition,
        curCard.CardInstance.CardPosition);
    }
  }
}