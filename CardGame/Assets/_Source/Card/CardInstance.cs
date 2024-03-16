namespace Card
{
  public class CardInstance
  {
    public CardAsset CardAsset;
    public int LayoutId;
    public int CardPosition;
    public CardInstance(CardAsset cardAsset)
    {
      CardAsset = cardAsset;
    }

    public void MoveToLayout(int newLayoutId, int newCardPosition)
    {
      LayoutId = newLayoutId;
      CardPosition = newCardPosition;
    }
  }
}