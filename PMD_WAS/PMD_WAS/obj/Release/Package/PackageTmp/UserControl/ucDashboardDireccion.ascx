<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucDashboardDireccion.ascx.vb" Inherits="PMD_WAS.ucDashboardDireccion" %>

<asp:UpdatePanel ID="updDashboard" runat="server">
    <ContentTemplate>
        <div class="container-fluid">
    <div class="row">
        <div class="col-3 col-md-4">
            <asp:DropDownList ID="ddlSecretaria" runat="server" CssClass="form-control small select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-3 col-md-4">
            <asp:DropDownList ID="ddlDireccion" runat="server" CssClass="form-control small select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-3 col-md-2">
            <asp:DropDownList ID="ddlAdmin" runat="server" CssClass="form-control small select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-3 col-md-2">
            <asp:DropDownList ID="ddlAnio" runat="server" CssClass="form-control small select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="col-md-6 col-xl">
            <div class="dashboard-item">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-3">
                            <i class="fas fa-hand-holding-usd logo-dashboard"></i>
                        </div>
                        <div class="col-9">
                            <asp:Label ID="lblPresupuestoAsignado" runat="server" CssClass="presupuesto-label" Text="$0"></asp:Label>
                            <br />
                            <label class="label-footer">PRESUPUESTO ASIGNADO</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 col-xl">
            <div class="dashboard-item">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-3">
                            <i class="fas fa-receipt logo-dashboard"></i>
                        </div>
                        <div class="col-9">
                            <asp:Label ID="lblPresupuestoCapturado" runat="server" CssClass="presupuesto-label" Text="$0"></asp:Label><br />
                            <label class="label-footer">Capturado</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 col-xl">
            <div class="dashboard-item">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-3"><i class="fas fa-money-bill-alt logo-dashboard"></i></div>
                        <div class="col-9">
                            <asp:Label ID="lblPresupuestoAutorizado" runat="server" CssClass="presupuesto-label" Text="$0"></asp:Label>
                            <br />
                            <label class="label-footer">Autorizado</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 col-xl">
            <div class="dashboard-item">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-3"><i class="fas fa-money-check-alt logo-dashboard"></i></div>
                        <div class="col-9">
                            <asp:Label ID="lblPresupuestoDevengado" runat="server" CssClass="presupuesto-label" Text="$0"></asp:Label>
                            <br />
                            <label class="label-footer">Devengado</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="d-none d-xxl-flex col-md-6 col-xl">
            <div class="dashboard-item">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-3"><i class="fas fa-university logo-dashboard"></i></div>
                        <div class="col-9">
                            <asp:Label ID="lblPresupuestoComprometido" runat="server" CssClass="presupuesto-label" Text="$0"></asp:Label>
                            <br />
                            <label class="label-footer">Comprometido</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
    </ContentTemplate>
</asp:UpdatePanel>
