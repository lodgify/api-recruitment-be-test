### Feedback

*Please add below any feedback you want to send to the team*


I have doubt regarding following method methos in ShowTimeRepository

public IEnumerable<ShowtimeEntity> GetCollection(Func<IQueryable<ShowtimeEntity>, bool> filter)

The above method parameter Func<IQueryable<ShowtimeEntity>, bool> filter have following observation

I dont find any DBSet.Where version which can take above filter argument. Only parameter which is acceptable for ShowTimeEntities.Where is Expression<Func<ShowtimeEntity, bool>>
So that is why I changed the paramter of GetCollection Function . 

Secondly there is another way to work GetCollection is I could change parameter type to Func<IQueryable<ShowtimeEntity>, IQueryable<ShowtimeEntity>>
so in my get GetCollection I do some thing like this

var newFilter = filter(_context.ShowTimeEntities) 

return newFilter.ASEnumerable();

and this will invoke my client callback function and in client code I can do some thing like this to build my dynamic filter

void GET (criteria)
{
	_repository.GetCollection(filter => 
	{
			if(citeria.ShowTime.HasValue)
			{
				filter.Where(x=>criteria.ShowTime.Value >= x.StartDate && criteria.ShowTime.Value <= x.EndDate); 
			}
			
			if(string.isNullOrEmpty(criteria.MovieTitle))
			{
				filter.Where(x=> x.Movie.MovieTitle == criteria.MovieTitle);
			}
			
			return filter
	});
} 

Please let me know how we can make this filter Func<IQueryable<ShowtimeEntity>, bool> filter work ???





For regarding api testing I have used post man and Iw as trying to fix curl commands on windows but it taking long time to fix some erroes 

I have many other task and assignment to done so I have less time to spend on this

