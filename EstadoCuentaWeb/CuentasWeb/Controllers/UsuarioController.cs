using CuentasWeb.Dtos;
using CuentasWeb.Models;
using CuentasWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuentasWeb.Controllers
{
    // [Autorice]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service)
        {
                _service = service;
        }

        public ActionResult Usuario()
        {
            return View("Usuario");
        }

        [HttpPost]
        public async Task<ActionResult> Login(UsuarioLogin model)
        {
            try
            {



            }
            catch (Exception e)
            {
                return View("Login");
            }
        }

        [HttpPost]
        public ActionResult Post(FormCollection collection, UsuarioAddDto model)
        {
            try
            {
                return RedirectToAction("Usuario");
            }
            catch (Exception e)
            {
                return View("Usuario");
            }
        }

        [HttpPut]
        public ActionResult Put(FormCollection collection, UsuarioAddDto model)
        {
            try
            {
                return RedirectToAction("Usuario");
            }
            catch (Exception e)
            {
                return View("Usuario");
            }
        }

        [HttpDelete]
        public ActionResult Delete(FormCollection collection, UsuarioAddDto model)
        {
            try
            {
                return RedirectToAction("Usuario");
            }
            catch (Exception e)
            {
                return View("Usuario");
            }
        }
    }
}