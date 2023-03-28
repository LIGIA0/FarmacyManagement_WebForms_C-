<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Farmacie.aspx.cs" Inherits="Management_Farmacie.Farmacie" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 1326px; width: 1089px">
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="DropDownDS" DataTextField="producator" DataValueField="id_furnizor" AutoPostBack="True">
            </asp:DropDownList>
            <asp:SqlDataSource ID="DropDownDS" runat="server" ConnectionString="<%$ ConnectionStrings:farmacieCS %>" SelectCommand="SELECT * FROM [Furnizori]"></asp:SqlDataSource>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" DataSourceID="GridViewDS" GridLines="Horizontal" AllowSorting="True" DataKeyNames="cod_medicament" Height="224px" ShowFooter="True" Width="629px">
                <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Insert" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Select" Text="Select"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="cod_medicament" InsertVisible="False" SortExpression="cod_medicament" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("cod_medicament") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("cod_medicament") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="id_furnizor" SortExpression="id_furnizor" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id_furnizor") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("id_furnizor") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="stoc" SortExpression="stoc">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("stoc") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtStoc" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("stoc") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="pret" SortExpression="pret">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("pret") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPret" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("pret") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="denumire" SortExpression="denumire">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("denumire") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDenumire" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("denumire") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="producator" SortExpression="producator">
                        <EditItemTemplate>
                         <asp:DropDownList ID="ddFurnizor" runat="server" DataSourceID="DropDownDS" DataTextField="producator" DataValueField="id_furnizor" SelectedValue='<%# Bind("id_furnizor") %>'>
                </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddFurnizor" runat="server" DataSourceID="DropDownDS" DataTextField="producator" DataValueField="id_furnizor">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("producator") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#487575" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#275353" />
            </asp:GridView>
            <asp:SqlDataSource ID="GridViewDS" runat="server" ConnectionString="<%$ ConnectionStrings:farmacieCS %>" SelectCommand="SELECT Medicamente.stoc, Medicamente.pret, Medicamente.denumire, Furnizori.producator, Medicamente.cod_medicament, Furnizori.id_furnizor FROM Medicamente INNER JOIN Furnizori ON Medicamente.id_furnizor = Furnizori.id_furnizor WHERE (Medicamente.id_furnizor = @id_furnizor)" DeleteCommand="DELETE FROM [Medicamente] WHERE [cod_medicament] = @cod_medicament" InsertCommand="INSERT INTO [Medicamente] ([id_furnizor], [stoc], [pret], [denumire]) VALUES (@id_furnizor, @stoc, @pret, @denumire)" UpdateCommand="UPDATE [Medicamente] SET [id_furnizor] = @id_furnizor, [stoc] = @stoc, [pret] = @pret, [denumire] = @denumire WHERE [cod_medicament] = @cod_medicament">
                <DeleteParameters>
                    <asp:Parameter Name="cod_medicament" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="id_furnizor" Type="Int32" />
                    <asp:Parameter Name="stoc" Type="Int32" />
                    <asp:Parameter Name="pret" Type="Double" />
                    <asp:Parameter Name="denumire" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownList1" Name="id_furnizor" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="id_furnizor" Type="Int32" DefaultValue="2" />
                    <asp:Parameter Name="producator" Type="String" />
                    <asp:Parameter Name="stoc" Type="Int32" />
                    <asp:Parameter Name="pret" Type="Double" />
                    <asp:Parameter Name="denumire" Type="String" />
                    <asp:Parameter Name="cod_medicament" Type="Int32"  DefaultValue="22"/>
                </UpdateParameters>
            </asp:SqlDataSource>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            <asp:TextBox ID="txtPret" runat="server" AutoPostBack="True"></asp:TextBox>
            <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="White" />
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" DataSourceID="FurnizoriDS" DataTextField="producator" DataValueField="id_furnizor">
            </asp:DropDownList>
            <asp:SqlDataSource ID="FurnizoriDS" runat="server" ConnectionString="<%$ ConnectionStrings:farmacieCS %>" SelectCommand="SELECT * FROM [Furnizori]"></asp:SqlDataSource>
            <asp:GridView ID="GridView3" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                <SortedDescendingHeaderStyle BackColor="#93451F" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
