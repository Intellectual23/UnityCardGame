namespace Card
{
  public class CardInstance
  {
    private CardAsset _cardAsset;
    public int LayoutId;
    public int CardPosition;
    public CardInstance(CardAsset cardAsset)
    {
      _cardAsset = cardAsset;
    }

    public void MoveToLayout(int newLayoutId, int newCardPosition)
    {
      LayoutId = newLayoutId;
      CardPosition = newCardPosition;
    }
    public CardAsset CardAsset
    {
      get => _cardAsset;
      set => _cardAsset = value;
    }
  }
}