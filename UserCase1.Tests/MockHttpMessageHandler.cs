using System.Net;

namespace UserCase1.Tests;

public class MockHttpMessageHandler : HttpMessageHandler
{
    public virtual HttpResponseMessage Send(HttpRequestMessage request)
    {
        return new HttpResponseMessage(HttpStatusCode.OK);
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Send(request));
    }
}