			/*	===	ADDING A NEW THEME	===	*/

-Add new bs css in content
-in App_start -> BundleConfig.cs we define bundles of client-side assets
we can combine & compress frontend files which results in faster loads
-We replace our css bundle from boostrap.css to bootstrap-lumen.css
			
			/*	===	ACTION RESULTS	===	*/

-Actions return ActionResult - it's the base class for all AR in MVC.
-Depending on what action does it will return an instance of one of the derived classes.
-Helper View method is part of base Controller class from which our controller derives
-If we return a view, we could also define our return type of action method as ViewResult
(that's a good practice for unit testing since it saves us some casting)
-In some cases it's possible to have different execution paths in action and return different
action results - in that case we have return type ActionResult

			/*	===	ACTION PARAMETERS	===	*/

-Sources - in URL(/movies/edit/1)
		- in query string(movies/edit?id=1)
		- In form data(id = 1)
-Default name of value we pass (such as id as opposed to movieId) is also defined in our route
configuration (which is named "id")

OPTIONAL PARAMETERS:

public ActionResult Index(int? pageIndex,string sortBy)
{
            
}
-We put questionmark after builtin types to indicate they're "NULLABLE" - not required
-Reference types are nullable by default, so there's no need for ? after string.
-W/ nullable parameter we can then use HasValue method:
		if (!pageIndex.HasValue)

			/*	===	CONVENTION-BASED ROUTES	===	*/

-Imagine custom route where we can access movies by date and month:
/movies/released/2015/04
RouteConfig.cs
-Must be defined from MOST SPECIFIC to MOST GENERIC
Thus: before default:
routes.MapRoute(
	name: "MoviesByReleaseDate",
	url: "movies/released/{year}/{month}"
	default: new { controller="Movies", action="ByReleaseDate"  }
);
-Next we create an Action in Movies controller
(Code snippet mvcaction4 + tab)
ADDING CONSTRAINTS:
-In RouteConfig.cs
We add a new argument - new anonymous method where we can use RegEx

new {year = @"\d{4}", month = @"\d{2}"}
	-@ is because we have escape characters in RegEx
	-\d means digit /  {contstraint} = number of digits
	-We could also specify that only two values are legal:
	eg new{year = @"2015|2016"} (only 2015 or 2016)

            /*	===	ATTRIBUTE ROUTING	===	*/
					(recommended way)
If we have large app - custom routes will become a mess in RouteConfig.cs
+We have to go back&forth between actions and routes
+If we rename a cotnroller we have to manually rename action in route
-We have an ATTRIBUTE 
1st we have to enable them in RouteConfig: routes.MapMvcAttributeRoutes();
	-we delete our convetion route
2nd in controller [Route("movies/released/{year}/{month}")]
3rd APPLY CONSTRAINTS: 
	[Route("movies/released/{year}/{month:regex(\\d{4}}")] 
	-Here we repeat backslash (\) because we cannot use @ for escape character this way
4th APPLY ADDITIONAL CONSTRAINTS:
	{month:regex(\\d{4}):range(1,12)}")
	
	OTHER CONSTRAINTS:
	-min
	-max
	-minlenght
	-max--
	-int
	-float
	-guid

			/*	===	PASSING DATA TO VIEWS	===	*/

Ways to pass:
	-return View(model);
	-ViewData["Movie"] = movie;
		Every contoller has property ViewData of type ViewDataDictionary
		which we can use accordingly (we still have to return an 
		(empty) View!)
		In VIEW - instead of @Model property we have to use @ViewData["Movie"]
		*Beware: each item in Dictionary is of type object!
		*There's a problem w/ magic string ["Movie"] and casting w/ ViewData dict!
	-ViewBag
		ViewBag.Movie = movie;
		Beware - Movie property is "magic" & it's added at runtime
		(so no compile time safety)!!!
		Casting issue as well.
	-The only resonable way is to pass movie object to way as by default.
(With R# - > View F12)
			
			/*	===	VIEW-MODELS	===	*/

So far we can only display say name of the movie.
-What if we also had to disply list of customers who rented the movie?
	In our domain model there may not be a releationship between 
	the customer and the movie classes.
	We need to pass two different models to the view, but we only have 
	1 model in a view.
*ViewModel = model SPECIFICALY BUILT for a VIEW.
[EXAMPLE]
-We add a customer class to our model
	props: Name,Id
-In Models folder we only have domain classes, so we make a new folder
ViewModels.
-Here add new class RandomMovieViewModel
We need 2 props: Movie (Movie) and List<Customer> (Customers)
In controller in Random action we create List<Customer>
	Object initialization syntax to add 2 customers (Customer1 and 2(just name))
-We create VM object
	We initialize Movie property & Customers
	Then we pass a viewModel to View
	In view we change model to RMVM

			/*	===	RAZOR VIEWS	===	*/
<ul>
@foreach(var customer in Model.Customers){
	<li>@customer.Name</li>
}
</ul>
-{} are necessay in for a single line in Razor!
-We could also add a conditional before our ul such as:
	@if(Model.Customers.Count == 0){
	<p>No one has rented this movie before</p>
	}
	else{
		<ul> (from above)
	}
-We can also conditonally ADD CLASSES to TAGS in HTML
-Say if movie was rented at least 5 times it's popular
	@{
		var className = Model.Customers.Count > 5 ? "popular" : null;
	}
	<h2 class="@className">@Models.Movie.Name</h2>
-Comments w/ razor: @* Comment *@ (can be multiple lines)