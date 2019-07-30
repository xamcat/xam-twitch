using System;
using System.Net.Http;
using Microsoft.MobCAT.Services;

namespace XamTwitch.Services
{
    public class TwitchHttpService : BaseHttpService, ITwitchHttpService
    {
        public TwitchHttpService() : base(Constants.TwitchApiUri, handler: null)
        {
        }
    }
}
