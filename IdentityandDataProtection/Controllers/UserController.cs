using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IDataProtector _protector;

    public UserController(AppDbContext context, IDataProtectionProvider provider)
    {
        _context = context;
        _protector = provider.CreateProtector("PasswordProtector");
    }

    // GET: api/user
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }

    // POST: api/user
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Encrypt the password
        user.Password = _protector.Protect(user.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }

    // GET: api/user/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Şifreyi burada şifrelemeniz gerekebilir
        user.Password = HashPassword(user.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    private string HashPassword(string password)
    {
        // Şifreleme işlemi için bir method ekleyin. 
        // Örneğin, ASP.NET Core Identity kullanıyorsanız, PasswordHasher kullanabilirsiniz.
        return password;
    }
}
