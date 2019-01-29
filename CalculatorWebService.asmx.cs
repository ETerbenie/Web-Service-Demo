using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServicesDemo
{
    /// <summary>
    /// Summary description for CalculatorWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CalculatorWebService : System.Web.Services.WebService // CalculatorWebService aready inheriting after :
    {
        // What do we want to achieve?
        // We want all the recent calculations the user has performed using a session object 
        
         /* [WebMethod] attritbutes:
         @@ Description - Use to specify a description for the web service mthod
         @@ BufferResponse - This is a boolean property. Default is true. When this property is true, the repsonse fo the XML web service method
                             is not returned to the client until either the response is completely serialized or the buffer is full.
                             (when set to sales, the response of the XML web service method is returned to the client as it is being serialized
                             in general set it to false when the XML web service method returns large amounts of data. For smaller amounts of data
                              web service performance is better when it is set to true)
         @@ CacheDuration - Use this property, if you want to cache the results of a web service method. This is an integer property, and specifies
                            the number of seconds that the response should be cached. A response is cached for each unique parameter.
         */
         // When updating the Description and CacheDuration, make sure to build the web service again (WebServiceDemo) and update the service reference (CalculatorService)
         // When that number (20 sec in this example) passes, then the result will be the same, after 20 sec the same result will populate again
        [WebMethod(EnableSession = true, Description ="This method adds 2 numbers", CacheDuration = 20)] 
        public int Add(int firstNumber, int secondNumber)
        {
            List<string> calculations;

            // We have access to the session object because of the inheritance, CalculatorWebService : System.Web.Services.WebService 
            // We are using "CALCULATIONS" session key to store all calculations this method is going to receive 
            if (Session["CALCULATIONS"] == null)
            {
                // if the user hasn't done any calculations yet, we create a new list of strings, maybe when no session is already created
                calculations = new List<string>();
            }
            else
            {
                // if the user has already performed calculations, ad created a session, we store more into that session into this variable
                calculations = (List<string>)Session["CALCULATIONS"];
            }

            // The output
            // This is the format of the list results being presented and diplayed to the user 
            string strRecentCalculation = firstNumber.ToString() + " + " + secondNumber.ToString() + " = " + (firstNumber + secondNumber).ToString();

            // strRecentCalculation is going to contain the output we expect
            // now we add strRecentCalculation to out calculations string array 
            calculations.Add(strRecentCalculation); // adding the calculations tot he results

            // stores the calculations object inside of the session variable 
            Session["CALCULATIONS"] = calculations;

            return firstNumber + secondNumber;
        }
        // Basic implementation code, we are checking if there is anythignin the sessions variable, if null we just create one else we just retrieve the other variables, everytime the method is called
        // we build the recent calculations (string strRecentCalculation does this for us) and then storing that calculation within (calculations = new List<string>(); done by calculations = (List<string>)Session["CALCULATIONS"];) and then storing it back into 
        // the session variable (Session["CALCULATIONS"] = calculations;)

        [WebMethod(EnableSession = true)] //This method is going to check the session variable 
        // This method is goign to return all the recent calculations the user has performed and they should be reutnred as a list of strings
        public List<string> GetCalculations()
        {
            // This will check the session variable, user has not done any calculations and will create a new list of strings
            if (Session["CALCULATIONS"] == null)
            {
                // Creating new list
                List<string> calculations = new List<string>();
                calculations.Add("You have not performed any calculations"); // Telling user no calculations have been made prior 
                return calculations;
            }
            else
            {
                // This means some calculations were already created, this returns those calculations in a list
                return (List<string>)Session["CALCULATIONS"];

                // Web service is working, now we have to get GetCalculations method into our web app client --> adding to WebForms1.aspx
                // we added <asp:GridView ID="gvCalculations" runat="server"></asp:GridView> into WebForm1.aspx
                // Then we invoke the GetCalculations method in the proxy class within WebForm1.aspx.cs
            }
        }
    }
}


