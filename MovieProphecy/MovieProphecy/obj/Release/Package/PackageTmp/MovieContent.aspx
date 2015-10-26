<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MovieContent.aspx.cs" Inherits="MovieProphecy.Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <br />
        <div runat="server" style="position: relative; margin-left: 0%">
            <asp:GridView ID="grdDisplayData" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="grdDisplayData_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" OnClick="btn_Click" Text='<% #Bind("movie_name")%>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            
            <br />
            <asp:Panel ID="pnlMovieDetails" runat="server">
                <div class="panel panel-default" style="width: 400px">
                    <div class="panel-heading">
                        <asp:Label ID="lblMovieName" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="panel-body">
                        <ul style="list-style-type: none">
                            <li>
                                <table>
                                    <tr>
                                        <td style ="width:20%">
                                            <asp:Label ID="lblPercent" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                                        </td>
                                        <td  style="width:60%;float :right">
                                            <br />
                                            <div class="progress">
                                                <div id="divBar" class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" runat="server">
                                                    <span class="sr-only"></span>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                            </li>
                            <li>Average Rating:<asp:Label ID="lblRating" runat="server"></asp:Label></li>
                            <li>Reviews Counted:<asp:Label ID="lblNoOfReviews" runat="server"></asp:Label></li>
                            <li>Verdict:<asp:Label ID="lblReview" runat="server"></asp:Label></li>
                        </ul>
                    </div>
                </div>
            </asp:Panel>
            <br />
        </div>
        <asp:LinkButton ID="lnkInfo" runat="server" OnClick="LinkButton2_Click">Click here for Detailed Information</asp:LinkButton>


</asp:Content>
