namespace OrderService.Filters
{
    public class ServiceResponseError
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public object? Data { get; set; }

        /// <summary>
        /// Gets or sets the status. 
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string? Status { get; set; }


        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string? Message { get; set; }

    }
}
