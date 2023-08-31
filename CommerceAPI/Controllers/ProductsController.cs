﻿using CommerceAPI.DataAccess;
using CommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommerceAPI.Controllers
{
    [Route("/api/merchants/{merchantId:int}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CommerceApiContext _context;

        public ProductsController(CommerceApiContext context)
        {
            _context = context;
        }

        //Retrieve a product by its primary key
        [HttpGet("{productId}")]
        public ActionResult<Product> GetProduct(int merchantId, int productId)
        {
            return _context.Products.Where(p => p.ProductId == productId && p.MerchantId == merchantId).First();
        }
        //Create a new product associated with a specific merchant
        [HttpPost]
        public ActionResult CreateProduct(int merchantId, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            product.MerchantId = merchantId;
            _context.Products.Add(product);
            _context.SaveChanges();
            Response.StatusCode = 201;

            return new JsonResult(product);
        }
        //Update an existing product

        //Delete a product by its primary key
    }
}
