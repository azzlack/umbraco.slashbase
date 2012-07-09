uBase
=================

/Base 2.0 for Umbraco 4.7 and upwards

## How to use ##

1. Go to your Umbraco site and download and install the uBase package.
2. Create a regular class project in Visual Studio.
3. Download and install the NuGet Package "[Umbraco.SlashBase](http://nuget.org/packages/Umbraco.SlashBase)"
4. Add a new class file and derive from SlashBaseController.
5. If you want to add Umbraco security, add the SlashBaseSecurity attribute to the method or class and set one or more of the following properties: 
*(NOTE: Class attribute overrides method attribute)*
* AllowedMembers
* AllowedMemberGroups
* AllowedMemberTypes
* AllowedUsers
* AllowedUserTypes
6. Compile your class and drop it in the bin folder of your Umbraco installation. 
You can read more about how the routing works by reading the following guide: [http://www.asp.net/web-api/overview/web-api-routing-and-actions/routing-in-aspnet-web-api](http://www.asp.net/web-api/overview/web-api-routing-and-actions/routing-in-aspnet-web-api) (Just change "api" with "ubase").
Example (from the Tests.Controllers project): 
<table>
<tr><td>GET: /ubase/Open</td><td>Runs the Get() method in the  OpenController class</td></tr>
<tr><td>GET: /ubase/Open/1</td><td>Runs the Get(string id) method in the  OpenController class</td></tr>
</table>