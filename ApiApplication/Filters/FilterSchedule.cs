using System.Linq;
using System;
using ApiApplication.Models.ViewModels;

namespace ApiApplication.Filters
{
    public class FilterSchedule
    {
        public Func<IQueryable<ScheduleViewModel>, bool> GetFilterSchedule(string title = null, DateTime? date = null)
        {

            if (title != null && date != null)
            {
                Func<IQueryable<ScheduleViewModel>, bool> filterFunctionTitleDate = source =>
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
                Func<IQueryable<ScheduleViewModel>, bool> filterFunctionDate = source =>
                {
                    var a = source.Where(p => p.StartDate < date
                                    & p.EndDate > date);


                    return a.Any();

                };
                return filterFunctionDate;
            }

            if (title != null)
            {
                Func<IQueryable<ScheduleViewModel>, bool> filterFunctionTitle = source =>
                {
                    var a = source.Where(p => p.Title == title);
                    return a.Any();

                };

                return filterFunctionTitle;
            }



            Func<IQueryable<ScheduleViewModel>, bool> filterFunction = source => { return true; };

            return filterFunction;

        }
    }
}
