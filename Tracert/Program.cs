using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

namespace Tracert
{
    internal static class Program
    {
        private const int Timeout = 10000;
        private const int MaxTtl = 30;

        private static void Main()
        {
            Console.Write("Enter IP-address or domain name: ");
            var ipOrDomain = Console.ReadLine();

            if (!IPAddress.TryParse(ipOrDomain, out var ip))
            {
                try
                {
                    if (ipOrDomain != null) ip = Dns.GetHostAddresses(ipOrDomain)[0];
                }
                catch
                {
                    Console.WriteLine("Invalid IP or domain name");
                    return;
                }
            }

            Console.WriteLine($"Tracing to {ipOrDomain} ({ip})");
            Console.WriteLine($"Over a maximum of {MaxTtl} hops");
            var traceRoute = TraceRoute(ip.ToString());

            foreach (var trace in traceRoute)
            {
                trace.PrintResult();
            }

            Console.WriteLine("Trace finished");
        }

        private static IEnumerable<Trace> TraceRoute(string ip)
        {
            using var pingSender = new Ping();
            var stopWatch = new Stopwatch();

            for (var ttl = 1; ttl <= MaxTtl; ttl++)
            {
                stopWatch.Reset();
                stopWatch.Start();
                var reply = pingSender.Send(ip, Timeout, new byte[32], new PingOptions(ttl, true));
                stopWatch.Stop();

                if (reply.Status == IPStatus.Success)
                {
                    yield return new Trace(reply.Address.ToString(), stopWatch.ElapsedMilliseconds, ttl);
                    break;
                }

                if (reply.Status == IPStatus.TtlExpired)
                {
                    yield return new Trace(reply.Address.ToString(), stopWatch.ElapsedMilliseconds, ttl);
                }

                if (reply.Status == IPStatus.TimedOut)
                {
                    yield return new Trace(ttl);
                }
            }
        }
    }
}