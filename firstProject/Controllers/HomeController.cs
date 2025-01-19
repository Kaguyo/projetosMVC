using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using firstProject.Models;
using firstProject.Data;

namespace firstProject.Controllers;

public class HomeController : Controller
{
    private readonly UserRepository _userRepository;
    private readonly ILogger<HomeController> _logger;
    private readonly IDbConnectionFactory _dbConnection;
    public HomeController(ILogger<HomeController> logger, UserRepository userRepository, IDbConnectionFactory dbConnection)
    {   
        _logger = logger;
        _dbConnection = dbConnection;
        _userRepository = userRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(User user)
    {   
        // Validacao de dados...

        user.HashSenha = BCrypt.Net.BCrypt.HashPassword(user.HashSenha);
        await _userRepository.AddAsync(user);

        return View(user);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
