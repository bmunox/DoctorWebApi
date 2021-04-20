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
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class ClinicaController : ApiController
    {
        [HttpGet]
        public IEnumerable<ClinicaCLS> listarClinica()
        {
            using (BDDoctorDataContext bd = new BDDoctorDataContext())
            {
                IEnumerable<ClinicaCLS> listaClinica = (from clinica in bd.Clinica
                                                        where clinica.BHABILITADO == 1
                                                        select new ClinicaCLS 
                                                        {
                                                            iidClinica =  clinica.IIDCLINICA,
                                                            nombreClinica = clinica.NOMBRE
                                                        }).ToList();
                return listaClinica;
            }
        }
    }
}
