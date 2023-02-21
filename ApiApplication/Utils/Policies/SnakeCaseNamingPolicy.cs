using ApiApplication.Extensions;
using System.Text.Json;

namespace ApiApplication.Utils
{
    //https://maximgorbatyuk.github.io/blog/development/2021-02-20-snake-case-and-asp-net-core/
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToSnakeCase();
    }
}
