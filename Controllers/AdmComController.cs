using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdmComWebApplication.Models;

namespace AdmComWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmComController : ControllerBase
    {
        private static readonly IList<Entrant> entrantsList = new List<Entrant>();

        [HttpGet]
        public IList<Entrant> Get()
        {
            return entrantsList;
        }

        [HttpGet("StatusBar")]
        public StatusBar GetStatus()
        {
            var status = new StatusBar()
            {
                AmountEntrants = entrantsList.Count(),
                AmountPassedEntrants = entrantsList.Where(x => x.SumExams > 150).Count(),
            };
            return status;
        }

        [HttpPost]
        public Entrant AddEntrant(EnteredEntrant value)
        {
            var entrant = new Entrant()
            {
                Id = Guid.NewGuid(),
                FullName = value.FullName,
                Gender = value.Gender,
                BirthDate = value.BirthDate,
                EducationForm = value.EducationForm,
                MathExams = value.MathExams,
                RussianExams = value.RussianExams,
                ITExams = value.ITExams,
                SumExams = value.MathExams + value.RussianExams + value.ITExams,
            };
            entrantsList.Add(entrant);
            return entrant;
        }

        [HttpPut("{id}")]
        public Entrant EditEntrant([FromRoute] Guid id, [FromBody] EnteredEntrant value)
        {
            var entrant = entrantsList.FirstOrDefault(x => x.Id == id);
            if (entrant != null)
            {
                var index = entrantsList.IndexOf(entrant);

                entrant.FullName = value.FullName;
                entrant.Gender = value.Gender;
                entrant.BirthDate = value.BirthDate;
                entrant.EducationForm = value.EducationForm;
                entrant.MathExams = value.MathExams;
                entrant.RussianExams = value.RussianExams;
                entrant.ITExams = value.ITExams;
                entrant.SumExams = value.MathExams + value.RussianExams + value.ITExams;

                entrantsList[index] = entrant;
            }
            return entrant;
        }

        [HttpDelete]
        public bool DeleteEntrant(Guid id)
        {
            var entrant = entrantsList.FirstOrDefault(x => x.Id == id);
            if (entrant != null)
            {
                return entrantsList.Remove(entrant);
            }
            return false;
        }
    }
}
