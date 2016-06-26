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

            