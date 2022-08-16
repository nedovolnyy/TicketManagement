using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TicketManagement.EventManagerAPI.Contracts
{
    /// <summary>
    /// Helper methods for collection of the city object.
    /// </summary>
    public static class CityExtensions
    {
        /// <summary>
        /// Find city by it's name.
        /// </summary>
        /// <param name="cities">collection of city objects.</param>
        /// <param name="name">name of the city required.</param>
        /// <returns>.</returns>
        public static City GetCityByName(this IEnumerable<City> cities, string name) => cities.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))!;
    }

    /// <summary>
    /// City entity.
    /// </summary>
    public class City
    {
        /// <summary>
        /// All main cities of Belarus.
        /// </summary>
        public static List<City> BelarusRegionalCities => new ()
        {
            new City
            {
                Name = "Gomel",
                NumberOfPeople = 536938,
            },
            new City
            {
                Name = "Brest",
                NumberOfPeople = 340318,
            },
            new City
            {
                Name = "Grodna",
                NumberOfPeople = 361352,
            },
            new City
            {
                Name = "Vitebsk",
                NumberOfPeople = 364800,
            },
            new City
            {
                Name = "Mogilev",
                NumberOfPeople = 357100,
            },
            new City
            {
                Name = "Minsk",
                NumberOfPeople = 2020600,
            },
        };

        /// <summary>
        /// Name of the city.
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Number of people in the city.
        /// </summary>
        public long NumberOfPeople { get; set; }
    }
}
