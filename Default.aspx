<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FFLeastResistancePath.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Least Resistance Path Finder</title>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" style="margin:20px;" runat="server">
        <div class="row">
            <div class="col-md-8">
                <h2>Upload Input files. Only .txt files are allowed.Default format is given below</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                Input<br />
                3 4 1 2 8 6<br />
                6 1 8 2 7 4<br />
                5 9 3 9 9 5<br />
                8 4 1 3 2 6<br />
                3 7 2 8 6 4<br />
            </div>
            <div class="col-md-4">
                Ouput<br />
                Yes<br /><br />
                16<br /><br />
                1 2 3 4 4 5
            </div>
        </div><br />
        <div class="row">
            <div class="col-md-4">
                <asp:FileUpload ID="fileInput" runat="server" />
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnGetOutput" CssClass="btn btn-primary" Text="Get Least Path" OnClick="btnGetOutput_Click" runat="server" />
            </div>
        </div><br />
        <div class="row">
            <div class="col-md-8" id="divMessage" runat="server"></div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div id="divInput" runat="server"></div>
            </div>
            <div class="col-md-4">
                <div id="divOutput" runat="server"></div>
            </div>
        </div>
    </form>
</body>
</html>
