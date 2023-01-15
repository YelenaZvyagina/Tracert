namespace Tracert;

public class Trace
{
    private readonly string _replyAddress;
    private readonly long _elapsedTime;
    private readonly int _ttl;

    public Trace(string address, long time, int ttl)
    {
        _replyAddress = address;
        _elapsedTime = time;
        _ttl = ttl;
    }

    public Trace(int ttl)
    {
        _ttl = ttl;
        _replyAddress = "* * * * * * *";
    }

    public void PrintResult()
    {
        Console.WriteLine(_ttl + ") " + _replyAddress + "\t" + _elapsedTime + "ms");
    }
}