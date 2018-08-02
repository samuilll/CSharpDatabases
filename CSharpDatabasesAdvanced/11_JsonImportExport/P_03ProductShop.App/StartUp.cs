using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P_03ProductShop.App.ModelsDto;
using P_03ProductShop.Data;
using P_03ProductShop.Models;
using System;
using System.Collections.Generic;
namespace P_03ProductShop.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var manager = new ShopManager();

            manager.ResetDatabase();

            manager.ImportData();

            manager.ProductInRangeExport();

            manager.SoldProductsExport();

            manager.CategoryByProductExport();

            manager.UsersAndProductsExport();
        }


    }
}
