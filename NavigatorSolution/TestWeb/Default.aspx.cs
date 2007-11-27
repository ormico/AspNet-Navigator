using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZacksFiasco.Web.Navigation;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btHidden1_Click(object sender, EventArgs e)
    {
        Navigator n = new Navigator("HiddenOne");
        n.Parameters["text"] = txParam.Text;
        n.Parameters["time"] = DateTime.Now.ToLongTimeString();

        n.Redirect();
    }
}
