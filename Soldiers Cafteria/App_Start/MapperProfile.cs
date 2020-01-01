using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Soldiers_Cafteria.Models.DTOs.Order;
using Soldiers_Cafteria.Models.DTOs.OrderProduct;
using Soldiers_Cafteria.Models.DTOs.Payment;
using Soldiers_Cafteria.Models.DTOs.Person;
using Soldiers_Cafteria.Models.DTOs.Product;
using Soldiers_Cafteria.Models.DTOs.Seller;
using Soldiers_Cafteria.Models.DTOs.Category;
using Soldiers_Cafteria.Models.DTOs.Far3;
using Soldiers_Cafteria.Models.DTOs.Daraga;
using Soldiers_Cafteria.Models.InitModels;

namespace Soldiers_Cafteria.App_Start
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            // Person
            Mapper.CreateMap<Person, PersonDTO>();
            Mapper.CreateMap<PersonDTO,Person >();
            Mapper.CreateMap<Person, PersonDetailsDTO>();
            Mapper.CreateMap<PersonDetailsDTO, Person>();
            Mapper.CreateMap<Person, PersonDTO>();   //abdochanges
            Mapper.CreateMap<PersonDTO, Person>();  // abdochanges

            //Product
            Mapper.CreateMap<Product, ProductDTO>();
            Mapper.CreateMap<ProductDTO, Product>();
            Mapper.CreateMap<Product, AdminProductDetailsDTO>();
            Mapper.CreateMap<AdminProductDetailsDTO, Product>();
            Mapper.CreateMap<Product, SellerProductDetailsDTO>();
            Mapper.CreateMap<SellerProductDetailsDTO, Product>();
            Mapper.CreateMap<Product, ProductEditVM>();
            Mapper.CreateMap<ProductEditVM, Product>();

            //Seller
            Mapper.CreateMap<Seller, RegisterSellerDTO>();
            Mapper.CreateMap<RegisterSellerDTO, Seller>();
            Mapper.CreateMap<Seller, SellerLoginDTO>();
            Mapper.CreateMap<SellerLoginDTO, Seller>();
            Mapper.CreateMap<Seller, SellerDetailsDTO>();
            Mapper.CreateMap<SellerDetailsDTO, Seller>();

            //Order
            Mapper.CreateMap<Order, AddOrderDTO>();
            Mapper.CreateMap<AddOrderDTO, Order>();
            Mapper.CreateMap<Order, OrderDetailsDTO>();
            Mapper.CreateMap<OrderDetailsDTO, Order>();
            Mapper.CreateMap<Order, ProductsOfOrderDTO>();
            Mapper.CreateMap<ProductsOfOrderDTO, Order>();

            //Ta2re4a
            //Category
            //OrderProduct
            Mapper.CreateMap<OrderProduct, AddOrderProductDTO>();
            Mapper.CreateMap<AddOrderProductDTO, OrderProduct>();
            //List<OrderProduct>
            //Payment
            Mapper.CreateMap<Payment, PaymentDTO>();
            Mapper.CreateMap<PaymentDTO, Payment>();
            // category
            Mapper.CreateMap<Category, CategoryDTO>();
            Mapper.CreateMap<CategoryDTO, Category>();
            //FAr3
            //Daraga
        }
    }
}