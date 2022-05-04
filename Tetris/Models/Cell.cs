public class Cell
{
    private CellType _type = CellType.Empty;
    public CellType Type
    {
        get => _type;
        set
        {
            if (value == CellType.Empty)
            {
                Block?.QueueFree();
                Block = null;
            }
            _type = value;
        }
    }

    public BaseBlock _block = null;
    public BaseBlock Block
    {
        get => _block;
        set
        {
            if (value != null)
            {
                _type = CellType.Filled;
            }
            else
            {
                _type = CellType.Empty;
            }

            _block = value;
        }
    }

    public void FreeCell() {
        Type = CellType.Empty;
    }
}