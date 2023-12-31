﻿using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http.Headers;
using System.Reflection;
using static TourPlanner1.Model.RouteResponse;
using static System.Net.WebRequestMethods;
using SixLabors.ImageSharp; //using this because it is compatible with windows and mac
using TourPlanner1.Model;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using TourPlanner1.Utility;
using iText.Kernel.Pdf;

namespace TourPlanner1.Utility
{
    public class MapHandler
    {
        readonly HttpClient client = new();
        string key;
        int imageWidth;
        int imageHeight;
        private static IConfig _config;

        /// <summary>
        /// constuctor initializes variables and sets HttpClient base address
        /// </summary>
        public MapHandler(IConfig config)
        {
            _config = config;
            InitializeMapHandler();
            client.BaseAddress = new Uri("https://www.mapquestapi.com");
        }

        /// <summary>
        /// makes two requests to mapquest api to get the route and its image, stores the info in tour object and returns it
        /// </summary>
        /// <param name="fromLocation"></param>
        /// <param name="toLocation"></param>
        /// <returns>tour object</returns>
        public Tour GetRoute(string fromLocation, string toLocation, string description, string name)
        {
            Root root = GetRouteAsync(fromLocation, toLocation).Result;
            Image<Rgba32> mapImage;
            try
            {
                mapImage = GetRouteImageAsync(root.route.sessionId).Result;
            }
            catch
            {
                return null;
            }
            string uniqueFilename = SaveToFile(mapImage);
            return BuildNewTour(fromLocation, toLocation, root, uniqueFilename, description, name);
        }

        /// <summary>
        /// get route from API https://www.mapquestapi.com/directions/v2/
        /// </summary>
        /// <param name="fromLocation"></param>
        /// <param name="toLocation"></param>
        /// <returns>route task</returns>
        private async Task<Root> GetRouteAsync(string fromLocation, string toLocation)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Root root;
            HttpResponseMessage response = client.GetAsync(BuildRouteEndpoint(fromLocation, toLocation, key, client)).Result;
            response.EnsureSuccessStatusCode();
            root = await response.Content.ReadFromJsonAsync<Root>();
            return root;
        }

        /// <summary>
        /// concatinates the base address, key, fromlocation and to locatin to build the endpoint
        /// </summary>
        /// <param name="fromLocation"></param>
        /// <param name="toLocation"></param>
        /// <param name="key"></param>
        /// <param name="client"></param>
        /// <returns>endpoint string</returns>
        public static string BuildRouteEndpoint(string fromLocation, string toLocation, string key, HttpClient client)
        {
            string endpoint = client.BaseAddress.ToString() +
                "directions/v2/route?key=" + key +
                "&from=" + fromLocation +
                "&to=" + toLocation +
                "&outFormat=json&ambiguities=ignore&routeType=fastest&doReverseGeocode=false&enhancedNarrative=false&avoidTimedConditions=false";
            return endpoint;
        }

        /// <summary>
        /// uses sessionId from route call to get the image from the api https://www.mapquestapi.com/staticmap/v5/
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>image</returns>
        private async Task<Image<Rgba32>> GetRouteImageAsync(string sessionId)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));
            HttpResponseMessage response = client.GetAsync(BuildRouteImageEndpoint(sessionId, key, imageWidth, imageHeight, client)).Result;
            response.EnsureSuccessStatusCode();

            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

            using MemoryStream memoryStream = new(imageBytes);
            Image<Rgba32> mapImage = Image.Load<Rgba32>(memoryStream);
            return mapImage;
        }

        /// <summary>
        /// build the endpoint for image retrieval
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="key"></param>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        /// <param name="client"></param>
        /// <returns>endpoint string</returns>
        public static string BuildRouteImageEndpoint(string sessionId, string key, int imageWidth, int imageHeight, HttpClient client)
        {
            string endpoint = client.BaseAddress.ToString() + "staticmap/v5/map?key=" + key + "&session=" + sessionId + "&size=" + imageWidth + "," + imageHeight;
            return endpoint;
        }

        /// <summary>
        /// sets key, imagewidth and imageheight with values from config file
        /// </summary>
        private void InitializeMapHandler()
        {
            key = GetKey();
            imageWidth = Int32.Parse(GetWidth());
            imageHeight = Int32.Parse(GetHeight());
        }

        /// <summary>
        /// gets key from config class
        /// </summary>
        /// <returns>key string</returns>
        private static string GetKey()
        {
            return _config.MapHandlerKey;
        }

        /// <summary>
        /// gets width from config class
        /// </summary>
        /// <returns>width string</returns>
        private static string GetWidth()
        {
            return _config.MapHandlerWidth;
        }

        /// <summary>
        /// gets height from config class
        /// </summary>
        /// <returns>height string</returns>
        private static string GetHeight()
        {
            return _config.MapHandlerHeight;
        }

        /// <summary>
        /// build tour object from  parameters
        /// </summary>
        /// <param name="fromLocation"></param>
        /// <param name="toLocation"></param>
        /// <param name="root"></param>
        /// <param name="uniqueFilename"></param>
        /// <param name="description"></param>
        /// <param name="name"></param>
        /// <returns>newly built tour object</returns>
        public static Tour BuildNewTour(string fromLocation, string toLocation, Root root, string uniqueFilename, string description, string name)
        {
            Tour tour = new()
            {
                FromLocation = fromLocation,
                ToLocation = toLocation,
                TransportType = root.route.legs[0].maneuvers[0].transportMode,
                TourDistance = (int)Math.Round(root.route.distance * 1.609),
                EstimatedTime = root.route.realTime,
                RouteImage = uniqueFilename,
                Description = description,
                Name = name
            };
            return tour;
        }

        /// <summary>
        /// gets the path to Images folder, creates unique ID for the image, saves it to  the file system
        /// </summary>
        /// <param name="mapImage"></param>
        /// <returns>uniqueFilename</returns>
        private string SaveToFile(Image<Rgba32> mapImage)
        {
            if(_config.Mode == "unitTest")
            {
                return "image.jpg";
            }
            string imagesPath = PathHelper.GetBasePath() + "\\Images";
            var uniqueFilename = string.Format(@"{0}.jpg", Guid.NewGuid());
            string path = Path.Combine(imagesPath, uniqueFilename);
            mapImage.SaveAsJpeg(path);
            return uniqueFilename;

        }
    }
}