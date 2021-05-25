<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucAgregaComentarioCotizacion.ascx.vb" Inherits="PMD_WAS.ucAgregaComentarioCotizacion" %>


<div id="modalComentariosCotizacion" class="modal modal-medium fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Comentarios de la cotizacion</h5>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body">

                <asp:UpdatePanel ID="updComentarios" runat="server">
                    <ContentTemplate>

                        <asp:HiddenField ID="hdnIdCotizacionDetalle" runat="server" Value="0" />
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-10 col-md-8">
                                    <p class="simple">Comentarios</p>
                                    <asp:TextBox ID="txtComentario" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-2 col-md-4">
                                    <p class="simple" style="visibility:hidden;">Comentarios</p>
                                    <asp:Button ID="btnAgregaComentario" runat="server" CssClass="btn btn-primary" Text="Agregar comentario" OnClick="btnAgregaComentario_Click" />
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-12">
                                    <table class="table table-bordered table-hover table-small" width="100%">
                                        <thead>
                                            <tr>
                                                <th><b>Usuario</b></th>
                                                <th><b>Comentario</b></th>
                                                <th><b>Fecha envio</b></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptComentarios" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("Usuario") %></td>
                                                        <td><%# Eval("Comentario") %></td>
                                                        <td><%# Eval("Fecha") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Panel ID="pnlSinComentarios" runat="server" Visible='<%# rptComentarios.Items.Count = 0 %>'>
                                                        <tr>
                                                            <td colspan="3">
                                                                <h5>No hay comentarios</h5>
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal-footer">
                <a class="right btn btn-primary" data-dismiss="modal">Cerar</a>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function abreModalComentarios() {
        $('#modalComentariosCotizacion').modal('show');
    }

    function cierraModalComentarios() {
        $('#modalComentariosCotizacion').modal('hide');
    }
</script>
