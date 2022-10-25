using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace printing_calculator.controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaperEditController : ControllerBase
    {
        // PUT api/<PaperEditController>/5
        [HttpPut]
        public bool Put(Test test)
        {
            return true;
        }

        // DELETE api/<PaperEditController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return true;
        }
    }
}

public class Test
{
    public int id { get; set; }
    public float newPrice { get; set; }
    public int status { get; set; }
}