using Brewery.Server.Core;
using Brewery.Server.Logic;
using Newtonsoft.Json;
using Restup.Webserver;
using Restup.Webserver.Attributes;
using Restup.Webserver.Http;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace Brewery.ServerMock
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeWebServer();
        }

        private async Task InitializeWebServer()
        {
            var server = IocContainer.GetInstance<IServer>();
            await server.StartServerAsync();
        }

        public async void GetServerStatus(object sender, RoutedEventArgs e)
        {
            await SendRequest<EmptyResp>("serverStatus", MethodTypes.GET);
        }

        public async void MixerPowerOn(object sender, RoutedEventArgs e)
        {
            await SendRequest<EmptyResp>("/mixer/power/true", MethodTypes.PUT);
        }

        public async void PiezoPowerOn(object sender, RoutedEventArgs e)
        {
            await SendRequest<EmptyResp>("/piezo/power/true", MethodTypes.PUT);
        }

        public async void GetBoilingPlate1PowerStatus(object sender, RoutedEventArgs e)
        {
            var resutl = await SendRequest<TestResp>("boilingPlate1/powerStatus", MethodTypes.GET);
        }

        enum MethodTypes
        {
            GET = 0,
            PUT = 1
        }

        private async Task<T> SendRequest<T>(string method, MethodTypes methodType, string body = null)
        {
            var requestUri = $"http://tab-128a:8800/api/{method}";

            var webRequest = WebRequest.CreateHttp(requestUri);
            webRequest.Accept = "application/json";
            webRequest.Method = methodType.ToString();

            if (webRequest.Method != MethodTypes.GET.ToString() && !string.IsNullOrWhiteSpace(body))
            {
                webRequest.ContentType = "application/json";

                using (var requestStream = await webRequest.GetRequestStreamAsync())
                {
                    using (var streamWriter = new StreamWriter(requestStream))
                    {
                        await streamWriter.WriteAsync(body);
                    }
                }
            }

            using (var response = await webRequest.GetResponseAsync())
            {
                var responseStream = response.GetResponseStream();
                using (var streamReader = new StreamReader(responseStream))
                {
                    var readAll = await streamReader.ReadToEndAsync();

                    WriteToOutput((HttpWebResponse)response, readAll);
                    return JsonConvert.DeserializeObject<T>(readAll);
                }
            }
        }

        private void WriteToOutput(HttpWebResponse response, string readAll)
        {            
            Debug.WriteLine($"Status: {response.StatusCode}");
            Debug.WriteLine(readAll);
        }
    }

    public class EmptyResp
    {
    }

    public class TestResp
    {
        public bool Power { get; set; }
    }
}
