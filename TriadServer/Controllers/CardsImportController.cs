using TriadServer.Contexts;
using TriadServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TriadServer.Controllers
{
    public class CardsImportController : ControllerBase
    {
        public static Card card = new Card();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        //public readonly TriadContext _context;

        public CardsImportController(IConfiguration configuration, IUserService userService/*, TriadContext context*/)
        {
            _configuration = configuration;
            _userService = userService;
            //_context = context;
        }
        [HttpPost("import")]
        public async Task<ActionResult> Import()
        {
            string url = "https://triad.raelys.com/api/cards?language=fr";

            HttpClient client = new HttpClient();

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                using (HttpContent content = response.Content)
                {
                    var json = await content.ReadAsStringAsync();
                    var forecast2 = JsonConvert.DeserializeObject<Root>(json)!;
                    string cs = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Triad;Integrated Security=True;";
                    var con = new SqlConnection(cs);
                    var cmd = con.CreateCommand();
                    con.Open();
                    foreach (var result in forecast2.results)
                    {
                        //Console.WriteLine($"{result.id}) {result.name} {result.sell_price}");
                        //Console.WriteLine($"top:{result.stats.formatted.top} right:{result.stats.formatted.right} bottom:{result.stats.formatted.bottom} left:{result.stats.formatted.left}");

                        cmd.CommandText = "INSERT INTO Cards ([Id], [Name], [Description], [Stars], [Type], [Img], [Top], [Right], [Bottom], [Left], [SellPrice]) VALUES (@id, @name, @description, @stars, @type, @img, @top, @right, @bottom, @left, @sellPrice)"; 
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = result.id;
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = result.name;
                        cmd.Parameters.Add("@description", SqlDbType.NVarChar, 1000).Value = result.description;
                        cmd.Parameters.Add("@stars", SqlDbType.Int).Value = result.stars;
                        cmd.Parameters.Add("@type", SqlDbType.NVarChar, 25).Value = result.type.name;
                        cmd.Parameters.Add("@img", SqlDbType.NVarChar, 255).Value = result.image;
                        cmd.Parameters.Add("@top", SqlDbType.Int).Value = result.stats.numeric.top;
                        cmd.Parameters.Add("@right", SqlDbType.Int).Value = result.stats.numeric.right;
                        cmd.Parameters.Add("@bottom", SqlDbType.Int).Value = result.stats.numeric.bottom;
                        cmd.Parameters.Add("@left", SqlDbType.Int).Value = result.stats.numeric.left;
                        cmd.Parameters.Add("@sellPrice", SqlDbType.Int).Value = result.sell_price;
                        Console.WriteLine(result.id);
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                            //Console.WriteLine($"err");
                        }
                        cmd.Parameters.Clear();
                    }
                    //Console.WriteLine($"Ok");
                }
            }
            return Ok();
        }
        public class Formatted
        {
            public string top;
            public string right;
            public string bottom;
            public string left;
        }

        public class Location
        {
            public string name;
            public string region;
            public string x;
            public string y;
        }

        public class Npc
        {
            public int id;
            public int resident_id;
            public string name;
            public string difficulty;
            public bool excluded;
            public string patch;
            public string link;
            public Location location;
            public object quest;
            public List<string> rules;
            public List<int> rule_ids;
        }

        public class Numeric
        {
            public int top;
            public int right;
            public int bottom;
            public int left;
        }

        public class Query
        {
        }

        public class Result
        {
            public int id;
            public string name;
            public string description;
            public int stars;
            public string patch;
            public int sell_price;
            public int order_group;
            public int order;
            public int deck_order;
            public string number;
            public string icon;
            public string image;
            public string link;
            public Stats stats;
            public Type type;
            public string owned;
            public Sources sources;
        }

        public class Root
        {
            public Query query;
            public int count;
            public List<Result> results;
        }

        public class Sources
        {
            public List<Npc> npcs;
            public List<object> packs;
            public List<string> drops;
            public object purchase;
        }

        public class Stats
        {
            public Numeric numeric;
            public Formatted formatted;
        }

        public class Type
        {
            public int id;
            public string name;
            public object image;
        }
    }
}
