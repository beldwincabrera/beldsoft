using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Options;

namespace Beldsoft.Web.Services;

internal sealed class GraphLeadNotificationSender : ILeadNotificationSender
{
    internal const string HttpClientName = "MicrosoftGraphLeadNotifications";
    private static readonly TokenRequestContext TokenRequest =
        new(["https://graph.microsoft.com/.default"]);
    private readonly ClientSecretCredential _credential;
    private readonly GraphEmailOptions _options;
    private readonly IHttpClientFactory _httpClientFactory;

    public GraphLeadNotificationSender(
        IOptions<LeadOptions> options,
        IHttpClientFactory httpClientFactory)
    {
        _options = options.Value.Graph;
        _httpClientFactory = httpClientFactory;

        if (!_options.IsConfigured)
        {
            throw new InvalidOperationException(
                "Production Microsoft Graph Lead notification settings are incomplete.");
        }

        _credential = new ClientSecretCredential(
            _options.TenantId,
            _options.ClientId,
            _options.ClientSecret);
    }

    public string DeliveryMode => "Microsoft Graph";

    public async Task SendAsync(
        LeadNotification notification,
        CancellationToken cancellationToken = default)
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(TimeSpan.FromSeconds(_options.TimeoutSeconds));

        var accessToken = await _credential.GetTokenAsync(TokenRequest, timeout.Token);
        var client = _httpClientFactory.CreateClient(HttpClientName);
        var endpoint = $"users/{Uri.EscapeDataString(_options.SenderUserId)}/sendMail";

        for (var attempt = 0; attempt < 2; attempt++)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = JsonContent.Create(CreatePayload(notification))
            };
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken.Token);

            using var response = await client.SendAsync(request, timeout.Token);
            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                return;
            }

            if (attempt == 0 && IsTransient(response.StatusCode))
            {
                await Task.Delay(GetRetryDelay(response), timeout.Token);
                continue;
            }

            throw new HttpRequestException(
                $"Microsoft Graph did not accept the Lead notification. Status code: {(int)response.StatusCode}.",
                inner: null,
                response.StatusCode);
        }
    }

    private static object CreatePayload(LeadNotification notification) =>
        new
        {
            message = new
            {
                subject = notification.Subject,
                body = new
                {
                    contentType = "Text",
                    content = notification.PlainTextBody
                },
                toRecipients = new[]
                {
                    new
                    {
                        emailAddress = new
                        {
                            address = notification.RecipientAddress
                        }
                    }
                },
                replyTo = new[]
                {
                    new
                    {
                        emailAddress = new
                        {
                            address = notification.ReplyToAddress
                        }
                    }
                }
            },
            saveToSentItems = true
        };

    private static bool IsTransient(HttpStatusCode statusCode) =>
        statusCode == HttpStatusCode.TooManyRequests ||
        (int)statusCode >= 500;

    private static TimeSpan GetRetryDelay(HttpResponseMessage response)
    {
        if (response.Headers.RetryAfter?.Delta is { } delta)
        {
            return delta;
        }

        if (response.Headers.RetryAfter?.Date is { } retryAt)
        {
            var delay = retryAt - DateTimeOffset.UtcNow;
            return delay > TimeSpan.Zero ? delay : TimeSpan.Zero;
        }

        return TimeSpan.FromSeconds(1);
    }
}
