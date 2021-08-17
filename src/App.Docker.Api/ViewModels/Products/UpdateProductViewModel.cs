using System;

namespace App.Docker.Api.ViewModels.Products
{
    public class UpdateProductViewModel
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
