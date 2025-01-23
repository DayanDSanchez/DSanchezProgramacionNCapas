using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    public class CandidatoController : ApiController
    {
        [HttpGet]
        [Route("api/Candidato/GetAll")]
        public IHttpActionResult GetAll()
        {
            var result = BL.Candidato.CandidatoGetAllEF();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/Candidato/GetById/{IdCandidato}")]
        [HttpGet]
        public IHttpActionResult GetById(int IdCandidato)
        {
            ML.Result result = BL.Candidato.CandidatoGetByIdEF(IdCandidato);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpPost]
        [Route("api/Candidato/Add")]
        public IHttpActionResult Add([FromBody] ML.Candidato candidato)
        {
            var result = BL.Candidato.CandidatoAddEF(candidato);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/Candidato/Update/")]
        public IHttpActionResult Update([FromBody] ML.Candidato candidato)
        {
            var result = BL.Candidato.CandidatoUpdateEF(candidato);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/Candidato/Delete/{IdCandidato}")]
        // DELETE api/subcategoria/5
        public IHttpActionResult Delete(int IdCandidato)
        {
            ML.Candidato candidato = new ML.Candidato();
            var result = BL.Candidato.CandidatoDeleteEF(IdCandidato);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        //public class Numeros
        //{
        //    public int N1 { get; set; }
        //    public int N2 { get; set; }
        //}

        //[Route("api/Suma")]
        //[HttpPost]
        //public IHttpActionResult Suma([FromBody] Numeros request)
        //{
        //    int resultado = request.N1 + request.N2;
        //    return Content(HttpStatusCode.OK, resultado);
        //}

        //[Route("api/Resta")]
        //[HttpPost]
        //public IHttpActionResult Resta([FromBody] Numeros request)
        //{
        //    int resultado = request.N1 - request.N2;
        //    return Content(HttpStatusCode.OK, resultado);
        //}

        //[Route("api/Multi")]
        //[HttpPost]
        //public IHttpActionResult Multi([FromBody] Numeros request)
        //{
        //    int resultado = request.N1 * request.N2;
        //    return Content(HttpStatusCode.OK, resultado);
        //}

        //[Route("api/Div")]
        //[HttpPost]
        //public IHttpActionResult Div([FromBody] Numeros request)
        //{
        //    int resultado = request.N1 / request.N2;
        //    return Content(HttpStatusCode.OK, resultado);
        //}
        //public IHttpActionResult Suma(int N1, int N2)
        //{
        //    return Content(HttpStatusCode.OK, N1 + N2);
        //}

        //[System.Web.Http.Route("api/Resta")]
        //[System.Web.Http.HttpGet]
        //public IHttpActionResult Resta(int N1, int N2)
        //{
        //    return Content(HttpStatusCode.OK, N1 - N2);
        //}
        //[System.Web.Http.Route("api/Multi")]
        //[System.Web.Http.HttpGet]
        //public IHttpActionResult Multi(int N1, int N2)
        //{
        //    return Content(HttpStatusCode.OK, N1 * N2);
        //}
        //[System.Web.Http.Route("api/Division")]
        //[System.Web.Http.HttpGet]
        //public IHttpActionResult Division(int N1, int N2)
        //{
        //    return Content(HttpStatusCode.OK, N1 / N2);
        //}
        [Route("api/Cita")]
        [HttpGet]
        public IHttpActionResult CitaGetByIdCandidato(int IdCandidato)
        {
            ML.Result result = BL.Cita.CitaGetByIdCandidatoLinq(IdCandidato);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}
