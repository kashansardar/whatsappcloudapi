// <copyright file="MessagesController.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace WhatsAppCloudAPI.Standard.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Converters;
    using WhatsAppCloudAPI.Standard;
    using WhatsAppCloudAPI.Standard.Authentication;
    using WhatsAppCloudAPI.Standard.Http.Client;
    using WhatsAppCloudAPI.Standard.Http.Request;
    using WhatsAppCloudAPI.Standard.Http.Request.Configuration;
    using WhatsAppCloudAPI.Standard.Http.Response;
    using WhatsAppCloudAPI.Standard.Utilities;

    /// <summary>
    /// MessagesController.
    /// </summary>
    public class MessagesController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        /// </summary>
        /// <param name="config"> config instance. </param>
        /// <param name="httpClient"> httpClient. </param>
        /// <param name="authManagers"> authManager. </param>
        internal MessagesController(IConfiguration config, IHttpClient httpClient, IDictionary<string, IAuthManager> authManagers)
            : base(config, httpClient, authManagers)
        {
        }

        /// <summary>
        /// Use this endpoint to send text messages, media, message templates to your customers.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="body">Required parameter: To send a message, you must first assemble a message object with the content you want to send..</param>
        /// <returns>Returns the Models.SendMessageResponse response from the API call.</returns>
        public Models.SendMessageResponse SendMessage(
                string phoneNumberID,
                Models.Message body)
        {
            Task<Models.SendMessageResponse> t = this.SendMessageAsync(phoneNumberID, body);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Use this endpoint to send text messages, media, message templates to your customers.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="body">Required parameter: To send a message, you must first assemble a message object with the content you want to send..</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.SendMessageResponse response from the API call.</returns>
        public async Task<Models.SendMessageResponse> SendMessageAsync(
                string phoneNumberID,
                Models.Message body,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/{Phone-Number-ID}/messages");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "Phone-Number-ID", phoneNumberID },
            });

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
                { "accept", "application/json" },
                { "Content-Type", "application/json" },
            };

            // append body params.
            var bodyText = ApiHelper.JsonSerialize(body);

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().PostBody(queryBuilder.ToString(), headers, bodyText);

            httpRequest = await this.AuthManagers["global"].ApplyAsync(httpRequest).ConfigureAwait(false);

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return ApiHelper.JsonDeserialize<Models.SendMessageResponse>(response.Body);
        }

        /// <summary>
        /// When you receive an incoming message from Webhooks, you could use messages endpoint to change the status of it to read.  .
        /// We recommend marking incoming messages as read within 30 days of receipt.  .
        /// **Note**: You cannot mark outgoing messages you sent as read.
        /// You need to obtain the **`message_id`** of the incoming message from Webhooks.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="body">Required parameter: Example: .</param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public Models.SuccessResponse MarkMessageAsRead(
                string phoneNumberID,
                Models.MarkMessageAsReadRequest body)
        {
            Task<Models.SuccessResponse> t = this.MarkMessageAsReadAsync(phoneNumberID, body);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// When you receive an incoming message from Webhooks, you could use messages endpoint to change the status of it to read.  .
        /// We recommend marking incoming messages as read within 30 days of receipt.  .
        /// **Note**: You cannot mark outgoing messages you sent as read.
        /// You need to obtain the **`message_id`** of the incoming message from Webhooks.
        /// </summary>
        /// <param name="phoneNumberID">Required parameter: Example: .</param>
        /// <param name="body">Required parameter: Example: .</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.SuccessResponse response from the API call.</returns>
        public async Task<Models.SuccessResponse> MarkMessageAsReadAsync(
                string phoneNumberID,
                Models.MarkMessageAsReadRequest body,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/{Phone-Number-ID}/messages");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "Phone-Number-ID", phoneNumberID },
            });

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
                { "accept", "application/json" },
                { "Content-Type", "application/json" },
            };

            // append body params.
            var bodyText = ApiHelper.JsonSerialize(body);

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().PutBody(queryBuilder.ToString(), headers, bodyText);

            httpRequest = await this.AuthManagers["global"].ApplyAsync(httpRequest).ConfigureAwait(false);

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return ApiHelper.JsonDeserialize<Models.SuccessResponse>(response.Body);
        }
    }
}