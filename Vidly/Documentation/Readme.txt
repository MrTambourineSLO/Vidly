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
