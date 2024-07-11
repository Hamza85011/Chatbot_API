using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;

namespace Chatbot_API.Controllers
{
    public class OpenAIController : Controller
    {
        [HttpPost]
        [Route("getanswer")]
        public IActionResult GetResult([FromBody] string prompt)
        {
            string apiKey = "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";
            string answer = string.Empty;

            try
            {
                var openai = new OpenAIAPI(apiKey);
                CompletionRequest completion = new CompletionRequest();
                completion.Prompt = prompt;
                completion.Model = "text-davinci-002";
                completion.MaxTokens = 4000;

                var result = openai.Completions.CreateCompletionAsync(completion).Result;

                if (result != null && result.Completions.Count > 0)
                {
                    answer = result.Completions[0].Text;
                    return Ok(answer);
                }
                else
                {
                    return BadRequest("No completion response received.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
