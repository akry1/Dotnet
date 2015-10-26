<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetailedView.aspx.cs" Inherits="MovieProphecy.DetailedView" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-left: -10%">

        <p>
            <asp:Label ID="lblMovieName" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label>
        </p>
        <ul class="nav nav-tabs" data-tabs="tabs">
            <li id="li1" runat ="server" class="active" ><asp:LinkButton ID="lnlTab1" Text="Home" runat="server" OnClick="lnktab_Click"></asp:LinkButton></li>
            <li id="li2" runat ="server"><asp:LinkButton ID="lnlTab2" Text="Chart" runat="server" OnClick="lnktab_Click"></asp:LinkButton></li>
            <li id="li3" runat ="server"><asp:LinkButton ID="lnlTab3" Text="Timeline" runat="server" OnClick="lnktab_Click"></asp:LinkButton></li>
            <li id="li4" runat ="server"><asp:LinkButton ID="lnlTab4" Text="Tweets" runat="server" OnClick="lnktab_Click"></asp:LinkButton></li>
        </ul>
                

       <div id="myTabContent" class="tab-content"> 
            <asp:Panel CssClass ="tab-pane fade in active" ID="Panel1" runat="server">
                <div class="panel panel-default" style="width: 400px; margin-top: 5%">
                    <div class="panel-heading">
                        <asp:Label ID="lblMovieNameHome" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="panel-body">
                        <ul style="list-style-type: none">
                            <li>
                                <table>
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="lblPercent" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                                        </td>
                                        <td style="width: 60%; float: right">
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
             <asp:Panel CssClass ="tab-pane fade in active" ID="Panel2" runat="server">
                <table style="margin-top: 5%">
                    <tr>
                        <td>
                            <asp:Chart ID="Chart1" runat="server" BackColor="WhiteSmoke" Height="296px" Width="412px" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69">
                                <Legends>
                                    <asp:Legend BackColor="Transparent" Alignment="Center" Docking="Bottom" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" LegendStyle="Row"></asp:Legend>
                                </Legends>
                                <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                <Series>
                                    <asp:Series Name="pieChart" ChartType="Pie" ChartArea="ChartArea1" BorderColor="180, 26, 59, 105"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="Transparent" ShadowColor="Transparent" BorderWidth="0">
                                        <Area3DStyle Rotation="0" />
                                        <AxisY LineColor="64, 64, 64, 64">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                        </AxisY>
                                        <AxisX LineColor="64, 64, 64, 64">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                        <td>
                            <asp:Chart ID="Chart2" runat="server" BackColor="WhiteSmoke" Height="296px" Width="412px" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69">
                                 <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                <Series>
                                    <asp:Series Name="barChart" ChartType="Bar" ChartArea="ChartArea1" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="Transparent" ShadowColor="Transparent" BorderWidth="0">
                                        <Area3DStyle Rotation="0" />
                                        <AxisY LineColor="64, 64, 64, 64">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                        </AxisY>
                                        <AxisX LineColor="64, 64, 64, 64">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>

                    </tr>

                </table>
                 </asp:Panel>

            <asp:Panel CssClass ="tab-pane fade in active" ID="Panel3" runat="server">
                
                <div style="margin-top: 5%">
                    <asp:Chart ID="Chart3" runat="server" Height="296px" Width="850px" BackColor="211, 223, 240" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69">
                        <Series>
                            <asp:Series Name="Series1" Legend="Default" LegendText ="Positive" ChartType="Column" ChartArea="ChartArea1" Color="Green" BorderColor="180, 26, 59, 105"></asp:Series>
                            <asp:Series Name="Series2" Legend="Default" LegendText ="Negative" ChartType="Column" ChartArea="ChartArea1" Color="Red" BorderColor="180, 26, 59, 105"></asp:Series>
                            <asp:Series Name="Series3" Legend="Default" LegendText ="Neutral" ChartType="Column" ChartArea="ChartArea1" Color="Gray" BorderColor="180, 26, 59, 105"></asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" Docking="Right" IsDockedInsideChartArea="false" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Enabled="true" Name="Default"></asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Inclination="15" WallWidth="0" />
                                <Position Y="3" Height="92" Width="92" X="2"></Position>
                                <AxisY LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                </AxisY>
                                <AxisX LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8" IntervalType="Months">
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <MajorGrid LineColor="64, 64, 64, 64"  />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
                          
                </asp:Panel>
                <asp:Panel CssClass ="tab-pane fade in active" ID="Panel4" runat="server">
                <div style="margin-top: 5%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                        <asp:GridView ID="grdDisplayData" runat="server" Height ="400px" CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="True" AllowPaging="false" EnableSortingAndPagingCallbacks="true"  OnSorting="grdDisplayData_Sorting">
                            <alternatingrowstyle backcolor="White" forecolor="#284775" />
                            <editrowstyle backcolor="#999999" />
                            <footerstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
                            <headerstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
                            <pagerstyle backcolor="#284775" forecolor="White" horizontalalign="Center" />
                            <rowstyle backcolor="#F7F6F3" forecolor="#333333" />
                            <selectedrowstyle backcolor="#E2DED6" font-bold="True" forecolor="#333333" />
                            <sortedascendingcellstyle backcolor="#E9E7E2" />
                            <sortedascendingheaderstyle backcolor="#506C8C" />
                            <sorteddescendingcellstyle backcolor="#FFFDF8" />
                            <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                        </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                    </asp:Panel>
        </div>
    </div>
</asp:Content>
