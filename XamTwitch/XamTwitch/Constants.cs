using System;

namespace XamTwitch
{
    public static class Constants
    {
        // Hardcoded anonymous ClientId used by Twitch
        public const string TwitchAnonymousClientId = "kimne78kx3ncx6brgo4mv6wki5h1ko";

        // dev.twitch.tv application settings
        public const string TwitchApiKey = "<TWITCH_CLIENT_ID>";
        public const string TwitchApiUri = "https://api.twitch.tv/";
        public const string TwitchPlaylistApiUri = "https://usher.ttvnw.net/";
        public const string ClientIDHeaderKey = "Client-ID";
        public static string AppName = "XamTwitch";

        // OAuth
        public static string iOSClientId = "<TWITCH_CLIENT_ID>";
        public static string AndroidClientId = "<TWITCH_CLIENT_ID>";

        // These values do not need changing
        public static string Scope = "user:read:email";
        public static string AuthorizeUrl = "https://id.twitch.tv/oauth2/authorize";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string iOSRedirectUrl = "https://localhost:/oauth2redirect";
        public static string AndroidRedirectUrl = "https://localhost:/oauth2redirect";
    }
}
