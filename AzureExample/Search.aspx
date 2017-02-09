<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="AzureExample.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/typeahead.jquery.min.js"></script>
    <script src="Scripts/typeahead.bundle.js"></script>
    <script src="Scripts/bloodhound.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
    <script type="text/javascript">
        jQuery(document).ready(function () {

            $('#searchTermField').typeahead({ minLength: 3 }, {
                async: true,
                source: function (query, processSync, processAsync) {
                    var url = '/search.ashx';
                    url += '?Term=' + query;

                    $.ajax({
                        url: url,
                        type: "get",
                        dataType: "json",
                        success: function (data) {

                            processAsync(data);
                        }
                    });

                }
            });
        }
            );

    </script>
    <style>
        .tt-query, /* UPDATE: newer versions use tt-input instead of tt-query */
.tt-hint {
    width: 396px;
    height: 30px;
    padding: 8px 12px;
    font-size: 24px;
    line-height: 30px;
    border: 2px solid #ccc;
    border-radius: 8px;
    outline: none;
}

.tt-query { /* UPDATE: newer versions use tt-input instead of tt-query */
    box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
}

.tt-hint {
    color: #999;
}

.tt-menu { /* UPDATE: newer versions use tt-menu instead of tt-dropdown-menu */
    width: 422px;
    margin-top: 12px;
    padding: 8px 0;
    background-color: #fff;
    border: 1px solid #ccc;
    border: 1px solid rgba(0, 0, 0, 0.2);
    border-radius: 8px;
    box-shadow: 0 5px 10px rgba(0,0,0,.2);
}

.tt-suggestion {
    padding: 3px 20px;
    font-size: 18px;
    line-height: 24px;
}

.tt-suggestion.tt-is-under-cursor { /* UPDATE: newer versions use .tt-suggestion.tt-cursor */
    color: #fff;
    background-color: #0097cf;

}

.tt-suggestion p {
    margin: 0;
}

body {
    margin: 60px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
            <h1>Search Test</h1>
            <div class="input-group">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtFilter" placeholder="Filter"></asp:TextBox>

            </div>
            <div class="input-group">
                <asp:TextBox runat="server" ID="searchTermField" type="text" placeholder="Search" ClientIDMode="static" class="form-control typeahead"
                    name="Term" />
                <span class="input-group-btn">
                    <asp:Button class="btn btn-primary" type="text" runat="server" OnClick="btn_Click" ID="btn" Text="Search"></asp:Button>
                </span>
            </div>

            <div class="well">
                <asp:Literal runat="server" ID="txtResults"></asp:Literal>
            </div>
    </form>
</body>
</html>
