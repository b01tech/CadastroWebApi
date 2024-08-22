using CadastroWebApi.Context;
using CadastroWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var products = _context.Products.ToList();
        if (products == null)
        {
            return NotFound();
        }

        return products;
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public ActionResult<Product> Get(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public ActionResult Post([FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest();
        }
        _context.Products.Add(product);
        _context.SaveChanges();
        return new CreatedAtRouteResult("GetProduct", new { id = product.ProductId }, product);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put([FromRoute] int id, [FromBody] Product product)
    {
        if (id != product.ProductId)
        {
            return BadRequest();
        }
        _context.Update(product);
        _context.SaveChanges();
        return Ok(product);
    }

    [HttpDelete]
    public ActionResult Delete([FromRoute] int id)
    {
        var product = _context.Products.FirstOrDefault(_ => _.ProductId == id);

        if (product is null)
        {
            return NotFound("Product Not found.");
        }
        _context.Products.Remove(product);
        _context.SaveChanges();
        return Ok(product);
    }




}
