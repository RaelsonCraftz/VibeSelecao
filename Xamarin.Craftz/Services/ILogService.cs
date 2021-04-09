﻿using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Xamarin.Craftz.Services
{
    public interface ILogService
    {
        void LogAction(Action action, Action<string> onError = null, string caller = "", string errorMessage = null);
        Task LogActionAsync(Func<Task> task, Func<string, Task> onError = null, string caller = "", string errorMessage = null);
        Task LogRequestAsync(Func<Task> task, Func<string, Task> onError = null, string caller = "", string errorMessage = null);
        void MeasureTime(string consoleMessage, Action action, string caller = "");
    }

    public class LogService : ILogService
    {
        public void LogAction(Action action, Action<string> onError = null, [CallerMemberName] string caller = "", string errorMessage = null)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
#if !DEBUG
                Crashes.TrackError(ex, new Dictionary<string, string>
                {
                    { ex.GetType().Name, caller }
                });
#endif

                HandleErrorInternal(errorMessage, ex.Message, onError, ex.StackTrace);
            }
        }

        public async Task LogActionAsync(Func<Task> task, Func<string, Task> onError, [CallerMemberName] string caller = "", string errorMessage = null)
        {
            try
            {
                await task();
            }
            catch (Exception ex)
            {
#if !DEBUG
                Crashes.TrackError(ex, new Dictionary<string, string>
                {
                    { ex.GetType().Name, caller }
                });
#endif

                await HandleErrorInternalAsync(errorMessage, ex.Message, onError, ex.StackTrace);
            }
        }

        public async Task LogRequestAsync(Func<Task> task, Func<string, Task> onError, [CallerMemberName] string caller = "", string errorMessage = null)
        {
            try
            {
                await task();
            }
            catch (WebException exWeb)
            {
#if !DEBUG
                Crashes.TrackError(exWeb, new Dictionary<string, string>
                {
                    { exWeb.GetType().Name, caller }
                });
#endif

                if (exWeb.Response != null)
                {
                    using (var stream = new StreamReader(exWeb.Response.GetResponseStream()))
                        await HandleErrorInternalAsync(errorMessage, $"[{AppInfo.Name}] houve um erro de requisição: {JsonConvert.DeserializeObject(stream.ReadToEnd())}", onError, exWeb.StackTrace);
                    return;
                }

                await HandleErrorInternalAsync(errorMessage, exWeb.Message, onError, exWeb.StackTrace);
            }
            catch (Exception ex)
            {
#if !DEBUG
                Crashes.TrackError(ex, new Dictionary<string, string>
                {
                    { ex.GetType().Name, caller }
                });
#endif
                await HandleErrorInternalAsync(errorMessage, ex.Message, onError, ex.StackTrace);
            }
        }

        public void MeasureTime(string consoleMessage, Action action, [CallerMemberName] string caller = "")
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            LogAction(action, caller: caller);
            stopWatch.Stop();

            Console.WriteLine($"[{AppInfo.Name}] {consoleMessage} {stopWatch.ElapsedMilliseconds} ms");
        }

        private void HandleErrorInternal(string errorMessage, string exceptionMessage, Action<string> errorCallback, string stackTrace)
        {
            Console.WriteLine($"[{AppInfo.Name}] houve um erro: {exceptionMessage}");
            Console.WriteLine($"[{AppInfo.Name}] stack trace do erro: {stackTrace}");
            errorCallback?.Invoke(errorMessage ?? "Houve algum erro, contate o desenvolvedor");
        }

        private async Task HandleErrorInternalAsync(string errorMessage, string exceptionMessage, Func<string, Task> errorCallback, string stackTrace)
        {
            Console.WriteLine($"[{AppInfo.Name}] houve algum erro genérico: {exceptionMessage}");
            Console.WriteLine($"[{AppInfo.Name}] stack trace do erro: {stackTrace}");
            await errorCallback?.Invoke(errorMessage ?? "Houve algum erro, contate o desenvolvedor");
        }
    }
}
