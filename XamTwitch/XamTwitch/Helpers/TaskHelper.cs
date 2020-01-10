using System;
using System.Threading.Tasks;

namespace XamTwitch.Helpers
{
    public static class TaskHelper
    {
        public static void HandleResult(this Task source)
        {
            if (source == null)
                return;

            source.ContinueWith(r =>
            {
                var ex = r.Exception?.Flatten();
                System.Diagnostics.Debug.WriteLine(ex);
                Microsoft.MobCAT.Logger.Error(ex);
            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
