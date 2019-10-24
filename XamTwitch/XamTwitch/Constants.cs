using System;
namespace XamTwitch
{
    public static class Constants
    {
        public const string TwitchApiKey = "if8owq3ywwqc2sn9q9wrwnpr6yyjtp";
        public const string TwitchApiUri = "https://api.twitch.tv/";
        public const string TwitchPlaylistApiUri = "https://usher.ttvnw.net/";
        public const string ClientIDHeaderKey = "Client-ID";

        public static string AppName = "XamTwitch";

        // OAuth
        public static string iOSClientId = "qk1v6l0d2nodm1bkk6sqv6ivueh9j2";
        public static string AndroidClientId = "y90kgs7q4kow9kczfa6wxalpaeja62";

        // These values do not need changing
        public static string Scope = "user:read:email";
        public static string AuthorizeUrl = "https://id.twitch.tv/oauth2/authorize";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string iOSRedirectUrl = "https://localhost:/oauth2redirect";
        public static string AndroidRedirectUrl = "https://localhost:/oauth2redirect";
    }
}
