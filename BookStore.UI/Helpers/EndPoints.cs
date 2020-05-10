namespace BookStore.UI.Helpers
{
    public static class EndPoints
    {
        public static string BaseURL { get; } = "http://localhost:5000/api/";
        public static string Authors { get; } = $"{BaseURL}authors";
        public static string Books { get; } = $"{BaseURL}books";
        public static string Registration { get; } = $"{BaseURL}users/register";
    }
}
