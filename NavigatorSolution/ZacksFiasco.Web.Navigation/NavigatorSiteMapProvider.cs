using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ZacksFiasco.Web.Navigation
{
    public class NavigatorSiteMapProvider : XmlSiteMapProvider
    {
        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            bool isVisible = true;
            bool rc = false;

            if (node != null && node["visible"] != null && bool.TryParse(node["visible"], out isVisible))
            {
                if (isVisible)
                {
                    rc = base.IsAccessibleToUser(context, node);
                }
                else
                {
                    rc = false;
                }
            }
            else
            {
                rc = base.IsAccessibleToUser(context, node);
            }

            return rc;
        }        
    }
}
