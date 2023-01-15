namespace Tracert;

public class Trace
{
    public readonly string ReplyAddress;
    public readonly long ElapsedTime;
    public readonly int Hop;

    public Trace(string address, long time, int hop)
    {
        ReplyAddress = address;
        ElapsedTime = time;
        Hop = hop;
    }

    public Trace(int hop)
    {
        Hop = hop;
        ReplyAddress = "* * * * * * *";
    }
}