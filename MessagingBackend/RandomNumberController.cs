using Microsoft.AspNetCore.Mvc;
using System;

namespace MessagingBackend
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomNumberController : ControllerBase
    {
        [HttpGet("myrandomnumber")]
        public IActionResult GetRandomNumber()
        {
            Random random = new();
            int randomNumber = random.Next(1, 101); // Generates a random number between 1 and 100
            string message = $"Your random number is {randomNumber}, good luck!";
            return Ok(message);
        }
    }
}
