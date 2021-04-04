using Ecomerce.Models;
using System;
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

        public void Dispose()
        {
            Dispose();
        }
    }
}