using Ecomerce.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecomerce.Clases
{
    public class CombosHelper : IDisposable
    {
        private static EcomerceContext db = new EcomerceContext();

        public static List<Deparment> GetDeparments() {
            var departments = db.Deparments.ToList();
            departments.Add(new Deparment
            {
                DepartmentId = 0,
                Name = "[Select a Department...]",
            });

            return departments.OrderBy(b => b.Name).ToList();

        }

        public static List<City> GetCities()
        {
            var cities = db.Cities.ToList();
            cities.Add(new City
            {
                CityId = 0,
                Name = "[Select a City...]",
            });

            return cities.OrderBy(b => b.Name).ToList();

        }

        public static List<Company> GetCompanies()
        {
            var companies = db.Companies.ToList();
            companies.Add(new Company
            {
                CompanyId = 0,
                Name = "[Select a Company...]",
            });

            return companies.OrderBy(b => b.Name).ToList();

        }

        public void Dispose()
        {
            Dispose();
        }

        public static List<Category> GetCategories(int companyId)
        {
            var categories = db.Categories.Where(c => c.CompanyId == companyId).ToList();
            categories.Add(new Category
            {
                CategoryId = 0,
                Description = "[Select a category...]",
            });

            return categories.OrderBy(b => b.Description).ToList();
        }

        public static List<Tax> GetTaxes(int companyId)
        {
            var taxes = db.Taxes.Where(c => c.CompanyId == companyId).ToList();
            taxes.Add(new Tax
            {
                TaxId = 0,
                Description = "[Select a tax...]",
            });

            return taxes.OrderBy(b => b.Description).ToList();
        }
    }
}