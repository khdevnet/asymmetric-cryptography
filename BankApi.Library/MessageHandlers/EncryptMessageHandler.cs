﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BankApi.Library.MessageHandlers
{
    public class EncryptMessageHandler : CryptographyMessageHandlerBase
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestContent = request.Content.ReadAsStringAsync().Result;

            Log.Info("Request News Subscribe Web NormalContent: " + requestContent);
            request.Content = EncryptContent(requestContent);
            Log.Info("Request News Subscribe Web EncryptedContent: " + request.Content.ReadAsStringAsync().Result);

            Log.Info("=====================================");

            var response = await base.SendAsync(request, cancellationToken);

            var responseContent = response.Content.ReadAsStringAsync().Result;

            Log.Info("Response News Subscribe Web DecryptedContent: " + responseContent);
            response.Content = DecryptContent(responseContent);
            Log.Info("Response News Subscribe Web NormalResponseContent: " + response.Content.ReadAsStringAsync().Result);
            return response;
        }
    }
}