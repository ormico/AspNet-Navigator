using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using ZacksFiasco.Web.Navigation.Configuration;

namespace ZacksFiasco.Web.Navigation
{
    public class Navigator
    {
        public Navigator()
        {
            parameters = new Dictionary<string, string>(3);
        }

        public Navigator(string NavigatorId)
            : this()
        {
            navigatorId = NavigatorId;
        }

        static string GetUrlFromKey(string navigatorId)
        {
            SiteMapNode rcNode = null;
            string rc = string.Empty;

            NavigatorSection ns = ConfigurationManager.GetSection("ZacksFiasco.Web.Navigation") as NavigatorSection;

            if (ns != null)
            {
                foreach (ProviderElement pe in ns.SiteMapProvidersToSearch)
                {
                    //foreach (SiteMapNode node in SiteMap.Providers["AspNetXmlSiteMapProvider"].RootNode.GetAllNodes())
                    foreach (SiteMapNode node in SiteMap.Providers[pe.SiteMapProviderName].RootNode.GetAllNodes())
                    {
                        if (node.IsAccessibleToUser(HttpContext.Current) && node["navigatorId"] == navigatorId)
                        {
                            rcNode = node;
                            break;
                        }
                    }

                    if (rcNode != null)
                    {
                        break;
                    }
                }
            }
            else
            {
                foreach (SiteMapNode node in SiteMap.Providers["AspNetXmlSiteMapProvider"].RootNode.GetAllNodes())
                {
                    if (node.IsAccessibleToUser(HttpContext.Current) && node["navigatorId"] == navigatorId)
                    {
                        rcNode = node;
                        break;
                    }
                }
            }

            if (rcNode != null)
            {
                rc = rcNode.Url;
            }

            return rc;
        }

        string navigatorId;

        public string NavigatorId
        {
            get { return navigatorId; }
            set { navigatorId = value; }
        }

        Dictionary<string, string> parameters;

        public Dictionary<string, string> Parameters
        {
            get { return parameters; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Navigator.GetUrlFromKey(navigatorId));
            HttpServerUtility s = HttpContext.Current.Server;

            if (parameters.Count > 0)
            {
                sb.Append("?");//todo: url might already have a 1st item. don't assume
                bool isFirst = true;
                foreach (string key in parameters.Keys)
                {
                    if (isFirst == false)
                    {
                        sb.Append("&");
                    }

                    isFirst = false;
                    sb.AppendFormat("{0}={1}", s.UrlEncode(key), s.UrlEncode(parameters[key]));
                }
            }

            return sb.ToString();
        }

        public void Transfer()
        {
            HttpContext.Current.Server.Transfer(ToString());
        }

        public void Redirect()
        {
            HttpContext.Current.Response.Redirect(ToString());
        }
    }
    
}