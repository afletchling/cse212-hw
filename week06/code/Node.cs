public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        if (Left is not null && Left.Data == value) return;
        if (Right is not null && Right.Data == value) return;

        if (value < Data)
        {
            if (Left is null)
            {
                Left = new Node(value);
            }
            else
            {
                Left.Insert(value);
            }
        }
        else
        {
            if (Right is null)
            {
                Right = new Node(value);
            }
            else
            {
                Right.Insert(value);
            }
        }
    }

    public bool Contains(int value)
    {
        if (Left is not null)
        {
            bool result = Left.Contains(value);
            if (result)
            {
                return true;
            }
        }

        if (Right is not null)
        {
            bool result = Right.Contains(value);
            if (result)
            {
                return true;
            }
        }

        return Data == value;
    }

    public int GetHeight()
    {
        int result = 1;
        if (Left is not null)
        {
            result = Math.Max(1 + Left.GetHeight(), result);
        }

        if (Right is not null)
        {
            result = Math.Max(1 + Right.GetHeight(), result);
        }

        return result;
    }
}