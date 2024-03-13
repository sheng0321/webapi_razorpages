using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitWebApp.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Diagnostics;
using Microsoft.Extensions.Logging;


namespace FruitWebApp.Pages
{
    public class AddModel : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;



        public AddModel(IHttpClientFactory httpClientFactory, ILogger<AddModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // Add the data model and bind the form data to the page model properties
        [BindProperty]
        public FruitModel FruitModels { get; set; }

        // Begin POST operation code
        public async Task<IActionResult> OnPost()
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(FruitModels), Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient("FruitAPI");
            using HttpResponseMessage response = await httpClient.PostAsync("", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Fruit added successfully!";
                _logger.LogInformation($"{FruitModels.name} added successfully!");
                return RedirectToPage("Index");
            }
            else
            {
                TempData["failure"] = "Failed to add fruit. Please try again.";
                return RedirectToPage("Index");
            }
        }

        // End POST operation code
    }
}

