using MapLocation.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MapLocation.Controllers
{
    public class Museum : Controller
    {
        private readonly HttpClient _httpClient;

        public Museum(IHttpClientFactory httpfact)
        {
            _httpClient = httpfact.CreateClient();
        }
        public async Task<IActionResult> Location(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/MuseumLocation?MId=39");
            //var response = await _httpClient.GetAsync($"https://localhost:5001/api/museum/location/{id}");
            if(!response.IsSuccessStatusCode)
                return View("Error");

            var json=await response.Content.ReadAsStringAsync();
            var location= JsonConvert.DeserializeObject<MuseumLocationViewModel>(json);
            return View(location);
        }
    }
}
