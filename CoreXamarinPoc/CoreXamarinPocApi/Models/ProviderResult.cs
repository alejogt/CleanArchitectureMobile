using System;
namespace poc.providers.api.Models
{
    public class ProviderResult
    {
        /// <summary>
		/// Is Success return.
		/// </summary>
		public bool Success { get; set; }

        /// <summary>
        /// JSON message from server return.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Response for the server message.
        /// </summary>
        public string Response { get; set; }

        public int CodeStatus { get; set; }
    }
}
