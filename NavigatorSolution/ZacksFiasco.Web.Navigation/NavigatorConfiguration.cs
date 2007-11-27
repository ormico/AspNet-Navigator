using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ZacksFiasco.Web.Navigation.Configuration
{
    public class NavigatorSection : ConfigurationSection
    {
        [ConfigurationProperty("siteMapProvidersToSearch")]
        public ProviderCollection SiteMapProvidersToSearch
        {
            get { return (ProviderCollection)this["siteMapProvidersToSearch"] ?? new ProviderCollection(); }
        }
    }

    public class ProviderCollection : System.Configuration.ConfigurationElementCollection
    {
        public ProviderElement this[int index]
        {
            get{ return base.BaseGet(index) as ProviderElement; }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }

                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ProviderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProviderElement)element).SiteMapProviderName;
        }
    }

    public class ProviderElement : ConfigurationElement
    {
        [ConfigurationProperty("siteMapProviderName", IsRequired=true)]
        public string SiteMapProviderName
        {
            get { return this["siteMapProviderName"] as string; }
//            set { siteMapProviderName = value; }
        }
    }
}
