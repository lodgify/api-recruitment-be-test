using ApiApplication.DTOs.API;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    /// <summary>
    /// Specifies the contract for a service that implements operations to manage Showtime entities in the application.
    /// </summary>
    public interface IShowtimeService
    {
        /// <summary>
        /// Returns all the Showtime entities in the application.
        /// </summary>
        /// <param name="date">Filter Showtime entities for a date between their StartDate and EndDate.</param>
        /// <param name="movieTitle">Filter Showtime entities for Movies which the specified title.</param>
        /// <returns></returns>
        Task<IEnumerable<ShowtimeEntity>> GetAsync(DateTime date, string movieTitle);
        
        /// <summary>
        /// Returns the Showtime entity with the specified id or null if it is not found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ShowtimeEntity> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new Showtime entity to the application from a provided Showtime DTO object.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ShowtimeEntity> CreateAsync(Showtime entity);
        
        /// <summary>
        /// Updates an existing Showtime entity in the application from a provided Showtime DTO object.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ShowtimeEntity> UpdateAsync(Showtime entity);
        
        /// <summary>
        /// Removes an existing Showtime entity from the application with the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
