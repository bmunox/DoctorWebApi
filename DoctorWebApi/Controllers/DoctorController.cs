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
    public class DoctorController : ApiController
    {
        [HttpGet]
        public IEnumerable<DoctorCLS> listarDoctor()
        {
            using (BDDoctorDataContext bd = new BDDoctorDataContext())
            {
                IEnumerable<DoctorCLS> listaDoctor = (from doctor in bd.Doctor
                                                      join clinica in bd.Clinica
                                                      on doctor.IIDCLINICA equals
                                                      clinica.IIDCLINICA
                                                      join especialidad in bd.Especialidad
                                                      on doctor.IIDESPECIALIDAD equals
                                                      especialidad.IIDESPECIALIDAD
                                                      where doctor.BHABILITADO == 1
                                                      select new DoctorCLS
                                                      {
                                                          iidDoctor = doctor.IIDDOCTOR,
                                                          nombreCompleto = doctor.NOMBRE + " " + doctor.APPATERNO + " " + doctor.APMATERNO,
                                                          nombreClinica = clinica.NOMBRE,
                                                          nombreEspecialidad = especialidad.NOMBRE,
                                                          email = doctor.EMAIL,
                                                          fechaContrato = ((DateTime)doctor.FECHACONTRATO)
                                                      }).ToList();

                return listaDoctor;
            }
        }
        [HttpGet]
        public DoctorCLS recuperarDoctor(int iidDoctor)
        {
            try
            {
                using (BDDoctorDataContext bd = new BDDoctorDataContext())
                {
                    DoctorCLS oDoctor = (from Doctor in bd.Doctor
                                         where Doctor.IIDDOCTOR == iidDoctor
                                         select new DoctorCLS
                                         { 
                                             iidDoctor = Doctor.IIDDOCTOR,
                                             nombre = Doctor.NOMBRE,
                                             apPaterno = Doctor.APPATERNO,
                                             apMaterno = Doctor.APMATERNO,
                                             email = Doctor.EMAIL,
                                             fechaContrato = ((DateTime)Doctor.FECHACONTRATO),
                                             archivo = Doctor.ARCHIVO,
                                             nombreArchivo = Doctor.NOMBREARCHIVO,
                                             iidSexo = ((int)Doctor.IIDSEXO),
                                             telfCelular = Doctor.TELEFONOCELULAR,
                                             sueldo = ((decimal)Doctor.SUELDO),
                                             iidClinica = ((int)Doctor.IIDCLINICA),
                                             iidEspecialidad = ((int)Doctor.IIDESPECIALIDAD)
                                         }).First();
                    return oDoctor;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPut]
        public int eliminarDoctor(int iidDoctor)
        {
            int rpta = 0;
            try
            {
                using (BDDoctorDataContext bd = new BDDoctorDataContext())
                {
                    Doctor oDoctor = bd.Doctor.Where(p => p.IIDDOCTOR == iidDoctor).First();
                    oDoctor.BHABILITADO = 0;
                    bd.SubmitChanges();
                    rpta = 1;

                    /*
                    var oDoctor2 = (from Doctor in bd.Doctor
                               where Doctor.IIDDOCTOR == iidDoctor select Doctor).First();
                    bd.Doctor.DeleteOnSubmit(oDoctor2);
                    bd.SubmitChanges();
                    */
                }
            }
            catch (Exception ex)
            {
                rpta = 0;
            }
            return rpta;
        }

        [HttpPost]
        public int agregarDoctor([FromBody]Doctor oDoctor)
        {
            int rpta = 0;
            try
            {
                using (BDDoctorDataContext bd = new BDDoctorDataContext())
                {
                    if (oDoctor.IIDDOCTOR == 0)//Crear uno nuevo
                    {
                        bd.Doctor.InsertOnSubmit(oDoctor);
                        bd.SubmitChanges();
                        rpta = 1;
                    }
                    else//Editar uno existente
                    {
                        Doctor oDoctorItem = bd.Doctor.Where(p => p.IIDDOCTOR == oDoctor.IIDDOCTOR).First();
                        oDoctorItem.NOMBRE = oDoctor.NOMBRE;
                        oDoctorItem.APPATERNO = oDoctor.APPATERNO;
                        oDoctorItem.APMATERNO = oDoctor.APMATERNO;
                        oDoctorItem.EMAIL = oDoctor.EMAIL;
                        oDoctorItem.FECHACONTRATO = oDoctor.FECHACONTRATO;
                        oDoctorItem.ARCHIVO = oDoctor.ARCHIVO;
                        oDoctorItem.NOMBREARCHIVO = oDoctor.NOMBREARCHIVO;
                        oDoctorItem.IIDSEXO = oDoctor.IIDSEXO;
                        oDoctorItem.TELEFONOCELULAR = oDoctor.TELEFONOCELULAR;
                        oDoctorItem.SUELDO = oDoctor.SUELDO;
                        oDoctorItem.IIDCLINICA = oDoctor.IIDCLINICA;
                        oDoctorItem.IIDESPECIALIDAD = oDoctor.IIDESPECIALIDAD;
                        bd.SubmitChanges();
                        rpta = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = 0;
            }
            return rpta;
        }
    }
}
