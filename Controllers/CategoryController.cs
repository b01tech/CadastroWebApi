using CadastroWebApi.Context;
using CadastroWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> Get()
    {
        var categories = _context.Categories.ToList();
        if (categories is null)
        {
            return NotFound();
        }
        return categories;
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public ActionResult<Category> Get([FromRoute] int id)
    {
        var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);

        if (category == null)
        {
            return NotFound("Category not found.");
        }

        return category;
    }

    [HttpPost]
    public ActionResult Post([FromBody] Category category)
    {
        if (category is null)
        {
            return BadRequest();
        }
        _context.Categories.Add(category);
        _context.SaveChanges();
        return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, category);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put([FromRoute] int id, [FromBody] Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }

        _context.Entry(category).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(category);

    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete([FromRoute] int id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

        if (category == null)
            return NotFound("Category not found");

        _context.Remove(category);
        _context.SaveChanges();
        return Ok(category);
    }

}
