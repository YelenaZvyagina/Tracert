namespace Tracert;

public class Trace
{
    public readonly string ReplyAddress;
    public readonly long ElapsedTime;
    public readonly int Ttl;

    public Trace(string address, long time, int ttl)
    {
        ReplyAddress = address;
        ElapsedTime = time;
        Ttl = ttl;
    }

    public Trace(int ttl)
    {
        Ttl = ttl;
        ReplyAddress = "* * * * * * *";
    }
}