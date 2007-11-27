1. Add the assembly to your bin folder

2. Add these sections to your web.config
<configuration>
	<configSections>
		<section name="ZacksFiasco.Web.Navigation"
				 type="ZacksFiasco.Web.Navigation.Configuration.NavigatorSection, ZacksFiasco.Web.Navigation"/>
	</configSections>
	<ZacksFiasco.Web.Navigation>
		<siteMapProvidersToSearch> 
			<add siteMapProviderName="AspNetXmlSiteMapProvider" />		  
		</siteMapProvidersToSearch>	   
	</ZacksFiasco.Web.Navigation>
    <system.web>
        <compilation debug="true" />
		<siteMap defaultProvider="NavSiteMapProvider" enabled="true">
			<providers>
				<add name="NavSiteMapProvider"
					 description="Custom SiteMap provider."
					 type="ZacksFiasco.Web.Navigation.NavigatorSiteMapProvider, ZacksFiasco.Web.Navigation"
					 siteMapFile="Web.sitemap"
					 securityTrimmingEnabled="true"/>
			</providers>
		</siteMap>
    </system.web>
</configuration>

3. Edit your sitemap file. 
Add every node that you want to be able to navigate to, either from a menu or through a Response.Redirect(), to the sitemap.
If you don't want a node to show up in a menu, then add Visible="false" to that node.

Ex.
<siteMapNode url="~/TimeTracking/EnterProjectTime.aspx" title="Timesheet" navigaorId="EnterProjectTime" Visible="false"/>

4. Use the Navigator in your code.

using ZacksFiasco.Web.Navigator;
...
Navigator n = new Navigator("<resourceKey>");

n.Parameters["<QueryString Item>"] = "<value>";
n.Parameters["<QueryString Item>"] = "<value>";
n.Parameters["<QueryString Item>"] = "<value>";

// items are automaticly URL Encoded and the user is transfered to the page specified by Resource Key
n.Redirect();

or 

hlHyperlink.NavigationUrl = n.ToString();