using System;
using System.Collections.Generic;
using System.Reflection;

namespace App.Docker.Api.ViewModels.Users
{
    public class UserViewModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public DateTime? InitialRegistrationDate { get; set; }
        public DateTime? FinalRegistrationDate { get; set; }
        public string Email { get; set; }
        
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
