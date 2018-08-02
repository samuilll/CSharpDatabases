﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace P_03ProductShop.App.ModelsDto
{
   public class ProductDto
    {
        [MinLength(3)]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
