using DoctorWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DoctorWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EspecialidadController : ApiController
    {
        [HttpGet]
        public IEnumerable<EspecialidadCLS> listarEspecialidad()
        {
            using (BDDoctorDataContext bd = new BDDoctorDataContext())
            {
                IEnumerable<EspecialidadCLS> listaEspecialidad = (from especialidad in bd.Especialidad
                                                               where especialidad.BHABILITADO == 1
                                                               select  new EspecialidadCLS
                                                               { 
                                                                   iidEspecialidad = especialidad.IIDESPECIALIDAD,
                                                                   nombreEspecialidad = especialidad.NOMBRE
                                                               }).ToList();
                return listaEspecialidad;
            }
        }
    }
}
