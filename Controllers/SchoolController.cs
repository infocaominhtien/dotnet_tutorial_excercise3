using System.Net;
using HttpClientProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HttpClientProject.Controllers;

[ApiController]
[Route("[controller]")]
public class SchoolController
{
    [HttpGet]
    public async Task<SchoolModel?> GetSchool(int id)
    {
        var url = "https://localhost:7066/api/School/" + id;
        try
        {
            var result = await HttpClientHelper.GetAsync<SchoolModel>(url);
            return result;
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("School not found");
            }
        }

        return null;
    }

    [HttpGet("multi")]
    public async Task<IEnumerable<SchoolModel?>> GetMultiSchoolAtOneTime([FromQuery] int[] ids)
    {
        if (ids.Length == 0)
        {
            return new List<SchoolModel?> { };
        }
        var baseUrl = "https://localhost:7066/api/School/{0}";
        var tasks = ids.Select(i => HttpClientHelper.GetAsync<SchoolModel>(string.Format(baseUrl, i)));
        var result = (await Task.WhenAll(tasks));
        return result;
    }
}