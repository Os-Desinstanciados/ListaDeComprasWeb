using Microsoft.AspNetCore.Mvc;

namespace ListaDeCompras.WebApp.Compartilhado.Apresentacao;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
}