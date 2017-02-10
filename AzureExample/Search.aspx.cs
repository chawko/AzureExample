using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzureExample
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            txtResults.Text = string.Empty;
            var term = searchTermField.Text;

            var key = "34FF26DE3EDB6D194C5F93A87AF526A2";
            var serviceName = "dev-bakingmad";



            SearchServiceClient serviceClient = new SearchServiceClient(serviceName, new SearchCredentials(key));
            ISearchIndexClient indexClient = serviceClient.Indexes.GetClient("example");

            var param = new Microsoft.Azure.Search.Models.SearchParameters();
            var Facets = new List<String>();
            Facets.Add("Difficulty");
            Facets.Add("Celebration");
            Facets.Add("PrimaryIngredient");
            Facets.Add("Everyday");
            Facets.Add("DietLifestyle");
            Facets.Add("SweetSavory");
            Facets.Add("Category");

            param.Facets = Facets;
            param.Filter = txtFilter.Text;
            param.IncludeTotalResultCount = true;
            
            var cols = new List<String>();
            cols.Add("id");
            cols.Add("Title");
            cols.Add("Difficulty");
            cols.Add("TotalTime");
            cols.Add("Date");
            cols.Add("Rating");
            cols.Add("Content");
            cols.Add("SearchType");
            cols.Add("Serves");
            cols.Add("DietaryNeeds");
   

            param.Select = cols;

            Microsoft.Azure.Search.Models.DocumentSearchResult<Res> results = indexClient.Documents.Search<Res>(term, param);

            txtResults.Text += "Results: " + results.Count.ToString() + "<br/>";
            txtResults.Text += "<br/>";

            foreach (var item in results.Results)
            {
                txtResults.Text += string.Format("Id:{0} <br/> Title:{1} <br/> Score:{2} <br/> TotalTime:{3}<br/>Difficulty:{4}<br/>Rating:{5}<br/>Date:{6}<br/><br/>", item.Document.id, item.Document.Title, item.Score, item.Document.TotalTime, item.Document.Difficulty, item.Document.Rating, item.Document.Date);
            }

            txtResults.Text += "<br/>";
            txtResults.Text += "Facets:" + "<br/>";
            txtResults.Text += "<br/>";
            foreach (var item in results.Facets)
            {
                
                txtResults.Text += string.Format("- {0}{1}", item.Key, "<br/>");
                foreach (var subitem in item.Value)
                {
                    txtResults.Text += string.Format("---- {0} ({1}) {2}", subitem.Value, subitem.Count, "<br/>");
                }
                txtResults.Text += "<br/>";
            }

        }

        public class Res
        {
            public string id { get; set; }
            public string Title { get; set; }
            public string TotalTime { get; set; }
            public string Difficulty { get; set; }
            public Nullable<double> Rating { get; set; }
            public string Category{ get; set; }
            public Nullable<DateTime> Date { get; set; }
        }
    }
}