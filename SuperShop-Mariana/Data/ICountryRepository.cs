using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Data
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        IQueryable GetCountryWithCities(); //Método que devolve os paises com as respectivas cidades

        Task<Country> GetCountryWithCitiesAsync(int id); //Método que através do id devolve o objeto cidade

        Task<City> GetCityAsync(int id); //Método que devolva o objeto cidade através do id

        Task AddCityAsync(CityViewModel model); //Vai receber o modelo (CityViewModel) e adiciona-lo (adiciona a cidade)

        Task<int> UpdateCityAsync(City city); //Vai fazer o update e devolve um inteiro que será o id da cidade.

        Task<int> DeleteCityAsync(City city); //Delete.

        IEnumerable<SelectListItem> GetComboCountries(); 

        IEnumerable<SelectListItem> GetComboCities(int countryId);

        Task<Country> GetCountryAsync(City city); //Devolve um country em função de uma city
    }
}
