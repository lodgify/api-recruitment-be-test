using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.DTO;
using ApiApplication.Services.Facade;
using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiApplication.Services.Filters
{
    public class FilterSchedule
    {

        public Func<IQueryable<ScheduleDTO>, bool> GetFilterSchedule(string title = null, DateTime? date = null)
        {

            if (title != null && date != null)
            {
                Func<IQueryable<ScheduleDTO>, bool> filterFunctionTitleDate = source =>
                {
                    var a = source.Where(p => p.Title == title
                                    & p.StartDate < date
                                    & p.EndDate > date);


                    return a.Any();

                };

                return filterFunctionTitleDate;
            }

            if (date != null)
            {
                Func<IQueryable<ScheduleDTO>, bool> filterFunctionDate = source =>
                {
                    var a = source.Where(p => p.StartDate < date
                                    & p.EndDate > date);


                    return a.Any();

                };
                return filterFunctionDate;
            }

            if (title != null)
            {
                Func<IQueryable<ScheduleDTO>, bool> filterFunctionTitle = source =>
                {
                    var a = source.Where(p => p.Title == title);
                    return a.Any();

                };

                return filterFunctionTitle;
            }



            Func<IQueryable<ScheduleDTO>, bool> filterFunction = source => { return true; };

            return filterFunction;

        }
    }
}
