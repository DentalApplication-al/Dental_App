namespace DentalApplication.Common
{
    public class BlobResponse
    {
        public dynamic? data { get; set; }
        public bool hasSucceded { get; set; }
        public static BlobResponse Success(dynamic data)
        {
            return new BlobResponse
            {
                data = data,
                hasSucceded = true,

            };
        }

        public static BlobResponse Fail()
        {
            return new BlobResponse
            {
                data = null,
                hasSucceded = false,

            };
        }
    }
}
