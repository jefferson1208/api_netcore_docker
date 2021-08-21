using System;
using System.Collections.Generic;
using System.Reflection;

namespace App.Docker.Api.ViewModels.Products
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Dictionary<string, string> CreateFilters()
        {
            var filters = new Dictionary<string, string>();

            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                var name = propertyInfo.Name;
                var value = Convert.ToString(propertyInfo.GetValue(this));

                if (!string.IsNullOrEmpty(value))
                {
                    filters.Add(name, value);
                }
            }

            return filters;
        }
    }
}
