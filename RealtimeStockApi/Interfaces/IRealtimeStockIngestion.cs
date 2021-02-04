namespace RealtimeStockApi
{
    /// <summary>
    /// Realtime stock ingestion interface
    /// </summary>
    public interface IRealtimeStockIngestion
    {
        /// <summary>
        /// Starts the ingestion.
        /// </summary>
        void StartIngestion();
    }
}
