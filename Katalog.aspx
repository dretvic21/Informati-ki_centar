<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Katalog.aspx.cs" Inherits="IcKatalog.Katalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;

        window.onload = function () {
            UpperBound = parseInt('<%= this.GridView1.Rows.Count %>') - 1;
            LowerBound = 0;
            SelectedRowIndex = -1;
        }

        function SelectRow(CurrentRow, RowIndex) {
            if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound) return;

            if (SelectedRow != null) {
                SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                SelectedRow.style.color = SelectedRow.originalForeColor;
            }

            if (CurrentRow != null) {
                CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
                CurrentRow.originalForeColor = CurrentRow.style.color;
                CurrentRow.style.backgroundColor = "DarkSlateBlue";
                CurrentRow.style.color = 'GhostWhite';
            }

            SelectedRow = CurrentRow;
            SelectedRowIndex = RowIndex;
            setTimeout("SelectedRow.focus();", 0);
        }

        function SelectSibling(e) {
            var e = e ? e : window.event;
            var KeyCode = e.which ? e.which : e.keyCode;

            if (KeyCode == 40)
                SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
            else if (KeyCode == 38)
                SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);

            return false;
        }
     </script>

<table style="width: 100%; height: 100%;" border="1">
  <tr>
    <th style="width: 30%; font-family: Verdana; height: 10%;">Odaberite karakterisitke artikla</th>
    <th style="width: 70%; font-family: Verdana;height: 10%;">Odabrano</th>
  </tr>
  <tr>
    <td style="height: 90%; vertical-align: top;"><asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="changegrid"></asp:TreeView></td>
    <td style="vertical-align: top">
        <asp:FormView ID="FormView1" visible="false" runat="server" Height="100%" Width="100%">
            <ItemTemplate>
                <asp:Image ID="Image2" runat="server" Width="100%" Height="20%" ImageAlign="Middle" />
                <asp:Table ID="Table1" runat="server" Height="80%" Width="100%" GridLines="Both" BorderWidth="1">

                    <asp:TableRow runat="server">
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                    </asp:TableRow>

                </asp:Table>
            <table style="width: 100%; height: 80%;" border="1">
                <tr>
                    <td>Šifra artikla:</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Naziv artikla:</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Jednica mjere:</td>
                    <td>&nbsp;</td>
                </tr>
            </table>    
            </ItemTemplate>
        </asp:FormView>
        <asp:GridView ID="GridView1" runat="server" 
        AllowSorting="True" 
        Width="100%" 
        Height="100%" 
        BackColor="LightGoldenrodYellow" 
        BorderColor="Tan" 
        BorderWidth="1px" 
        CellPadding="2" 
        ForeColor="Black" 
        ShowHeaderWhenEmpty="True" 
        AutoGenerateColumns="False" 
        DataKeyNames="sifra" 
        EmptyDataText="Nema artikala za odabrane uvjete!" 
        OnSorting="Gridview1_sort" 
        onRowCreated="GridView1_RowCreated">
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                <Columns>
                    <asp:BoundField DataField="sifra" ReadOnly="True" SortExpression="sifra" HeaderText="Šifra artikla" >
                    <ControlStyle Width="15%" BackColor="White" ForeColor="Black" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%"/>
                    <ItemStyle Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="opis" ReadOnly="True" SortExpression="opis" HeaderText="Naziv artikla" >
                    <ControlStyle Width="60%" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60%"/>
                    <ItemStyle Width="60%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="jm" ReadOnly="True" SortExpression="jm" HeaderText="Jednica mjere" >
                    <ControlStyle Width="20%" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%"/>
                    <ItemStyle Width="20%" />
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="artid" DataNavigateUrlFormatString="katalog.aspx?artid={0}" DataTextField="sifra" HeaderText="Šifra" />
                </Columns>
                <FooterStyle BackColor="Tan" />
                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                <SortedAscendingCellStyle BackColor="#FAFAE7" />
                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
            </asp:GridView></td>
  </tr>
</table>
</asp:Content>
