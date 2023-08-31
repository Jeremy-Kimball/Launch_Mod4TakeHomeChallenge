using CommerceAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CommerceAPI.Controllers
{
    [Route("/api/merchants/[controller]/[action]")]
    [ApiController]
    public class StatisticsController : Controller
    {
        private readonly CommerceApiContext _context;

        public StatisticsController(CommerceApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ContentResult CountByCategory()
        {
            var test = _context.Products.GroupBy(p => p.Category).Select(group => new
            {
                Key = group.Key,
                Value = group.Count()
            });
            string jsonString = "{";
            foreach(var group in test)
            {
                jsonString += $"\"{group.Key}\": {group.Value},";
            }
            jsonString = jsonString.TrimEnd(',') + "}";
            return Content(jsonString, "application/json");
        }
    }
}
