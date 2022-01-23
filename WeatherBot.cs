using Newtonsoft.Json;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

class WeatherBot
{
    public static TelegramBotClient Client { get; private set; }

    public WeatherBot()
    { 
        string token = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN");
        Client = new TelegramBotClient(token) { Timeout = TimeSpan.FromSeconds(10) };
        Client.OnMessage += Bot_OnMessage;
    }

    public void start()
    {
        Client.StartReceiving();
    }

    public void stop()
    {
        Client.StopReceiving();
    }

    public static string getWeatherData(string cityName)
    {
        string response;
        string url = Environment.GetEnvironmentVariable("WEATHERMAP_URL") + cityName + Environment.GetEnvironmentVariable("WEATHERMAP_API_KEY");
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest?.GetResponse();

        using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
        {
            response = streamReader.ReadToEnd();
        }
        WeatherResponse weatherData = JsonConvert.DeserializeObject<WeatherResponse>(response);

        return weatherData.ToString();
    }

    private async void Bot_OnMessage(object sender, MessageEventArgs e)
    {
        var message = e.Message;
        try
        {
            if (message.Type == MessageType.Text)
            {
                string cityName = message.Text;
                await Client.SendTextMessageAsync(message.Chat.Id, getWeatherData(cityName)); 
            }
        }
        catch (WebException ex)
        {
            await Client.SendTextMessageAsync(message.Chat.Id, "No such city found");
        }
    }
}
