namespace CourierManagementExceptionLibaray
{

    [Serializable]
    public class TrackingNumberNotFoundExceptionption : Exception
    {
        public TrackingNumberNotFoundExceptionption() { }
        public TrackingNumberNotFoundExceptionption(string message) : base(message) { }
        public TrackingNumberNotFoundExceptionption(string message, Exception inner) : base(message, inner) { }
        protected TrackingNumberNotFoundExceptionption(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}