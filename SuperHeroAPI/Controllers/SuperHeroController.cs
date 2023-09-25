using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.NET7.Models;
using System.Text.Json;

namespace SuperHeroAPI.NET7.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SuperHeroController : ControllerBase
	{
		public static List<SuperHero> superHeroes = new List<SuperHero> {
			new SuperHero {
				Id = 1,
				Name = "Spider Man",
				FirstName="Peter",
				LastName="Parker",
				Place="New York City"
			},
			new SuperHero {
				Id = 2,
				Name = "Iron Man",
				FirstName="Tony",
				LastName="Stark",
				Place="Malibu"
			},
		};

		[HttpGet]
		public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()  // IActionResult means status code.
		{
			return Ok(superHeroes);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<SuperHero>> GetSingleHero(int id) 
		{
			var hero = superHeroes.Find(x => x.Id == id);
			if (hero == null)
				return NotFound($"Hero with id '{id}' not found.");

			return Ok(hero);
		}

		[HttpPost]
		public async Task<ActionResult<List<SuperHero>>> AddHero([FromBody]SuperHero hero)
		{
			superHeroes.Add(hero);

			return Ok(superHeroes);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, [FromBody] SuperHero requestBody)
		{
			var hero = superHeroes.Find(x => x.Id == id);
			if (hero == null)
				return NotFound($"Hero with id '{id}' not found.");

			if (requestBody.Name != "") hero.Name = requestBody.Name;
			if (requestBody.FirstName != "") hero.FirstName = requestBody.FirstName;
			if (requestBody.LastName != "") hero.LastName = requestBody.LastName;
			if (requestBody.Place != "") hero.Place = requestBody.Place;

			return Ok(superHeroes);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
		{
			var hero = superHeroes.Find(x => x.Id == id);
			if (hero == null)
				return NotFound($"Hero with id '{id}' not found.");
			superHeroes.Remove(hero);

			var resposehehe = new {
				deletedId = id,
				result = superHeroes
			};

			return Ok(resposehehe);
		}
	}
}
