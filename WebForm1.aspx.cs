using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CalculatorWebApplication
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            CalculatorService.CalculatorWebServiceSoapClient client =
                new CalculatorService.CalculatorWebServiceSoapClient();
            int result = client.Add(Convert.ToInt32(txtFirstNumber.Text), Convert.ToInt32(txtSecondNumber.Text));
            lblResult.Text = result.ToString();

            // You won't gave the GetCalculation method intially, you have to update the service reference since we added another method for the proxy class to recognize in CalculatorWebService.asmx.cs
            // This mehod is going to return an array of strings 
            client.GetCalculations();

            // Sets that datasource into the gridview control 
            gvCalculations.DataSource = client.GetCalculations();
            gvCalculations.DataBind();

            // This changes the box header's name from deault "Items" to "Recent Calculations"
            gvCalculations.HeaderRow.Cells[0].Text = "Recent Calculations";

            // The list isn't returning calculations, instead You have not performed any calculations
            // Currently the web app is not sending the same session ID as the web service 
            // To make it work, in the Web.config file (this is automatically given to us when we create a service sreference to CalculatorWebService), allowCookies must be set to "true" (defaults as "false" or isn't present)

        }
    }
}


