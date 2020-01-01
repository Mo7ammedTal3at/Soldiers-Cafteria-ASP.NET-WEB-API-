using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using AutoMapper;
using Soldiers_Cafteria.Models;
using Soldiers_Cafteria.Models.InitModels;
using Soldiers_Cafteria.Models.DTOs.Product;
using Soldiers_Cafteria.Models.DTOs.Category;
//using System.Web.Mvc;

namespace Soldiers_Cafteria.Controllers.Demo
{
    public class ProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Products/AdminView
        [Route("api/Products/AdminView")]
        [HttpGet]
        [ResponseType(typeof(List<AdminProductDetailsDTO>))]
        public IHttpActionResult GetProductsForAdmin()
        {
            var products = db.Products.OrderByDescending(g => g.AddtionTime).ToList();                                                //Get All Goods From DataBase , in List
            var productsDTO = new List<AdminProductDetailsDTO>();                              //Create New List from GoodsDTO
            foreach (var temp in products)
            {
                var productsVm = Mapper.Map<AdminProductDetailsDTO>(temp);
                productsVm.AddtionDate = temp.AddtionTime.ToShortDateString();
                productsVm.NumberOfBoxes = (int)(temp.TotalItemsCount / temp.NumberOfItemsInBox);
                productsVm.NumberOfRestItems = (temp.TotalItemsCount % temp.NumberOfItemsInBox);
                productsDTO.Add(productsVm);
            }
            return Ok(productsDTO);
        }
        //Get : api/products/SellerView
        [Route("api/Products/SellerView")]
        [HttpGet]
        [ResponseType(typeof(List<SellerProductDetailsDTO>))]
        public IHttpActionResult GetProductsForSeller()
        {
            var products = db.Products.ToList();
            var productsDTO = new List<SellerProductDetailsDTO>();
            foreach (var temp in products)
            {
                var cat = db.Categories.SingleOrDefault(c => c.Id == temp.CategoryId);
                var productVM = Mapper.Map<SellerProductDetailsDTO>(temp);
                productVM.CategoryName = cat.Name;
                productsDTO.Add(productVM);
            }
            return Ok(productsDTO);
        }

        // GET: api/Products/AdminView/5
        [Route ("api/Products/AdminView/{id}")]
        [HttpGet]
        [ResponseType(typeof(AdminProductDetailsDTO))]
        public IHttpActionResult GetProductForAdmin(int id)
        {

            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDTO = Mapper.Map<AdminProductDetailsDTO>(product);
            return Ok(productDTO);
        }
        // GET: api/Products/SellerView/5
        [Route("api/Products/SellerView/{id}")]
        [HttpGet]
        [ResponseType(typeof(SellerProductDetailsDTO))]
        public IHttpActionResult GetProductForSeller(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDTO = Mapper.Map<SellerProductDetailsDTO>(product);
            return Ok(productDTO);
        }
        [HttpGet]
        [ResponseType(typeof(ProductEditVM))]
        public IHttpActionResult GetEditPage(int id)
        {
            var productInDb = db.Products.FirstOrDefault(g => g.Id == id);
            if (productInDb == null)
            {
                return BadRequest("هذا المنتج غير موجود");
            }
            var productEVm = Mapper.Map<ProductEditVM>(productInDb);
            return Ok(productEVm);
        }
        //not completed
        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct( [FromUri]int id,[FromBody]ProductEditVM productVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetErrorResponseMessage(ModelState));
            }
            var productInDb = db.Products.FirstOrDefault(p => p.Id == id);
            if (productInDb == null)
            {
                return BadRequest("هذا المنتج غير موجود");
            }
            Mapper.Map(productVM, productInDb);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return BadRequest("هذا الاسم موجود من قبل");
            }
            return Ok("تم تعديل المنتج بنجاح");
            




        }
        // GET: api/Products/AddProductPage
        [Route("api/Products/AddProductPage")]
        [HttpGet]
        [ResponseType(typeof(List<CategoryDTO>))]
        public IHttpActionResult AddProductPage()
        {
            var categories = db.Categories.OrderBy(c => c.Name).ToList().Select(Mapper.Map<Category, CategoryDTO>);
            return Ok(categories);
        }
        // POST: api/Products
        [ResponseType(typeof(string))]
        public IHttpActionResult PostProduct(ProductDTO productDTO)
        {
            if(productDTO == null)
            {
                return BadRequest("لابد من ادخال بيانات المنتج");
            }
            productDTO.Name = productDTO.Name.Trim();
            Product product = Mapper.Map<Product>(productDTO);
            product.TotalItemsCount = productDTO.NumberOfBoxes * productDTO.NumberOfItemsInBox;
            product.AddtionTime = DateTime.Now;
            if (productDTO.NumberOfBoxes <= productDTO.AlertLimit)
            {
                product.IsLimited = true;
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(GetErrorResponseMessage(ModelState));
            }
            db.Products.Add(product);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return BadRequest("هذا المنتج موجود بالفعل");
            }
            
            return CreatedAtRoute("DefaultApi", new { id = product.Id }, "تم إضافة \""+product.Name+"\" بنجاح");
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return Ok("تم حذف المنتج بنجاح");
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
        private string GetErrorResponseMessage(System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            string responseMessage = "";
            foreach (var temp in modelState)
            {
                foreach (var error in temp.Value.Errors)
                {
                    responseMessage += "* " + error.ErrorMessage + "\n";
                }
            }
            return responseMessage;
        }

       
        
    }
}