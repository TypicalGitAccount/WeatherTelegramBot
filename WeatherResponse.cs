public class WeatherResponse
{
    public string name { get; set; }
    public Dictionary<string, float> coord { get; set; } = new Dictionary<string, float>();
    public Dictionary<string, float> main { get; set; } = new Dictionary<string, float>();
    public Dictionary<string, float> wind { get; set; } = new Dictionary<string, float>();
    public Dictionary<string, float> clouds { get; set; } = new Dictionary<string, float>();

    private float FarToCelsius(float far)
    {
        return far - 273;
    }

    public override string ToString()
    {
        return $"City - {name}\nlocation : longitude - {coord["lon"]}, latitude - {coord["lat"]}\n" +
            $"Current temperature : {Math.Round(FarToCelsius(main["temp"]), 2)}," +
            $"feels like  {Math.Round(FarToCelsius(main["feels_like"]), 2)}\n" +
            $"Min temp : {Math.Round(FarToCelsius(main["temp_min"]))}, max temp" +
            $": {Math.Round(FarToCelsius(main["temp_max"]))}\n" + 
            $"Pressure : {main["pressure"]}, humidity : {main["humidity"]}\n" +
            $"Wind speed : {wind["speed"]}, clouds : {clouds["all"]}";
    }
}