public class FibonacciObject
{
    int
        previousNumber,
        currentNumber;
    int
        identifier;

    public FibonacciObject()
    {
        previousNumber = currentNumber = 1;
        identifier = 0;
    }

    public int GetValue()
    {
        return currentNumber;
    }

    public int GetNextValue()
    {
        int
            new_value;

        new_value = previousNumber + currentNumber;
        previousNumber = currentNumber;
        currentNumber = new_value;

        ++identifier;

        return previousNumber;
    }

    public int GetIdentifier()
    {
        return identifier;
    }
}