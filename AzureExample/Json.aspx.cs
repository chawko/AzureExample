using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace AzureExample
{
    public partial class Json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string path = HttpContext.Current.ApplicationInstance.Server.MapPath("/post.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(rss));

            StreamReader reader = new StreamReader(path);

            var rss = (rss)serializer.Deserialize(reader);
            reader.Close();

            var recipes = new Rootobject();
            recipes.value = new List<Value>();
            foreach (var item in rss.channel.item)
            {
                var recipe = new Value();
                recipe.id = item.post_id.ToString();
                recipe.Title = item.title;
                if (item.postmeta == null || item.category == null || item.post_date == null)
                {
                    continue;
                }
                var difficulty = item.postmeta.Where(pm => pm.meta_key == "recipe_skill_level").FirstOrDefault();
                recipe.Difficulty = difficulty != null ? difficulty.meta_value : "";
                recipe.Category = item.category.Where(c => c.domain == "category").FirstOrDefault().Value;
                recipe.Date = (DateTime.Parse(item.post_date)).ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                recipe.Keywords = new string[1] { "" };
                recipe.Celebration = new string[1] { "" };
                recipe.Everyday = new string[1] { "" };
                recipe.DietLifestyle = new string[1] { "" };
                
                var rating = item.postmeta.Where(pm => pm.meta_key == "recipe_average_rating").FirstOrDefault();
                recipe.Rating = rating != null ? float.Parse(rating.meta_value) : 0;
                recipe.SearchType = "Recipe";
                var totalTime = item.postmeta.Where(pm => pm.meta_key == "recipe_prep_time").FirstOrDefault();
                recipe.TotalTime = totalTime != null ? totalTime.meta_value : "";
                var serves = item.postmeta.Where(pm => pm.meta_key == "recipe_yield").FirstOrDefault();
                recipe.Serves = serves != null ? serves.meta_value : "";
                recipes.value.Add(recipe);
            }
            var json = new JavaScriptSerializer().Serialize(recipes);
            litTxt.Text = json;
        }
    }

    public class Rootobject
    {
        public List<Value> value { get; set; }
    }

    public class Value
    {
        [DataMember(Name = "@search.action")]
        public string searchaction { get { return "upload"; } }
        public string id { get; set; }
        public string Title { get; set; }
        public string Difficulty { get; set; }
        public string PrimaryIngredient { get; set; }
        public string[] Keywords { get; set; }
        public string Category { get; set; }
        public string[] Celebration { get; set; }
        public string[] Everyday { get; set; }
        public string[] DietLifestyle { get; set; }
        public string SweetSavory { get; set; }
        public string Date { get; set; }
        public float Rating { get; set; }
        public string SearchType { get; set; }
        public string TotalTime { get; set; }
        public string Serves { get; set; }
        public string DietaryNeeds { get; set; }
        public string Image { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class rss
    {

        private rssChannel channelField;

        /// <remarks/>
        public rssChannel channel
        {
            get
            {
                return this.channelField;
            }
            set
            {
                this.channelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rssChannel
    {

        private string titleField;

        private string linkField;

        private object descriptionField;

        private string pubDateField;

        private string languageField;

        private decimal wxr_versionField;

        private string base_site_urlField;

        private string base_blog_urlField;

        private rssChannelAuthor[] authorField;

        private string generatorField;

        private rssChannelItem[] itemField;

        /// <remarks/>
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string link
        {
            get
            {
                return this.linkField;
            }
            set
            {
                this.linkField = value;
            }
        }

        /// <remarks/>
        public object description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string pubDate
        {
            get
            {
                return this.pubDateField;
            }
            set
            {
                this.pubDateField = value;
            }
        }

        /// <remarks/>
        public string language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }

        /// <remarks/>
        public decimal wxr_version
        {
            get
            {
                return this.wxr_versionField;
            }
            set
            {
                this.wxr_versionField = value;
            }
        }

        /// <remarks/>
        public string base_site_url
        {
            get
            {
                return this.base_site_urlField;
            }
            set
            {
                this.base_site_urlField = value;
            }
        }

        /// <remarks/>
        public string base_blog_url
        {
            get
            {
                return this.base_blog_urlField;
            }
            set
            {
                this.base_blog_urlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("author")]
        public rssChannelAuthor[] author
        {
            get
            {
                return this.authorField;
            }
            set
            {
                this.authorField = value;
            }
        }

        /// <remarks/>
        public string generator
        {
            get
            {
                return this.generatorField;
            }
            set
            {
                this.generatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item")]
        public rssChannelItem[] item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rssChannelAuthor
    {

        private uint author_idField;

        private string author_loginField;

        private string author_emailField;

        private string author_display_nameField;

        private string author_first_nameField;

        private string author_last_nameField;

        /// <remarks/>
        public uint author_id
        {
            get
            {
                return this.author_idField;
            }
            set
            {
                this.author_idField = value;
            }
        }

        /// <remarks/>
        public string author_login
        {
            get
            {
                return this.author_loginField;
            }
            set
            {
                this.author_loginField = value;
            }
        }

        /// <remarks/>
        public string author_email
        {
            get
            {
                return this.author_emailField;
            }
            set
            {
                this.author_emailField = value;
            }
        }

        /// <remarks/>
        public string author_display_name
        {
            get
            {
                return this.author_display_nameField;
            }
            set
            {
                this.author_display_nameField = value;
            }
        }

        /// <remarks/>
        public string author_first_name
        {
            get
            {
                return this.author_first_nameField;
            }
            set
            {
                this.author_first_nameField = value;
            }
        }

        /// <remarks/>
        public string author_last_name
        {
            get
            {
                return this.author_last_nameField;
            }
            set
            {
                this.author_last_nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rssChannelItem
    {

        private string titleField;

        private string linkField;

        private string pubDateField;

        private string creatorField;

        private rssChannelItemGuid guidField;

        private object descriptionField;

        private string[] encodedField;

        private uint post_idField;

        private string post_dateField;

        private string post_date_gmtField;

        private string comment_statusField;

        private string ping_statusField;

        private string post_nameField;

        private string statusField;

        private byte post_parentField;

        private byte menu_orderField;

        private string post_typeField;

        private string post_passwordField;

        private byte is_stickyField;

        private rssChannelItemCategory[] categoryField;

        private rssChannelItemPostmeta[] postmetaField;

        private rssChannelItemComment[] commentField;

        /// <remarks/>
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string link
        {
            get
            {
                return this.linkField;
            }
            set
            {
                this.linkField = value;
            }
        }

        /// <remarks/>
        public string pubDate
        {
            get
            {
                return this.pubDateField;
            }
            set
            {
                this.pubDateField = value;
            }
        }

        /// <remarks/>
        public string creator
        {
            get
            {
                return this.creatorField;
            }
            set
            {
                this.creatorField = value;
            }
        }

        /// <remarks/>
        public rssChannelItemGuid guid
        {
            get
            {
                return this.guidField;
            }
            set
            {
                this.guidField = value;
            }
        }

        /// <remarks/>
        public object description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("encoded")]
        public string[] encoded
        {
            get
            {
                return this.encodedField;
            }
            set
            {
                this.encodedField = value;
            }
        }

        /// <remarks/>
        public uint post_id
        {
            get
            {
                return this.post_idField;
            }
            set
            {
                this.post_idField = value;
            }
        }

        /// <remarks/>
        public string post_date
        {
            get
            {
                return this.post_dateField;
            }
            set
            {
                this.post_dateField = value;
            }
        }

        /// <remarks/>
        public string post_date_gmt
        {
            get
            {
                return this.post_date_gmtField;
            }
            set
            {
                this.post_date_gmtField = value;
            }
        }

        /// <remarks/>
        public string comment_status
        {
            get
            {
                return this.comment_statusField;
            }
            set
            {
                this.comment_statusField = value;
            }
        }

        /// <remarks/>
        public string ping_status
        {
            get
            {
                return this.ping_statusField;
            }
            set
            {
                this.ping_statusField = value;
            }
        }

        /// <remarks/>
        public string post_name
        {
            get
            {
                return this.post_nameField;
            }
            set
            {
                this.post_nameField = value;
            }
        }

        /// <remarks/>
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public byte post_parent
        {
            get
            {
                return this.post_parentField;
            }
            set
            {
                this.post_parentField = value;
            }
        }

        /// <remarks/>
        public byte menu_order
        {
            get
            {
                return this.menu_orderField;
            }
            set
            {
                this.menu_orderField = value;
            }
        }

        /// <remarks/>
        public string post_type
        {
            get
            {
                return this.post_typeField;
            }
            set
            {
                this.post_typeField = value;
            }
        }

        /// <remarks/>
        public string post_password
        {
            get
            {
                return this.post_passwordField;
            }
            set
            {
                this.post_passwordField = value;
            }
        }

        /// <remarks/>
        public byte is_sticky
        {
            get
            {
                return this.is_stickyField;
            }
            set
            {
                this.is_stickyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("category")]
        public rssChannelItemCategory[] category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("postmeta")]
        public rssChannelItemPostmeta[] postmeta
        {
            get
            {
                return this.postmetaField;
            }
            set
            {
                this.postmetaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("comment")]
        public rssChannelItemComment[] comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rssChannelItemGuid
    {

        private bool isPermaLinkField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool isPermaLink
        {
            get
            {
                return this.isPermaLinkField;
            }
            set
            {
                this.isPermaLinkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rssChannelItemCategory
    {

        private string domainField;

        private string nicenameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string domain
        {
            get
            {
                return this.domainField;
            }
            set
            {
                this.domainField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nicename
        {
            get
            {
                return this.nicenameField;
            }
            set
            {
                this.nicenameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rssChannelItemPostmeta
    {

        private string meta_keyField;

        private string meta_valueField;

        /// <remarks/>
        public string meta_key
        {
            get
            {
                return this.meta_keyField;
            }
            set
            {
                this.meta_keyField = value;
            }
        }

        /// <remarks/>
        public string meta_value
        {
            get
            {
                return this.meta_valueField;
            }
            set
            {
                this.meta_valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rssChannelItemComment
    {

        private uint comment_idField;

        private string comment_authorField;

        private string comment_author_emailField;

        private object comment_author_urlField;

        private string comment_author_IPField;

        private string comment_dateField;

        private string comment_date_gmtField;

        private string comment_contentField;

        private string comment_approvedField;

        private string comment_typeField;

        private uint comment_parentField;

        private uint comment_user_idField;

        private rssChannelItemCommentCommentmeta[] commentmetaField;

        /// <remarks/>
        public uint comment_id
        {
            get
            {
                return this.comment_idField;
            }
            set
            {
                this.comment_idField = value;
            }
        }

        /// <remarks/>
        public string comment_author
        {
            get
            {
                return this.comment_authorField;
            }
            set
            {
                this.comment_authorField = value;
            }
        }

        /// <remarks/>
        public string comment_author_email
        {
            get
            {
                return this.comment_author_emailField;
            }
            set
            {
                this.comment_author_emailField = value;
            }
        }

        /// <remarks/>
        public object comment_author_url
        {
            get
            {
                return this.comment_author_urlField;
            }
            set
            {
                this.comment_author_urlField = value;
            }
        }

        /// <remarks/>
        public string comment_author_IP
        {
            get
            {
                return this.comment_author_IPField;
            }
            set
            {
                this.comment_author_IPField = value;
            }
        }

        /// <remarks/>
        public string comment_date
        {
            get
            {
                return this.comment_dateField;
            }
            set
            {
                this.comment_dateField = value;
            }
        }

        /// <remarks/>
        public string comment_date_gmt
        {
            get
            {
                return this.comment_date_gmtField;
            }
            set
            {
                this.comment_date_gmtField = value;
            }
        }

        /// <remarks/>
        public string comment_content
        {
            get
            {
                return this.comment_contentField;
            }
            set
            {
                this.comment_contentField = value;
            }
        }

        /// <remarks/>
        public string comment_approved
        {
            get
            {
                return this.comment_approvedField;
            }
            set
            {
                this.comment_approvedField = value;
            }
        }

        /// <remarks/>
        public string comment_type
        {
            get
            {
                return this.comment_typeField;
            }
            set
            {
                this.comment_typeField = value;
            }
        }

        /// <remarks/>
        public uint comment_parent
        {
            get
            {
                return this.comment_parentField;
            }
            set
            {
                this.comment_parentField = value;
            }
        }

        /// <remarks/>
        public uint comment_user_id
        {
            get
            {
                return this.comment_user_idField;
            }
            set
            {
                this.comment_user_idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("commentmeta")]
        public rssChannelItemCommentCommentmeta[] commentmeta
        {
            get
            {
                return this.commentmetaField;
            }
            set
            {
                this.commentmetaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rssChannelItemCommentCommentmeta
    {

        private string meta_keyField;

        private string meta_valueField;

        /// <remarks/>
        public string meta_key
        {
            get
            {
                return this.meta_keyField;
            }
            set
            {
                this.meta_keyField = value;
            }
        }

        /// <remarks/>
        public string meta_value
        {
            get
            {
                return this.meta_valueField;
            }
            set
            {
                this.meta_valueField = value;
            }
        }
    }


}