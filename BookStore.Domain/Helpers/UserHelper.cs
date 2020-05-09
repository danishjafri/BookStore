namespace BookStore.Domain.Helpers
{
    public static class UserHelper
    {
        internal static class Roles
        {
            public static string Administrator { get; } = "Administrator";
            public static string Customer { get; } = "Customer";
        }

        internal static class Credentials
        {
            public static string AdminUserName { get; } = "Administrator";
            public static string AdminEmail { get; } = "admin@bookstore.com";
            public static string AdminPassword { get; } = "p@SSword!2";

            public static string CustomerUserName { get; } = "Customer";
            public static string CustomerEmail { get; } = "customer@gmail.com";
            public static string CustomerPassword { get; } = "p@S$word!7";
        }
    }
}
