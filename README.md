
#Fork documentation
The MvcSiteMapprovider provides the ability to annotate the controller actions with attributes and we can define these controllers in multiple modules
However, i would have liked to provide the same file based Mvc.Sitemap feature for multiple assemblies, because in many large enterprise applications controllers and even domain models 
may need to be distributed among different assemblies. Having the ability to have each of these module different Mvc.Sitemap file without adorning the controllers gives us some flexibility.
Then we can merge the whole sitemap tree in the main web project file to build a single sitemap.

We need to define the other Assembly sitemap file as embedded resource to parse it through reflection.
A demo application is provided with the source to demonstrate the feature.
Main web project is DemoApp.Web
A demo module is StoreModule project each contains its Mvc.Sitemap file.

In the main web sitemap file a line 
<!--Sitemap from Store Module-->
<mvcSiteMapNode title="Books" moduleName="StoreModule"/>

embeds the sitemap from StoreModule assembly.

The Demo application uses a framework [Zephyr](https://github.com/marufbd/DefaultDDDArch) ORM data access repository etc.
DomainModels is made as a separate common project which every module refers

#Original Source Documentation

# MvcSiteMapProvider
An ASP.NET MVC SiteMapProvider implementation for the ASP.NET MVC framework.

## What can it be used for?
MvcSiteMapProvider is, as the name implies, an ASP.NET MVC SiteMapProvider implementation for the ASP.NET MVC framework. Targeted at ASP.NET MVC 2, it provides sitemap XML functionality and interoperability with the classic ASP.NET sitemap controls, like the SiteMapPath control for rendering breadcrumbs and the Menu control.

Based on areas, controller and action method names rather than hardcoded URL references, sitemap nodes are completely dynamic based on the routing engine used in an application. The dynamic character of ASP.NET MVC is followed in the MvcSiteMapProvider: there are numerous extensibility points that allow you to extend the basic functionality offered.

## Documentation
See the [documentation](https://github.com/maartenba/MvcSiteMapProvider/wiki)

## Get it on NuGet!

    Install-Package MvcSiteMapProvider
	
## License
[MS-PL License](https://github.com/maartenba/MvcSiteMapProvider/blob/master/LICENSE.md)

## Building the source
After cloning the repository, run build.cmd.

MvcSiteMapProvider used the psake build engine to build the project. Psake is a Powershell based engine and if it is the first time you execute powershell scripts on your system you may need to allow script execution by running the following command as adminstrator:

    Set-ExecutionPolicy unrestricted

## Acknowledgements
The downloads page features an example application. The example code is all based on the excellent [ASP.NET MVC Music Store sample application](http://www.asp.net/mvc/videos/mvc-2/music-store/mvc-music-store-part-1-intro,-tools,-and-project-structure) by [Jon Galloway](http://weblogs.asp.net/jgalloway/).