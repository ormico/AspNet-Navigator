using System;
using System.Web;
using System.Collections.Generic;
using System.Text;


namespace ZacksFiasco.Web.Navigation
{
    public class Navigator
    {
        public Navigator()
        {
            parameters = new Dictionary<string, string>(3);
        }

        public Navigator(string PageKey)
            : this()
        {
            pageKey = PageKey;
        }

        static string GetUrlFromKey(string resourceKey)
        {
            SiteMapNode rcNode = null;
            string rc = string.Empty;

            foreach (SiteMapNode node in SiteMap.Providers["AspNetXmlSiteMapProvider"].RootNode.GetAllNodes())
            {
                if (node.IsAccessibleToUser(HttpContext.Current) && node.ResourceKey == resourceKey)
                {
                    rcNode = node;
                    break;
                }
            }

            if (rcNode != null)
            {
                rc = rcNode.Url;
            }

            return rc;
        }

        string pageKey;

        public string PageKey
        {
            get { return pageKey; }
            set { pageKey = value; }
        }

        Dictionary<string, string> parameters;

        public Dictionary<string, string> Parameters
        {
            get { return parameters; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Navigator.GetUrlFromKey(pageKey));
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