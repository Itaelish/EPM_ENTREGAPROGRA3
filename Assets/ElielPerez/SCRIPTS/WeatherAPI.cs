using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;

///<summary>
///Este script es para comunicarse con la API de la pagina
///https://openweathermap.org/current
///<summary>

public class WeatherAPI : MonoBehaviour
{

    [SerializeField] private WeatherData data;
    [SerializeField] private LightController lightController;

    private static readonly float latitude = 59.33f;
    private static readonly float longitud = 18.06f;
    private static readonly string units = "metric";
    private static readonly string apiKey = "f88003e6500d24ac7484986c6870c46a";

    private string getWeatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitud}&appid={apiKey}&units={units}";

    private string json;

    private void Start()
    {
        StartCoroutine(WeatherUpdate());
        InvokeRepeating("CallAPIEveryHour", 3600f, 3600f);
    }

    private void CallAPIEveryHour()
    {
        StartCoroutine(WeatherUpdate());
    }

    IEnumerator WeatherUpdate()
    {
        UnityWebRequest request = new UnityWebRequest(getWeatherUrl);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            json = request.downloadHandler.text;
        }

        DecodeJson(json);

        lightController.UpdateLight(data.temp, data.timezone);
    }

    private void DecodeJson(string json)
    {
        var weatherJson = JSON.Parse(json);

        data.country = weatherJson["sys"]["country"].Value;
        data.city = weatherJson["name"].Value;
        data.temp = float.Parse(weatherJson["main"]["temp"].Value);
        data.timezone = int.Parse(weatherJson["timezone"].Value);
    }
}
