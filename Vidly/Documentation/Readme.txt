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