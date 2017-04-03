**Project Description**
Make ASP.NET Redirects() easier to perform and easier to manage when moving, re-naming, and changing pages.

## See the article [Make programmatic navigation easier in ASP.NET 2.0](http://zacksfiasco.com/post/2007/11/26/Make-programmatic-navigation-easier-in-ASPNET-20.aspx) for a quick article on this project.

How often do you do something like the following:

{{
string url = string.Format("output.aspx?ID={0}&StartDT={1}&QueryType={2}", Server.UrlEncode(id), Server.UrlEncode(startDate), Server.UrlEncode(queryType));
Response.Redirect(url);
}}
This is really cluttered. Take a look at the following:

{{
Navigator n = new Navigator("Output");
n.Parameters["ID"](_ID_) = id;
n.Parameters["StartDT"](_StartDT_) = startDate;
n.Parameters["QueryType"](_QueryType_) = queryType;
n.Redirect();
}}

This is much more clear. The URL Encoding is performed automaticly. In addition, by navigating by name instead of URL you can move resources as needed without breaking other pages.

